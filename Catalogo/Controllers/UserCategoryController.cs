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
        public ActionResult Index(string search)
        {
            var categoryList = _categoryBL.GetAll();

            if (!String.IsNullOrEmpty(search))
            {
                categoryList = categoryList.Where(s => s.Name.ToUpper().Contains(search.ToUpper())
                                || s.Description.ToUpper().Contains(search.ToUpper())).ToList();
            }
            if (categoryList.Count == 0)
            {
                TempData["InfoMessage"] = "No categories to show";
            }

            return View(categoryList);
        }
    }
}