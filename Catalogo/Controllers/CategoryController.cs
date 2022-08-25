using Catalogo.DataLayer;
using Catalogo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Catalogo.Controllers
{
    public class CategoryController : Controller
    {
        CategoryDAL _categoryDAL = new CategoryDAL();

        // GET ALL CATEGORIES
        public ActionResult Index()
        {
            var categoryList = _categoryDAL.GetAll();

            if (categoryList.Count == 0)
            {
                TempData["InfoMessage"] = "No categories to show";
            }

            return View(categoryList);
        }

        // CATEGORY DETAILS
        public ActionResult Details(int id)
        {
            try
            {
                var category = _categoryDAL.GetCategoryById(id).FirstOrDefault();

                if (category == null)
                {
                    TempData["InfoMessage"] = "Category not available";
                    return RedirectToAction("Index");
                }
                return View(category);
            }
            catch (Exception ex)
            {
                TempData["ErroMessage"] = ex.Message;
                return View();
            }
        }

        // CREATE CATEGORY METHOD: GET
        public ActionResult Create()
        {
            return View();
        }

        // CREATE CATEGORY METHOD: POST
        [HttpPost]
        public ActionResult Create(Category category)
        {
            bool IsCreated = false;
            try
            {
                if (ModelState.IsValid)
                {
                    IsCreated = _categoryDAL.CreateCategory(category);

                    if (IsCreated)
                    {
                        TempData["SuccessMessage"] = "Category has been created succesfully!";
                    }
                    else
                    {
                        TempData["ErroMessage"] = "Unable to create the category, possibly this category already exist";
                    }
                }

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData["ErroMessage"] = ex.Message;
                return View();
            }
        }

        // UPDATE CATEGORY METHOD: GET
        public ActionResult Edit(int id)
        {
            var category = _categoryDAL.GetCategoryById(id).FirstOrDefault();

            if (category == null)
            {
                TempData["InfoMessage"] = "Category not available";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // UPDATE CATEGORY METHOD: GET
        [HttpPost, ActionName("Edit")]
        public ActionResult UpdateCategory(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool IsUpdated = _categoryDAL.UpdateCategory(category);

                    if (IsUpdated)
                    {
                        TempData["SuccessMessage"] = "Category has been updated succesfully!";
                    }
                    else
                    {
                        TempData["ErroMessage"] = "Unable to update the product";
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // DELETE CATEGORY METHOD: GET
        public ActionResult Delete(int id)
        {
            try
            {
                var category = _categoryDAL.GetCategoryById(id).FirstOrDefault();

                if (category == null)
                {
                    TempData["InfoMessage"] = "Product not available";
                    return RedirectToAction("Index");
                }
                return View(category);
            }
            catch (Exception ex)
            {
                TempData["ErroMessage"] = ex.Message;
                return View();
            }
        }

        // DELETE CATEGORY METHOD: POST
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteCategory(int id)
        {
            try
            {
                string result = _categoryDAL.DeleteCategory(id);

                if (result.Contains("deleted"))
                {
                    TempData["SuccessMessage"] = result;
                }
                else
                {
                    TempData["ErroMessage"] = result;
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErroMessage"] = ex.Message;
                return View();
            }
        }
    }
}
