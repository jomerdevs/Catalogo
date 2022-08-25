using Catalogo.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Catalogo.Controllers
{
    public class UserController : Controller
    {
        AdminDAL _userDAL = new AdminDAL();
        // GET: User
        public ActionResult Index()
        {
            var usersList = _userDAL.GetAll();

            if (usersList.Count == 0)
            {
                TempData["InfoMessage"] = "No products to show";
            }
            return View(usersList);
        }
        
    }
}
