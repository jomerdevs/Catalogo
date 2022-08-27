using Admin.Permissions;
using DataBusiness;
using DataEntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Controllers
{
    [ValidateSession]
    public class CategoryController : Controller
    {
        CategoryBL _categoryBL = new CategoryBL();

        // GET ALL CATEGORIES
        public ActionResult Index()
        {
            var categoryList = _categoryBL.GetAll();

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
                var category = _categoryBL.GetCategoryById(id).FirstOrDefault();

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
        public ActionResult Create(CategoryEntity category)
        {
            bool IsCreated = false;
            try
            {
                if (ModelState.IsValid)
                {
                    IsCreated = _categoryBL.CreateCategory(category);

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
            catch (Exception ex)
            {
                TempData["ErroMessage"] = ex.Message;
                return View();
            }
        }

        // UPDATE CATEGORY METHOD: GET
        public ActionResult Edit(int id)
        {
            var category = _categoryBL.GetCategoryById(id).FirstOrDefault();

            if (category == null)
            {
                TempData["InfoMessage"] = "Category not available";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // UPDATE CATEGORY METHOD: POST
        [HttpPost, ActionName("Edit")]
        public ActionResult UpdateCategory(CategoryEntity category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool IsUpdated = _categoryBL.UpdateCategory(category);

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
                var category = _categoryBL.GetCategoryById(id).FirstOrDefault();

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
                string result = _categoryBL.Delete(id);

                if (result.Contains("deleted"))
                {
                    TempData["SuccessMessage"] = result;
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErroMessage"] = "Error deleting category..!";
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                TempData["ErroMessage"] = ex.Message;
                return View();
            }
        }
    }
}
