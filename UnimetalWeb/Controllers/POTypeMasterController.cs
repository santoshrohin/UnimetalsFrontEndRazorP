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
    public class POTypeMasterController : Controller
    {
        private IConfiguration _configuration;
        private string _baseURL;
        public AuthResponse _authResponse;

        public POTypeMasterController(IConfiguration configuration)
        {
            _configuration = configuration;
            _baseURL = _configuration.GetSection("baseULR").Value;

        }
        public async Task<IActionResult> Index()
        {
            CancellationToken cancellationToken = new CancellationToken();
            POTypeMasterResponse pOTypeMasterResponse = new POTypeMasterResponse();

            using (HttpClientHandler handler = new HttpClientHandler())
            {
                using (var httpClient = new HttpClient(handler))
                {
                    _authResponse = HttpContext.Session.GetObjectFromJson<AuthResponse>("loginDetails");
                    httpClient.Timeout = TimeSpan.FromMinutes(10);
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authResponse.result.Token);
                    using (var response = await httpClient.GetAsync(_baseURL + "api/POTypeMaster", cancellationToken))
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        pOTypeMasterResponse = JsonConvert.DeserializeObject<POTypeMasterResponse>(apiResponse);
                    }
                }
            }

            return View(pOTypeMasterResponse.result);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            CancellationToken cancellationToken = new CancellationToken();
            POTypeMasterResponseGetAll pOTypeMasterResponse = new POTypeMasterResponseGetAll();
            POTypeMaster productlinemaster = new POTypeMaster();

            using (var httpClient = new HttpClient())
            {

                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = await httpClient.GetAsync(_baseURL + "api/POTypeMaster/" + id, cancellationToken))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    pOTypeMasterResponse = JsonConvert.DeserializeObject<POTypeMasterResponseGetAll>(apiResponse);
                    productlinemaster = pOTypeMasterResponse.result;

                }
            }
            return View(productlinemaster);
        }
        public ActionResult Create()
        {
            return View("Edit");
        }
        [HttpPost]
        public async Task<ActionResult> Create(POTypeMaster productlinemaster)
        {
            _authResponse = HttpContext.Session.GetObjectFromJson<AuthResponse>("loginDetails");
            productlinemaster.Id = 0;
            productlinemaster.CompanyId = _authResponse.result.CompanyId; ;
            productlinemaster.CompanyCode = _authResponse.result.Id;
            productlinemaster.LastUpdatedDateandtime = CommonClasses.GetCurrentTime();
            productlinemaster.UserName = _authResponse.result.Username;
            productlinemaster.IsModify = false;
            productlinemaster.IsDelete = false;
            POTypeMasterResponseGetAll pOTypeMasterResponse = new POTypeMasterResponseGetAll();
            CancellationToken cancellationToken = new CancellationToken();
            string data = JsonConvert.SerializeObject(productlinemaster);
            StringContent stringContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = httpClient.PostAsync(_baseURL + "api/POTypeMaster/", stringContent).Result)
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    pOTypeMasterResponse = JsonConvert.DeserializeObject<POTypeMasterResponseGetAll>(apiResponse);
                    if (pOTypeMasterResponse.IsError == false)
                    {
                        TempData["SuccessMessage"] = pOTypeMasterResponse.Message;

                        //TempData["InfoMessage"] = string.Format("Hello {0}.\\nCurrent Date and Time: {1}", "Anshu", DateTime.Now.ToString());
                    }
                    else
                    {
                        TempData["ErrorMessage"] = pOTypeMasterResponse.Message;

                    }
                    if (pOTypeMasterResponse.IsError == false)
                    {

                        return RedirectToAction("Index");
                    }
                }
            }
            return View("Edit");
        }
        [HttpPost]
        public async Task<ActionResult> Edit(POTypeMaster productlinemaster)
        {

            _authResponse = HttpContext.Session.GetObjectFromJson<AuthResponse>("loginDetails");
            productlinemaster.CompanyId = _authResponse.result.CompanyId; ;
            productlinemaster.CompanyCode = _authResponse.result.Id;
            productlinemaster.LastUpdatedDateandtime = CommonClasses.GetCurrentTime();
            productlinemaster.UserName = _authResponse.result.Username;
            productlinemaster.IsModify = false;
            productlinemaster.IsDelete = false;

            POTypeMasterResponseGetAll pOTypeMasterResponse = new POTypeMasterResponseGetAll();
            CancellationToken cancellationToken = new CancellationToken();
            string data = JsonConvert.SerializeObject(productlinemaster);
            StringContent stringContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = httpClient.PutAsync(_baseURL + "api/POTypeMaster/", stringContent).Result)
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    pOTypeMasterResponse = JsonConvert.DeserializeObject<POTypeMasterResponseGetAll>(apiResponse);
                    if (pOTypeMasterResponse.IsError == false)
                    {
                        TempData["SuccessMessage"] = pOTypeMasterResponse.Message;

                        //TempData["InfoMessage"] = string.Format("Hello {0}.\\nCurrent Date and Time: {1}", "Anshu", DateTime.Now.ToString());
                    }
                    else
                    {
                        TempData["ErrorMessage"] = pOTypeMasterResponse.Message;

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
        public ActionResult Delete(int id)
        {

            CancellationToken cancellationToken = new CancellationToken();

            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = httpClient.GetAsync(_baseURL + "api/POTypeMaster/" + id).Result)
                {
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
            POTypeMasterResponseGetAll pOTypeMasterResponse = new POTypeMasterResponseGetAll();

            using (var httpClient = new HttpClient())
            {

                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = await httpClient.GetAsync(_baseURL + "api/POTypeMaster/" + id, cancellationToken))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    pOTypeMasterResponse = JsonConvert.DeserializeObject<POTypeMasterResponseGetAll>(apiResponse);
                }
            }
            return View(pOTypeMasterResponse.result);
        }

    }
    
}
