using NadavNutry.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;


/*  LOGIN AND REGISTER CONTROLLER   */

namespace NadavNutry.Controllers
{
    public class LoginController : Controller
    {
        public static String curr_user = "";

        // GET: Login
        public ActionResult Index(){ return View(); }
        public ActionResult Register(){ return View(); }

        [HttpPost]
        public String HandleLoginForm (String loginData)
        {
            JObject user_json = JObject.Parse(loginData);
            Users loginUser = new Users();
            loginUser.Email = (String)user_json["email"];
            loginUser.Password = (String)user_json["pass"];

            using (DBModel db = new DBModel())
            {
                var query =
                from usr in db.Users
                select usr;

                foreach (Users u in query) {
                    if (u.Email == loginUser.Email && u.Password == loginUser.Password)
                    {
                        curr_user = u.Name + " ("+ u.Email +")";
                        // UPDATE LoggedIn TABLE with email of logged user
                        return "yes";
                    }
                        
                }
            }
            return "no";
        }

        public String GetCurrentUser ()
        {
            return curr_user;
        }

        [HttpPost]
        public String HandleRegisterForm(String str)
        {

            // String back to JSON object
            JObject json_data = JObject.Parse(str);

            // INTO USER OBJECT
            Users user = new Users();
            user.Name = (String)json_data["name"];
            user.Email = (String)json_data["email"];
            user.Password = (String)json_data["pass"];

            // CHECK IF DOSE NOT EXIST
            Boolean exist = false;
            using (DBModel db = new DBModel())
            {
                var query =
                from usr in db.Users
                select usr;

                foreach (Users u in query)
                {
                    if (u.Email == user.Email)
                        exist = true;
                }
            }
            Debug.WriteLine("name: "+ user.Name+", EMAIL: "+ user.Email+", PASS: "+ user.Password);
            // IF DOEST NOT EXIST
            if (!exist)
            {
                // ADD NEW USER TO DB TABLE 'USERS'
                using (DBModel db = new DBModel())
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                }
                return "added";
            }

            //// RETURN JSON OBJECT AS STRING
            //JObject ret_json = new JObject();
            //ret_json["name"] = "nadav";
            //ret_json["age"] = "32";

            //String ret = ret_json.ToString(Formatting.None);

            return "exist";
        }

    }
}