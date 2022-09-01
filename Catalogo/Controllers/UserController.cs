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
    public class UserController : Controller
    {
        AdminBL _userBL = new AdminBL();
        
        public ActionResult Index()
        {
            
            return View();
        }

        public JsonResult List(string search)
        {
            return Json(_userBL.GetAll(search), JsonRequestBehavior.AllowGet);
        }

        public JsonResult FilterCategoryList(string search)
        {
            return Json(_userBL.FilterUserList(search), JsonRequestBehavior.AllowGet);
        }

    }
}
