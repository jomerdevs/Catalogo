using DataBusiness;
using Admin;
using DataEntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLog;

namespace Catalogo.Controllers
{
    public class AuthController : Controller
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        AuthBL _authBL = new AuthBL();
        
        public ActionResult Login()
        {
            return View();
        }
        
        [HttpPost, ActionName("Login")]
        public ActionResult LoginUser(UserEntity user)
        {
            try
            {
                if (user.Password != null)
                {
                    var login = _authBL.Login(user);

                    if (login)
                    {
                        if (user.IsAdmin)
                        {
                            Session["user"] = user;
                            return RedirectToAction("Index", "Product");
                        }
                        else
                        {
                            Session["user"] = user;
                            return RedirectToAction("Index", "UserProduct");
                        }
                    }
                    else
                    {
                        TempData["ErroMessage"] = "User not found";
                        return View();
                    }
                    
                }
                else
                {
                    TempData["ErroMessage"] = "Password required";
                    return View();
                }


            }
            catch(Exception ex)
            {    
;               logger.Error("Exception ===> " + ex.Message);
                return View();
            }

        }
        
        public ActionResult Logout()
        {
            Session["user"] = null;
            return RedirectToAction("Login", "Auth");
        }
        
        public ActionResult Register()
        {
            return View();
        }  

        
        [HttpPost, ActionName("Register")]
        public ActionResult RegisterUser(UserEntity user)
        {

            try
            {
                bool registered;

                if (user.Password != null)
                {
                    if (user.Password == user.ConfirmPassword)
                    {
                        registered = _authBL.Register(user);

                        if (registered)
                        {
                            TempData["SuccessMessage"] = "User has been registered";
                            return RedirectToAction("Login", "Auth");
                        }
                        else
                        {
                            TempData["ErroMessage"] = "The email or phone already exist";
                            return View();
                        }
                    }
                    else
                    {
                        TempData["ErroMessage"] = "The Passwords don't match";
                        return View();
                    }
                }
                else
                {
                    TempData["ErroMessage"] = "The password is required";
                    return View();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Exception ===> " + ex.Message);
                return View();
            }           
                 
        }
    }
}