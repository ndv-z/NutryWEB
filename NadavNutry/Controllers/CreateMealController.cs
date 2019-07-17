using NadavNutry.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NadavNutry.Controllers
{
    public class CreateMealController : Controller
    {
        // GET: CreateMeal
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EditMeals()
        {
            return View();
        }


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

                foreach (Food f in query)
                {
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
        public String AddNewMeal(String mealData)
        {

            // 1. String back to JSON object
            JObject json_data = JObject.Parse(mealData);

            // 2. INTO MEAL OBJECT
            Meal meal = new Meal();
            meal.Name = (String)json_data["Name"];
            meal.Date = (String)json_data["Date"];
            meal.Calories = (int)json_data["Calories"];
            meal.Proteins = (int)json_data["Proteins"];
            meal.Carbs = (int)json_data["Carbs"];
            meal.Fats = (int)json_data["Fats"];

            // 3. CHECK IF DOSE NOT EXIST
            Boolean exist = false;
            using (DBModel db = new DBModel())
            {
                var query =
                from ml in db.Meal
                select ml;

                foreach (Meal m in query)
                {
                    if (m.Name == meal.Name && m.Date == meal.Date)
                        exist = true;
                }
            }

            // 4. IF DOES NOT, ADD IT AND RETURN 'no'
            if (!exist)
            {
                // ADD NEW FOOD TO DB TABLE 'FOOD'
                using (DBModel db = new DBModel())
                {
                    db.Meal.Add(meal);
                    db.SaveChanges();
                }
                return "added";
            }

            // 5. IF EXISTS, RETURN 'yes'
            return "yes";
        }

        [HttpPost]
        public String GetAllMeals()
        {
            String jsonAllMeals = "";

            // get all meals into JObject
            JArray allMeals = new JArray();
            using (DBModel db = new DBModel())
            {
                var query =
                from meal in db.Meal
                select meal;

                foreach (Meal m in query)
                {
                    JObject jMeal = new JObject();
                    jMeal.Add("Name", m.Name);
                    jMeal.Add("Date", m.Date);
                    jMeal.Add("Calories", m.Calories);
                    jMeal.Add("Proteins", m.Proteins);
                    jMeal.Add("Carbs", m.Carbs);
                    jMeal.Add("Fats", m.Fats);
                    allMeals.Add(jMeal);
                }
            }

            // to json string and back
            if (!allMeals.HasValues)
                return "";
            jsonAllMeals = allMeals.ToString(Formatting.None);
            return jsonAllMeals;
        }

        [HttpPost]
        public String Del_Meal(String name, String date)
        {
            Boolean deleted = false;
            using (DBModel db = new DBModel())
            {
                var query =
                from ml in db.Meal
                where ml.Name == name && ml.Date == date
                select ml;

                foreach (Meal m in query)
                {
                    db.Meal.Remove(m);
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

        // MORE HERE ..

    }
}