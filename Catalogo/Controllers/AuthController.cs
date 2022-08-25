using Catalogo.Models;
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
    public class AuthController : Controller
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["connection"].ToString();
        // GET: Auth
        public ActionResult Login()
        {
            return View();
        }

        // LOGIN METHOD: POST
        [HttpPost, ActionName("Login")]
        public ActionResult LoginUser(Users user)
        {
            user.Password = HashSha256(user.Password);
            Users auxUser = null;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("validateUser", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;                        
                        cmd.Parameters.AddWithValue("@email", user.Email);                        
                        cmd.Parameters.AddWithValue("@password", user.Password);
                        
                        // Open DB Connection
                        connection.Open();                       

                        user.Id = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader != null)
                        {
                            auxUser = new Users();
                            int propIsAdmin = reader.GetOrdinal("IsAdmin");

                            while (reader.Read())
                            {                                 
                                user.IsAdmin = reader.IsDBNull(propIsAdmin) ? false : reader.GetBoolean(propIsAdmin);                                
                            }
                        }

                        connection.Close();

                    }

                    // CHECK IF USER EXISTS
                    if (user.Id != 0)
                    {
                        /* CHECK IF ISADMIN, ADMIN GOING TO SHOW THE ADMIN VIEWS WITH ALL ACTIONS BUTTONS,
                           IF NOT ADMIN GOING TO SHOW ONLY TABLES WITHOUT ACTIONS */
                        if (user.IsAdmin)
                        {
                            Session["userAdmin"] = user;
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
                        ViewData["message"] = "User not found";
                        return View();
                    }
                    

                }
                catch (Exception)
                {
                    connection.Close();
                    return View();
                }

            }            
        }

        // REGISTER USER METHOD: GET
        public ActionResult Register()
        {
            return View();
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

        // REGISTER USER METHOD; POST
        [HttpPost, ActionName("Register")]
        public ActionResult RegisterUser(Users user)
        {            
            bool registered;
            string message;

            if (user.Password == user.ConfirmPassword)
            {
                user.Password = HashSha256(user.Password);
            }
            else
            {
                ViewData["message"] = "The Passwords don't match";
            }
            

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("registerUser", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@firstName", user.FirstName);
                        cmd.Parameters.AddWithValue("@lastName", user.LastName);
                        cmd.Parameters.AddWithValue("@email", user.Email);
                        cmd.Parameters.AddWithValue("@phone", user.Phone);
                        cmd.Parameters.AddWithValue("@password", user.Password);
                        cmd.Parameters.AddWithValue("@isAdmin", user.IsAdmin);
                        cmd.Parameters.Add("@registered", SqlDbType.Bit).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@message", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;

                        // Open DB Connection
                        connection.Open();
                        cmd.ExecuteNonQuery(); // Returns 1 for success and 0 when fail

                        registered = Convert.ToBoolean(cmd.Parameters["registered"].Value);
                        message = cmd.Parameters["message"].Value.ToString();
                        connection.Close();
                    }
                    ViewData["message"] = message;

                    if (registered)
                    {
                        return RedirectToAction("Login", "Auth");
                    }
                    else
                    {
                        return View();
                    }

                }
                catch (Exception)
                {                    
                    connection.Close();
                    return View();
                }

            }
        }
    }
}