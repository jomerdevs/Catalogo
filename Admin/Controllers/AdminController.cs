using Admin.Permissions;
using Admin.Utils;
using DataBusiness;
using DataEntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Controllers
{
    [ValidateSession]
    public class AdminController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        AdminBL _userBL = new AdminBL();      
        
        public ActionResult Index(string search)
        {
            var usersList = _userBL.GetAll(search);
            
            if (usersList.Count == 0)
            {
                TempData["InfoMessage"] = "No users to show";
            }
            return View(usersList);
        }
        
        public ActionResult Details(int id)
        {
            try
            {
                var user = _userBL.GetUserById(id).FirstOrDefault();

                if (user == null)
                {
                    TempData["InfoMessage"] = "User not available";
                    return RedirectToAction("Index");
                }
                return View(user);
            }
            catch (Exception ex)
            {
                log.Info(ex.Message);
                return View();
            }
        }
        
        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost, ActionName("Create")]
        public ActionResult CreateUser(UserAuxEntity user)
        {
            user.Password = PasswordHash.HashSha256(user.Password);
            bool IsCreated = false;
            try
            {
                if (ModelState.IsValid)
                {
                    IsCreated = _userBL.CreateUser(user);

                    if (IsCreated)
                    {
                        TempData["SuccessMessage"] = "User has been created succesfully!";
                    }
                    else
                    {
                        TempData["ErroMessage"] = "Unable to create user, possibly this user already exist";
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                log.Info(ex.Message);
                return View();
            }
        }
        
        public ActionResult Edit(int id)
        {
            var user = _userBL.GetUserById(id).FirstOrDefault();

            if (user == null)
            {
                TempData["InfoMessage"] = "User not available";
                return RedirectToAction("Index");
            }

            return View(user);
        }
        
        [HttpPost, ActionName("Edit")]
        public ActionResult UpdateUser(UserAuxEntity user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var samePwd = user.Password.Equals(_userBL.PasswordToCompare());
                    
                    if (samePwd)
                    {
                        bool IsUpdated = _userBL.UpdateUser(user);

                        if (IsUpdated)
                        {
                            TempData["SuccessMessage"] = "User has been updated succesfully!";
                        }
                        else
                        {
                            TempData["ErroMessage"] = "Unable to update the user";
                        }
                    }
                    else
                    {
                        
                        user.Password = PasswordHash.HashSha256(user.Password);

                        bool IsUpdated = _userBL.UpdateUser(user);

                        if (IsUpdated)
                        {
                            TempData["SuccessMessage"] = "User has been updated succesfully!";
                        }
                        else
                        {
                            TempData["ErroMessage"] = "Unable to update the user";
                        }

                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                log.Info(ex.Message);
                return View();

            }
        }
        
        public ActionResult Delete(int id)
        {
            try
            {
                var user = _userBL.GetUserById(id).FirstOrDefault();

                if (user == null)
                {
                    TempData["InfoMessage"] = "User not available";
                    return RedirectToAction("Index");
                }
                return View(user);
            }
            catch (Exception ex)
            {
                log.Info(ex.Message);
                return View();
            }
        }
        
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteUser(int id)
        {
            try
            {
                string result = _userBL.Delete(id);

                if (result.Contains("deleted"))
                {
                    TempData["SuccessMessage"] = result;
                }
                else
                {
                    TempData["ErroMessage"] = result;
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                log.Info(ex.Message);
                return View();
            }
        }
    }
}
