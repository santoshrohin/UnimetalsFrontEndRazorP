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
    public class TrayTypeMasterController : Controller
    {
        private IConfiguration _configuration;
        private string _baseURL;
        public AuthResponse _authResponse;

        public TrayTypeMasterController(IConfiguration configuration)
        {
            _configuration = configuration;
            _baseURL = _configuration.GetSection("baseULR").Value;

        }
        public async Task<IActionResult> Index()
        {
            CancellationToken cancellationToken = new CancellationToken();
            TraytypemasterResponse traytypemasterResponse = new TraytypemasterResponse();

            using (HttpClientHandler handler = new HttpClientHandler())
            {
                using (var httpClient = new HttpClient(handler))
                {
                    _authResponse = HttpContext.Session.GetObjectFromJson<AuthResponse>("loginDetails");
                    httpClient.Timeout = TimeSpan.FromMinutes(10);
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authResponse.result.Token);
                    using (var response = await httpClient.GetAsync(_baseURL + "api/TrayTypeMaster", cancellationToken))
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        traytypemasterResponse = JsonConvert.DeserializeObject<TraytypemasterResponse>(apiResponse);
                    }
                }
            }

            return View(traytypemasterResponse.result);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            CancellationToken cancellationToken = new CancellationToken();
            TraytypemasterGetAll traytypemasterResponse = new TraytypemasterGetAll();
            Traytypemaster traytypemaster = new Traytypemaster();

            using (var httpClient = new HttpClient())
            {
                
                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = await httpClient.GetAsync(_baseURL + "api/TrayTypeMaster/" + id, cancellationToken))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    traytypemasterResponse = JsonConvert.DeserializeObject<TraytypemasterGetAll>(apiResponse);
                    traytypemaster = traytypemasterResponse.result;

                }
            }
            return View(traytypemaster);
        }
        public ActionResult Create()
        {
            return View("Edit");
        }
        [HttpPost]
        public async Task<ActionResult> Create(Traytypemaster traytypemaster)
        {
            _authResponse = HttpContext.Session.GetObjectFromJson<AuthResponse>("loginDetails");
            traytypemaster.Id = 0;
            traytypemaster.CompanyId = _authResponse.result.CompanyId; ;
            traytypemaster.CompanyCode = _authResponse.result.Id;
            traytypemaster.LastUpdatedDateandtime = CommonClasses.GetCurrentTime();
            traytypemaster.UserName = _authResponse.result.Username;
            traytypemaster.IsModify = false;
            traytypemaster.IsDelete = false;
            TraytypemasterGetAll traytypemasterResponse = new TraytypemasterGetAll();
            CancellationToken cancellationToken = new CancellationToken();
            string data = JsonConvert.SerializeObject(traytypemaster);
            StringContent stringContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = httpClient.PostAsync(_baseURL + "api/Traytypemaster/", stringContent).Result)
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    traytypemasterResponse = JsonConvert.DeserializeObject<TraytypemasterGetAll>(apiResponse);
                    if (traytypemasterResponse.IsError == false)
                    {
                        TempData["SuccessMessage"] = traytypemasterResponse.Message;

                        //TempData["InfoMessage"] = string.Format("Hello {0}.\\nCurrent Date and Time: {1}", "Anshu", DateTime.Now.ToString());
                    }
                    else
                    {
                        TempData["ErrorMessage"] = traytypemasterResponse.Message;

                    }
                    if (traytypemasterResponse.IsError == false)
                    {

                        return RedirectToAction("Index");
                    }
                }
            }
            return View("Edit");
        }
        [HttpPost]
        public async Task<ActionResult> Edit(Traytypemaster traytypemaster)
        {

            _authResponse = HttpContext.Session.GetObjectFromJson<AuthResponse>("loginDetails");
            traytypemaster.CompanyId = _authResponse.result.CompanyId; ;
            traytypemaster.CompanyCode = _authResponse.result.Id;
            traytypemaster.LastUpdatedDateandtime = CommonClasses.GetCurrentTime();
            traytypemaster.UserName = _authResponse.result.Username;
            traytypemaster.IsModify = false;
            traytypemaster.IsDelete = false;

            TraytypemasterGetAll traytypemasterResponse = new TraytypemasterGetAll();
            CancellationToken cancellationToken = new CancellationToken();
            string data = JsonConvert.SerializeObject(traytypemaster);
            StringContent stringContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = httpClient.PutAsync(_baseURL + "api/Traytypemaster/", stringContent).Result)
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    traytypemasterResponse = JsonConvert.DeserializeObject<TraytypemasterGetAll>(apiResponse);
                    if (traytypemasterResponse.IsError == false)
                    {
                        TempData["SuccessMessage"] = traytypemasterResponse.Message;

                        //TempData["InfoMessage"] = string.Format("Hello {0}.\\nCurrent Date and Time: {1}", "Anshu", DateTime.Now.ToString());
                    }
                    else
                    {
                        TempData["ErrorMessage"] = traytypemasterResponse.Message;

                    }
                    if (traytypemasterResponse.Message == "Duplicate Record")
                    {
                        return RedirectToAction("Edit", traytypemaster.Id);
                    }
                    if (response.IsSuccessStatusCode)
                    {

                        return RedirectToAction("Index");
                    }



                }
            }
            return View("Edit", traytypemaster);
        }
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {

            CancellationToken cancellationToken = new CancellationToken();
            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = httpClient.GetAsync(_baseURL + "api/Traytypemaster/Delete?id=" + id).Result)
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
            TraytypemasterGetAll traytypemasterResponse = new TraytypemasterGetAll();

            using (var httpClient = new HttpClient())
            {
                
                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = await httpClient.GetAsync(_baseURL + "api/Traytypemaster/" + id, cancellationToken))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    traytypemasterResponse = JsonConvert.DeserializeObject<TraytypemasterGetAll>(apiResponse);
                }
            }
            return View(traytypemasterResponse.result);
        }

    }
    
}
