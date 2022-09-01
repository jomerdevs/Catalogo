using Admin.Permissions;
using DataBusiness;
using DataEntityLayer;
using Microsoft.Build.Framework;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Controllers
{
    [ValidateSessionAttribute]
    public class ProductController : Controller
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        ProductBL _productBL = new ProductBL();        

        public ActionResult Index(string search)
        {
            var productList = _productBL.GetAll(search);
            
            if (productList.Count == 0)
            {
                TempData["InfoMessage"] = "No products to show";
            }

            return View(productList);
        }

        
        public ActionResult Details(int id)
        {
            try
            {
                var product = _productBL.GetProductById(id).FirstOrDefault();

                if (product == null)
                {
                    TempData["InfoMessage"] = "Product not available";
                    return RedirectToAction("Index");
                }
                return View(product);
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
        public ActionResult Create(ProductEntity product)
        {
            bool IsCreated = false;
            try
            {
                if (ModelState.IsValid)
                {
                    IsCreated = _productBL.CreateProduct(product);

                    if (IsCreated)
                    {
                        TempData["SuccessMessage"] = "Product has been created succesfully!";
                    }
                    else
                    {
                        TempData["ErroMessage"] = "Unable to create the product, possibly this product already exist";
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
            var product = _productBL.GetProductById(id).FirstOrDefault();

            if (product == null)
            {
                TempData["InfoMessage"] = "Product not available";
                return RedirectToAction("Index");
            }
            return View(product);
        }

        
        [HttpPost, ActionName("Edit")]
        public ActionResult UpdateProduct(ProductEntity product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool IsUpdated = _productBL.UpdateProduct(product);

                    if (IsUpdated)
                    {
                        TempData["SuccessMessage"] = "Product has been updated succesfully!";
                    }
                    else
                    {
                        TempData["ErroMessage"] = "Unable to update the product";
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

        
        public ActionResult Delete(int id)
        {
            try
            {
                var product = _productBL.GetProductById(id).FirstOrDefault();

                if (product == null)
                {
                    TempData["InfoMessage"] = "Product not available";
                    return RedirectToAction("Index");
                }
                return View(product);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return View();
            }
        }

        
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteProduct(int id)
        {
            try
            {
                string result = _productBL.Delete(id);

                if (result.Contains("deleted"))
                {
                    TempData["SuccessMessage"] = result;
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErroMessage"] = "Error deleting product..!";
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
