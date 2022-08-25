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
    public class UserCategoryController : Controller
    {
        CategoryDAL _categoryDAL = new CategoryDAL();
        // GET: UserCategory
        public ActionResult Index()
        {
            var categoryList = _categoryDAL.GetAll();

            if (categoryList.Count == 0)
            {
                TempData["InfoMessage"] = "No categories to show";
            }

            return View(categoryList);
        }
    }
}