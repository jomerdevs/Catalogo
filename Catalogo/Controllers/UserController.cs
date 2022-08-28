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
        public ActionResult Index(string search)
        {
            var usersList = _userBL.GetAll();

            if (!String.IsNullOrEmpty(search))
            {
                usersList = usersList.Where(s => s.FirstName.ToUpper().Contains(search.ToUpper())
                                || s.LastName.ToUpper().Contains(search.ToUpper()) || s.Email.ToUpper().Contains(search.ToUpper())).ToList();
            }
            if (usersList.Count == 0)
            {
                TempData["InfoMessage"] = "No users to show";
            }
            return View(usersList);
        }

    }
}
