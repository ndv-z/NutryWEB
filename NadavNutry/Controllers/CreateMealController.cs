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
    }
}