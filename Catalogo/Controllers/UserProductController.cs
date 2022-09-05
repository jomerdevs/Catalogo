using Catalogo.Permissions;
using DataBusiness;
using DataEntityLayer;
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

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult List(string search)
        {
            
            return Json(_productBL.GetAll(search), JsonRequestBehavior.AllowGet);
        }

        public JsonResult FilterProductsList(string search)
        {
            
            return Json(_productBL.FilterProductList(search), JsonRequestBehavior.AllowGet);
        }
        
    }
}