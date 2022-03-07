using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using UnimetalWeb.Helpers;
using UnimetalWeb.Models;
using Microsoft.Extensions.Configuration;

namespace UnimetalWeb.Controllers
{
    public class ProductlinemasterController : Controller
    {
        private IConfiguration _configuration;
        private string _baseURL;
        public AuthResponse _authResponse;

        public ProductlinemasterController(IConfiguration configuration)
        {
            _configuration = configuration;
            _baseURL = _configuration.GetSection("baseULR").Value;

        }
        public async Task<IActionResult> Index()
        {
            CancellationToken cancellationToken = new CancellationToken();
            ProductlinemasterResponse productlinemasterResponse = new ProductlinemasterResponse();

            using (HttpClientHandler handler = new HttpClientHandler())
            {
                using (var httpClient = new HttpClient(handler))
                {
                    _authResponse = HttpContext.Session.GetObjectFromJson<AuthResponse>("loginDetails");
                    httpClient.Timeout = TimeSpan.FromMinutes(10);
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authResponse.result.Token);
                    using (var response = await httpClient.GetAsync(_baseURL + "api/Productlinemaster", cancellationToken))
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        productlinemasterResponse = JsonConvert.DeserializeObject<ProductlinemasterResponse>(apiResponse);
                    }
                }
            }

            return View(productlinemasterResponse.result);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            CancellationToken cancellationToken = new CancellationToken();
            ProductlinemasterGetAll productlinemasterResponse = new ProductlinemasterGetAll();
            Productlinemaster productlinemaster = new Productlinemaster();

            using (var httpClient = new HttpClient())
            {

                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = await httpClient.GetAsync(_baseURL + "api/Productlinemaster/" + id, cancellationToken))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    productlinemasterResponse = JsonConvert.DeserializeObject<ProductlinemasterGetAll>(apiResponse);
                    productlinemaster = productlinemasterResponse.result;

                }
            }
            return View(productlinemaster);
        }
        public ActionResult Create()
        {
            return View("Edit");
        }
        [HttpPost]
        public async Task<ActionResult> Create(Productlinemaster productlinemaster)
        {
            _authResponse = HttpContext.Session.GetObjectFromJson<AuthResponse>("loginDetails");
            productlinemaster.Id = 0;
            productlinemaster.CompanyId = _authResponse.result.CompanyId; ;
            productlinemaster.CompanyCode = _authResponse.result.Id;
            productlinemaster.LastUpdatedDateandtime = CommonClasses.GetCurrentTime();
            productlinemaster.UserName = _authResponse.result.Username;
            productlinemaster.IsModify = false;
            productlinemaster.IsDelete = false;
            ProductlinemasterGetAll productlinemasterResponse = new ProductlinemasterGetAll();
            CancellationToken cancellationToken = new CancellationToken();
            string data = JsonConvert.SerializeObject(productlinemaster);
            StringContent stringContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = httpClient.PostAsync(_baseURL + "api/Productlinemaster/", stringContent).Result)
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    productlinemasterResponse = JsonConvert.DeserializeObject<ProductlinemasterGetAll>(apiResponse);
                    if (productlinemasterResponse.IsError == false)
                    {
                        TempData["SuccessMessage"] = productlinemasterResponse.Message;

                        //TempData["InfoMessage"] = string.Format("Hello {0}.\\nCurrent Date and Time: {1}", "Anshu", DateTime.Now.ToString());
                    }
                    else
                    {
                        TempData["ErrorMessage"] = productlinemasterResponse.Message;

                    }
                    if (productlinemasterResponse.IsError == false)
                    {

                        return RedirectToAction("Index");
                    }
                }
            }
            return View("Edit");
        }
        [HttpPost]
        public async Task<ActionResult> Edit(Productlinemaster productlinemaster)
        {

            _authResponse = HttpContext.Session.GetObjectFromJson<AuthResponse>("loginDetails");
            productlinemaster.CompanyId = _authResponse.result.CompanyId; ;
            productlinemaster.CompanyCode = _authResponse.result.Id;
            productlinemaster.LastUpdatedDateandtime = CommonClasses.GetCurrentTime();
            productlinemaster.UserName = _authResponse.result.Username;
            productlinemaster.IsModify = false;
            productlinemaster.IsDelete = false;

            ProductlinemasterGetAll productlinemasterResponse = new ProductlinemasterGetAll();
            CancellationToken cancellationToken = new CancellationToken();
            string data = JsonConvert.SerializeObject(productlinemaster);
            StringContent stringContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = httpClient.PutAsync(_baseURL + "api/Productlinemaster/", stringContent).Result)
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    productlinemasterResponse = JsonConvert.DeserializeObject<ProductlinemasterGetAll>(apiResponse);
                    if (productlinemasterResponse.IsError == false)
                    {
                        TempData["SuccessMessage"] = productlinemasterResponse.Message;

                        //TempData["InfoMessage"] = string.Format("Hello {0}.\\nCurrent Date and Time: {1}", "Anshu", DateTime.Now.ToString());
                    }
                    else
                    {
                        TempData["ErrorMessage"] = productlinemasterResponse.Message;

                    }
                    if (productlinemasterResponse.Message == "Duplicate Record")
                    {
                        return RedirectToAction("Edit", productlinemaster.Id);
                    }
                    if (response.IsSuccessStatusCode)
                    {

                        return RedirectToAction("Index");
                    }



                }
            }
            return View("Edit", productlinemaster);
        }
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {

            CancellationToken cancellationToken = new CancellationToken();
            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = httpClient.GetAsync(_baseURL + "api/Productlinemaster/Delete?id=" + id).Result)
                {
                    StatusCommonResponse statusCommonResponse = new StatusCommonResponse();
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    statusCommonResponse = JsonConvert.DeserializeObject<StatusCommonResponse>(apiResponse);
                    if (statusCommonResponse.IsError == false)
                    {
                        TempData["SuccessMessage"] = statusCommonResponse.Message;

                        //TempData["InfoMessage"] = string.Format("Hello {0}.\\nCurrent Date and Time: {1}", "Anshu", DateTime.Now.ToString());
                    }
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }

            return View("Index");
        }
        
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            CancellationToken cancellationToken = new CancellationToken();
            ProductlinemasterGetAll productlinemasterResponse = new ProductlinemasterGetAll();

            using (var httpClient = new HttpClient())
            {

                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = await httpClient.GetAsync(_baseURL + "api/Productlinemaster/" + id, cancellationToken))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    productlinemasterResponse = JsonConvert.DeserializeObject<ProductlinemasterGetAll>(apiResponse);
                }
            }
            return View(productlinemasterResponse.result);
        }

    }
    
}
