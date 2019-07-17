using NadavNutry.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NadavNutry.Controllers
{
    public class FoodController : Controller {

        // GET: Food
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewAllFoods()
        {
            // fpr faster loading of items in table view
            DBModel DataBase = new DBModel();
            IEnumerable<Food> food_list = DataBase.Food.ToList<Food>();

            return View(food_list);
        }

        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            //Food food = new Food();
            return View();
        }
        //[HttpPost]
        //public ActionResult AddOrEdit(Food food)
        //{

        //    if (food.ImageUpload != null)
        //    {
        //        string fileName = Path.GetFileNameWithoutExtension(food.ImageUpload.FileName);
        //        string extension = Path.GetExtension(food.ImageUpload.FileName);
        //        fileName += fileName + DateTime.Now.ToString("yymmssfff") + extension;
        //        food.ImagePath = "~/AppFiles/Images/" + fileName;
        //        food.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/AppFiles/Images/"), fileName));
        //    }

        //    using (DBModel db = new DBModel())
        //    {
        //        db.Food.Add(food);
        //        db.SaveChanges();
        //    }

        //    return RedirectToAction("ViewAllFoods", "Food");

        //}


        [HttpPost]
        public String GetAllFoods()
        {
            String jsonAllFoods = "";

            // get all foods into JObject
            JArray allFoods = new JArray();
            using (DBModel db = new DBModel())
            {
                var query =
                from food in db.Food
                select food;

                foreach (Food f in query) {
                    JObject jfood = new JObject();
                    jfood.Add("Name", f.Name);
                    jfood.Add("Units", f.Units);
                    jfood.Add("Calories", f.Calories);
                    jfood.Add("Proteins", f.Proteins);
                    jfood.Add("Carbs", f.Carbs);
                    jfood.Add("Fats", f.Fats);
                    allFoods.Add(jfood);
                }
            }

            // to json string and back
            if (!allFoods.HasValues)
                return "";
            jsonAllFoods = allFoods.ToString(Formatting.None);
            return jsonAllFoods;
        }




        [HttpPost]
        public String AddNewFood(String foodData)
        {

            //// String back to JSON object
            JObject json_data = JObject.Parse(foodData);

            //// INTO FOOD OBJECT
            Food food = new Food();
            food.Name = (String)json_data["Name"];
            food.Units = (String)json_data["Units"];
            food.Calories = (int)json_data["Calories"];
            food.Proteins = (int)json_data["Proteins"];
            food.Carbs = (int)json_data["Carbs"];
            food.Fats = (int)json_data["Fats"];

            //// CHECK IF DOSE NOT EXIST
            Boolean exist = false;
            using (DBModel db = new DBModel())
            {
                var query =
                from fd in db.Food
                select fd;

                foreach (Food f in query)
                {
                    if (f.Name == food.Name && f.Units == food.Units)
                        exist = true;
                }
            }
            //Debug.WriteLine("name: " + user.Name + ", EMAIL: " + user.Email + ", PASS: " + user.Password);

            // IF DOEST NOT EXIST
            if (!exist)
            {
                // ADD NEW FOOD TO DB TABLE 'FOOD'
                using (DBModel db = new DBModel())
                {
                    db.Food.Add(food);
                    db.SaveChanges();
                }
                return "added";
            }

            return "yes";
        }


        [HttpPost]
        public String Edit_Food(String foodData)
        {

            //// String back to JSON object
            JObject json_data = JObject.Parse(foodData);

            //// INTO FOOD OBJECT
            //Users user = new Users();
            Food food = new Food();
            food.Name = (String)json_data["Name"];
            food.Units = (String)json_data["Units"];
            food.Calories = (int)json_data["Calories"];
            food.Proteins = (int)json_data["Proteins"];
            food.Carbs = (int)json_data["Carbs"];
            food.Fats = (int)json_data["Fats"];

            //// CHECK IF DOSE NOT EXIST
            Boolean exist = false;
            Food to_edit = new Food();
            using (DBModel db = new DBModel())
            {
                var query =
                from fd in db.Food
                where fd.Name == (String)food.Name && (String)fd.Units == food.Units
                select fd;

                foreach (Food f in query)
                {
                    if (f.Name == food.Name && f.Units == food.Units) {
                        to_edit = f;
                        exist = true;
                    }
                }

                if (exist)
                {
                    // TODO: replace food
                    db.Food.Remove(to_edit);
                    db.Food.Add(food);
                    db.SaveChanges();
                    return "yes";
                }

            }



            return "no";
        }

        [HttpPost]
        public String Del_Food(String name, String units)
        {
            Boolean deleted = false;
            using (DBModel db = new DBModel())
            {
                var query =
                from fd in db.Food
                where fd.Name == name && fd.Units == units
                select fd;

                foreach (Food f in query)
                {
                    db.Food.Remove(f);
                    deleted = true;
                }

                if (deleted)
                {
                    db.SaveChanges();
                    return "yes";
                }

            }

            return "no";
        }


    }
}