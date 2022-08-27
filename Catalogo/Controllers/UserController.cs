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
        // GET: User
        public ActionResult Index()
        {
            var usersList = _userBL.GetAll();

            if (usersList.Count == 0)
            {
                TempData["InfoMessage"] = "No products to show";
            }
            return View(usersList);
        }

    }
}
