using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BonjourMVC.Controllers
{
    public class HelloController : Controller
    {

        private static Dictionary<string, string> Languages = new Dictionary<string, string>
        {
            {"en", "Hello {0}" },
            {"sp", "Hola {0}" },
            {"fr", "Salut {0}" },
            {"py", "print(\"Hello {0}\")" },
            {"c", "Console.WriteLine(\"Hello {0}\")" },
        };

        private static int counter = 0;

        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Index(string name, string lang)
        {
            string formattedString = "";

            if (!string.IsNullOrWhiteSpace(name))
            {
                if (Request.Cookies["counter"] == null)
                {

                    Response.Cookies.Append("counter", "2");
                    ViewBag.counter = ++counter;
                }
                else
                {
                    counter = int.Parse(Request.Cookies["counter"]);
                    Response.Cookies.Delete("counter");
                    Response.Cookies.Append("counter", (++counter).ToString());
                }

                if (Request.Cookies["counter"] != null)
                {
                    ViewBag.counter = Request.Cookies.Single(cookie => cookie.Key.Equals("counter")).Value;
                }
                formattedString = string.Format(Languages[lang], name);
                return View((object)formattedString);
            }

            return View((object)formattedString);
        }
    }
}
