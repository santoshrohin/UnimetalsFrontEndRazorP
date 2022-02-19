using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UnimetalWeb.Models;
using UnimetalWeb.Helpers;
using Microsoft.Extensions.Configuration;
using System.Threading;

namespace UnimetalWeb.Controllers
{
    public class LoginController : Controller
    {
        private IConfiguration _configuration;
        private string _baseURL;
        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
            _baseURL = _configuration.GetSection("baseULR").Value;
        }
        public async Task<IActionResult> Index()
        {
            CancellationToken cancellationToken = new CancellationToken();
            SelectListItem selectListItem = new SelectListItem();

            //string FieldNames = "OpeningDate as Value,	ClosingDate as Text  ";
            string FieldNames = "id as Value,ClosingDate as Text";
            string TableNames = "CompanyMaster";
            string WhereCondition = "isdelete=0";
            string DropDownId = "dd";


            using (var httpClient = new HttpClient())
            {
                //http://localhost:44350/api/CompanyMaster
                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = await httpClient.GetAsync("https://localhost:44350/api/CommonHelper/" + FieldNames+"/" + TableNames + "/" + WhereCondition + "/" + DropDownId + "/", cancellationToken))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    selectListItem = JsonConvert.DeserializeObject<SelectListItem>(apiResponse);
                    ViewBag.selectListItem = selectListItem.result;
                }
            }
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(AuthRequest authRequest)
        {
            if (ModelState.IsValid)
            {
                AuthResponse authResponse = new AuthResponse();
                string data = JsonConvert.SerializeObject(authRequest);
                StringContent stringContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");

                using (var httpClient = new HttpClient())
                {
                    httpClient.Timeout = TimeSpan.FromMinutes(10);

                    using (var response = httpClient.PostAsync(_baseURL+"api/Auth/Login/", stringContent).Result)
                    {
                        var apiResponse =await response.Content.ReadAsStringAsync();
                        authResponse = JsonConvert.DeserializeObject<AuthResponse>(apiResponse);
                        if (response.IsSuccessStatusCode)
                        {
                            HttpContext.Session.SetCustomString("SessionValid", "Yes");
                            HttpContext.Session.SetObjectAsJson("loginDetails", authResponse);
                            return RedirectToAction("Index","Dashboard");
                        }
                    }
                }
            }
            CancellationToken cancellationToken = new CancellationToken();
            SelectListItem selectListItem = new SelectListItem();

            //string FieldNames = "OpeningDate as Value,	ClosingDate as Text  ";
            string FieldNames = "id as Value,ClosingDate as Text";
            string TableNames = "CompanyMaster";
            string WhereCondition = "isdelete=0";
            string DropDownId = "dd";


            using (var httpClient = new HttpClient())
            {
                //http://localhost:44350/api/CompanyMaster
                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = await httpClient.GetAsync("https://localhost:44350/api/CommonHelper/" + FieldNames + "/" + TableNames + "/" + WhereCondition + "/" + DropDownId + "/", cancellationToken))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    selectListItem = JsonConvert.DeserializeObject<SelectListItem>(apiResponse);
                    ViewBag.selectListItem = selectListItem.result;
                }
            }
            return View("Index");
            
            
            
        }
    }
}
