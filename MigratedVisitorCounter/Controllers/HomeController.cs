using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MigratedVisitorCounter.Models;
using Microsoft.AspNetCore.Http;

namespace MigratedVisitorCounter.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult GetMessage()
        {
            HttpClient client = new HttpClient();
            String response = client.GetStringAsync("https://az3tnb2cq2.execute-api.us-east-1.amazonaws.com/Prod/api/values").Result;
            ViewData["LuckyNumbers"] = response;

            var views = (HttpContext.Session.GetInt32("ViewCount") ?? 0) + 1;
            HttpContext.Session.SetInt32("ViewCount", views);
            ViewData["Message"] = string.Format("You visited {0} times", views);

            String message = "Your connection token for today is:<b> " + response.Replace(" ", "") + " </b>and you have retrieved a token <b>" + views + "</b> time(s) during this session";
            return new JsonResult (message);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
