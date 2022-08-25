using Catalogo.DataLayer;
using Catalogo.Permissions;
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
        ProductDAL _productDAL = new ProductDAL();
        // GET: UserProduct
        public ActionResult Index()
        {
            var productList = _productDAL.GetAll();

            if (productList.Count == 0)
            {
                TempData["InfoMessage"] = "No products to show";
            }

            return View(productList);
        }
    }
}