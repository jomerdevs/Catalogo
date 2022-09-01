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
    public class UserCategoryController : Controller
    {        
        CategoryBL _categoryBL = new CategoryBL();

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ListCat(string search)
        {            
            return Json(_categoryBL.GetAll(search), JsonRequestBehavior.AllowGet);
        }

        public JsonResult FilterCategoryList(string search)
        {            
            return Json(_categoryBL.FilterCategoryList(search), JsonRequestBehavior.AllowGet);
        }
        
    }
}