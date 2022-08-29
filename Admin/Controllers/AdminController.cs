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
        AdminBL _userBL = new AdminBL();
        UserEntity userEx = new UserEntity();

        // GET ALL USERS
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

        // DETAILS
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
                TempData["ErroMessage"] = ex.Message;
                return View();
            }
        }

        // CREATE USER BY ADMIN METHOD: GET
        public ActionResult Create()
        {
            return View();
        }

        // CREATE USER BY ADMIN METHOD: POST
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
                TempData["ErroMessage"] = ex.Message;
                return View();
            }
        }

        // UPDATE USER METHOD: GET
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

        // UPDATE USER METHOD: POST
        [HttpPost, ActionName("Edit")]
        public ActionResult UpdateUser(UserAuxEntity user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var samePwd = user.Password.Equals(userEx.Password);

                    // IF THERE IS NO NEW PASSWORD WE DONT HASH THE PASSWORD, SIMPLY UPDATE THE NEW FIELDS
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
                        // HASHING INSERTED PASSWORD IF NEW PASSWORD IS INSERTED
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
                TempData["ErroMessage"] = ex.Message;
                return View();

            }
        }

        // DELETE USER METHOD: GET
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
                TempData["ErroMessage"] = ex.Message;
                return View();
            }
        }

        // DELETE USER METHOD: POST
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
                TempData["ErroMessage"] = ex.Message;
                return View();
            }
        }
    }
}
