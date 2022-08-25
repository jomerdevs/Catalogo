using Catalogo.DataLayer;
using Catalogo.Models;
using Catalogo.Permissions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Catalogo.Controllers
{
    [ValidateSession]
    public class AdminController : Controller
    {
        AdminDAL _userDAL = new AdminDAL();
        Users userEx = new Users();
        // GET ALL USERS
        public ActionResult Index()
        {
            var usersList = _userDAL.GetAll();

            if (usersList.Count == 0)
            {
                TempData["InfoMessage"] = "No products to show";
            }
            return View(usersList);
        }       

        // DETAILS
        public ActionResult Details(int id)
        {
            try
            {
                var user = _userDAL.GetUserById(id).FirstOrDefault();

                if (user == null)
                {
                    TempData["InfoMessage"] = "Category not available";
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
        public ActionResult CreateUser(UserAux user)
        {
            user.Password = HashSha256(user.Password);
            bool IsCreated = false;
            try
            {
                if (ModelState.IsValid)
                {
                    IsCreated = _userDAL.CreateUser(user);

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
            var user = _userDAL.GetUserById(id).FirstOrDefault();

            if (user == null)
            {
                TempData["InfoMessage"] = "User not available";
                return RedirectToAction("Index");
            }
            
            return View(user);
        }

        // ======== CREATE METHOD FOR HASH THE PASSWORD ===========
        public static string HashSha256(string password)
        {
            StringBuilder sb = new StringBuilder();
            using (SHA256 hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(password));

                foreach (byte b in result)
                    sb.Append(b.ToString("x2"));
            }

            return sb.ToString();

        }

        // UPDATE USER METHOD: POST
        [HttpPost, ActionName("Edit")]
        public ActionResult UpdateUser(UserAux user)
        {
            try
            {
                if (ModelState.IsValid)
                {                    
                    var samePwd = user.Password.Equals(userEx.Password);
                    
                    // IF THERE IS NO NEW PASSWORD WE DONT HASH THE PASSWORD
                    if (samePwd)
                    {                        
                        bool IsUpdated = _userDAL.UpdateUser(user);

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
                        // HASHING INSERTED PASSWORD WHIT THE METHOD I CREATED BEFORE UPDATE
                        user.Password = HashSha256(user.Password);                 
                        
                        bool IsUpdated = _userDAL.UpdateUser(user);

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
                var user = _userDAL.GetUserById(id).FirstOrDefault();

                if (user == null)
                {
                    TempData["InfoMessage"] = "Product not available";
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
                string result = _userDAL.DeleteUser(id);

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
