using Catalogo.Permissions;
using DataBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Catalogo.Controllers
{
    [ValidateSession]
    public class UserProductController : Controller
    {
        ProductBL _productBL = new ProductBL();

        // GET: UserProduct
        public ActionResult Index()
        {
            var productList = _productBL.GetAll();

            if (productList.Count == 0)
            {
                TempData["InfoMessage"] = "No products to show";
            }

            return View(productList);
        }
    }
}