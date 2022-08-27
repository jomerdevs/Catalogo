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
        // GET: UserCategory
        public ActionResult Index()
        {
            var categoryList = _categoryBL.GetAll();

            if (categoryList.Count == 0)
            {
                TempData["InfoMessage"] = "No categories to show";
            }

            return View(categoryList);
        }
    }
}