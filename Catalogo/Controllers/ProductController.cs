﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Catalogo.DataLayer;
using Catalogo.Models;
using Catalogo.Permissions;

namespace Catalogo.Controllers
{
    [ValidateSessionAttribute]
    public class ProductController : Controller
    {
        ProductDAL _productDAL = new ProductDAL();

        // GET: Product
        public ActionResult Index()
        {
            var productList = _productDAL.GetAll();

            if (productList.Count == 0)
            {
                TempData["InfoMessage"] = "No products to show";
            }
            
            return View(productList);
        }
        

        // DETAILS
        public ActionResult Details(int id)
        {
            try
            {
                var product = _productDAL.GetProductById(id).FirstOrDefault();

                if (product == null)
                {
                    TempData["InfoMessage"] = "Category not available";
                    return RedirectToAction("Index");
                }
                return View(product);
            }
            catch (Exception ex)
            {
                TempData["ErroMessage"] = ex.Message;
                return View();
            }
        }

        // CREATE PRODUCT METHOD: GET
        public ActionResult Create()
        {
            return View();
        }

        // CREATE PRODUCT METHOD: POST
        [HttpPost]
        public ActionResult Create(Product product)
        {
            bool IsCreated = false;
            try
            {
                if (ModelState.IsValid)
                {
                    IsCreated = _productDAL.CreateProduct(product);

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
            catch(Exception ex)
            {
                TempData["ErroMessage"] = ex.Message;
                return View();
            }
        }

        // UPDATE PRODUCT METHOD: GET
        public ActionResult Edit(int id)
        {
            var product = _productDAL.GetProductById(id).FirstOrDefault();

            if (product == null)
            {
                TempData["InfoMessage"] = "Product not available";
                return RedirectToAction("Index");
            }
            return View(product);

            
        }

        // UPDATE PRODUCT METHOD: POST
        [HttpPost, ActionName("Edit")]
        public ActionResult UpdateProduct(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool IsUpdated = _productDAL.UpdateProduct(product);

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
                TempData["ErroMessage"] = ex.Message;
                return View();

            }        
           
            
        }

        // DELETE PRODUCT METHOD: GET
        public ActionResult Delete(int id)
        {
            try
            {
                var product = _productDAL.GetProductById(id).FirstOrDefault();

                if (product == null)
                {
                    TempData["InfoMessage"] = "Product not available";
                    return RedirectToAction("Index");
                }
                return View(product);
            }
            catch (Exception ex)
            {
                TempData["ErroMessage"] = ex.Message;
                return View();
            }
        }

        // DELETE PRODUCT METHOD: POST
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteProduct(int id)
        {
            try
            {
                string result = _productDAL.DeleteProduct(id);

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
