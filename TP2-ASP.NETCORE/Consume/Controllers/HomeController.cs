using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Consume.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;

namespace Consume.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        /*public IActionResult Login([Bind("Email,Password")] Users user)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:49675");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var tokenJwt = client.PostAsJsonAsync("/Home", user).Result;
            var token = tokenJwt.Content.ReadAsStringAsync().Result;
            if (token != null)
            {
                CookieOptions option = new CookieOptions();
                option.HttpOnly = true;
                Response.Cookies.Append("jwttoken", token);
                ViewBag.token = token;
                return View();
            }
            return View("Index");
        }
        public IActionResult Getdetails()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:49675");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("/Home").Result;
            var us = response.Content.ReadAsAsync<Users>().Result;
            ViewBag.token = this.ControllerContext.HttpContext.Request.Cookies["jwttoken"];
            ViewBag.nom = response;
            return View("Login",us);
        }*/
        public IActionResult Login([Bind("Email,Password")] Users user)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:49675");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var tokenJwt = client.PostAsJsonAsync("/Home", user).Result;
            var token = tokenJwt.Content.ReadAsAsync<string>().Result;
            if (token != null)
            {
                System.Diagnostics.Debug.WriteLine(token);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
                System.Diagnostics.Debug.WriteLine(client.DefaultRequestHeaders.Authorization.ToString());
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("/Home").Result;
                System.Diagnostics.Debug.WriteLine(response);
               var us= response.Content.ReadAsAsync<Users>().Result;
               
                ViewBag.token = token;
                ViewBag.nom = response;
                return View(us);
            }
            return View("Index");
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
