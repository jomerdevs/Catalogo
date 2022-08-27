using DataBusiness;
using Admin;
using DataEntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Catalogo.Controllers
{
    public class AuthController : Controller
    {        
        AuthBL _authBL = new AuthBL();

        // LOGIN METHOD: GET
        public ActionResult Login()
        {
            return View();
        }

        // LOGIN METHOD: POST
        [HttpPost, ActionName("Login")]
        public ActionResult LoginUser(UserEntity user)
        {
            try
            {

                var login = _authBL.Login(user);

                if (login == "found")
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
                else if (login == "notFound")
                {
                    TempData["ErroMessage"] = "User not found";
                    return View();
                }
                else if (login == "noPassword")
                {
                    TempData["ErroMessage"] = "Password Required";
                    return View();
                }
                else
                {
                    TempData["ErroMessage"] = "Something went wrong :(";
                    return View();
                }                

            }
            catch (Exception)
            {
                TempData["ErroMessage"] = "User not found..!!";
                return View();
            }
                                
        }

        // ===== LOGOUT =====
        public ActionResult Logout()
        {
            Session["user"] = null;
            return RedirectToAction("Login", "Auth");
        }

        // REGISTER USER METHOD: GET
        public ActionResult Register()
        {
            return View();
        }        

        // REGISTER USER METHOD; POST
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
                TempData["ErroMessage"] = ex.Message;
                return View();
            }           
                 
        }
    }
}