using Admin.Permissions;
using DataBusiness;
using DataEntityLayer;
using NLog;
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
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        CategoryBL _categoryBL = new CategoryBL();
        
        public ActionResult Index(string search)
        {
            var categoryList = _categoryBL.GetAll(search);
            
            if (categoryList.Count == 0)
            {
                TempData["InfoMessage"] = "No categories to show";
            }

            return View(categoryList);
        }
        
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
                logger.Error(ex.Message);
                return View();
            }
        }
        
        public ActionResult Create()
        {
            return View();
        }
        
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
                logger.Error(ex.Message);
                return View();
            }
        }
        
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
            catch(Exception ex)
            {
                logger.Error(ex.Message);
                return View();
            }
        }
        
        public ActionResult Delete(int id)
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
                logger.Error(ex.Message);
                return View();
            }
        }
        
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
                logger.Error(ex.Message);
                return View();
            }
        }
    }
}
