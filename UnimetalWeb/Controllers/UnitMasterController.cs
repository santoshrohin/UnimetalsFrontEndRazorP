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
    public class UnitMasterController : Controller
    {
        private IConfiguration _configuration;
        private string _baseURL;
        public AuthResponse _authResponse;

        public UnitMasterController( IConfiguration configuration)
        {
            _configuration = configuration;
            _baseURL = _configuration.GetSection("baseULR").Value;

        }
        public async Task<IActionResult> Index()
        {
            CancellationToken cancellationToken = new CancellationToken();
            UnitmasterResponse unitmasterResponse = new UnitmasterResponse();
            
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                using (var httpClient = new HttpClient(handler))
                {
                    _authResponse = HttpContext.Session.GetObjectFromJson<AuthResponse>("loginDetails");
                    if (_authResponse == null)
                    {

                        return RedirectToActionPermanent("Index", "Login");

                    }
                    httpClient.Timeout = TimeSpan.FromMinutes(10);
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authResponse.result.Token);
                    using (var response = await httpClient.GetAsync(_baseURL + "api/Unitmaster",cancellationToken))
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        unitmasterResponse = JsonConvert.DeserializeObject<UnitmasterResponse>(apiResponse);
                    }
                }
            }
            
            return View(unitmasterResponse.result);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            CancellationToken cancellationToken = new CancellationToken();
            UnitmasterGetAll unitmasterResponse = new UnitmasterGetAll();
            Unitmaster unitmaster = new Unitmaster();

            using (var httpClient = new HttpClient())
            {
                
                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = await httpClient.GetAsync(_baseURL + "api/Unitmaster/" + id, cancellationToken))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    unitmasterResponse = JsonConvert.DeserializeObject<UnitmasterGetAll>(apiResponse);
                    unitmaster = unitmasterResponse.result;

                }
            }
            return View(unitmaster);
        }
        public ActionResult Create()
        {
            return View("Edit");
        }
        [HttpPost]
        public async Task<ActionResult> Create(Unitmaster unitmaster)
        {
            _authResponse = HttpContext.Session.GetObjectFromJson<AuthResponse>("loginDetails");
            unitmaster.Id = 0;
            unitmaster.CompanyId = _authResponse.result.CompanyId; ;
            unitmaster.CompanyCode = _authResponse.result.Id;
            unitmaster.LastUpdatedDateandtime = CommonClasses.GetCurrentTime();
            unitmaster.UserName = _authResponse.result.Username;
            unitmaster.IsModify = false;
            unitmaster.IsDelete = false;
            UnitmasterGetAll unitmasterResponse = new UnitmasterGetAll();
            CancellationToken cancellationToken = new CancellationToken();
            string data = JsonConvert.SerializeObject(unitmaster);
            StringContent stringContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = httpClient.PostAsync(_baseURL + "api/Unitmaster/", stringContent).Result)
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    unitmasterResponse = JsonConvert.DeserializeObject<UnitmasterGetAll>(apiResponse);
                    if (unitmasterResponse.IsError == false)
                    {
                        TempData["SuccessMessage"] = unitmasterResponse.Message;

                        //TempData["InfoMessage"] = string.Format("Hello {0}.\\nCurrent Date and Time: {1}", "Anshu", DateTime.Now.ToString());
                    }
                    else
                    {
                        TempData["ErrorMessage"] = unitmasterResponse.Message;

                    }
                    if (unitmasterResponse.IsError == false)
                    {

                        return RedirectToAction("Index");
                    }
                }
            }
            return View("Edit");
        }
        [HttpPost]
        public async Task<ActionResult> Edit(Unitmaster unitmaster)
        {
            
            _authResponse = HttpContext.Session.GetObjectFromJson<AuthResponse>("loginDetails");
            unitmaster.CompanyId = _authResponse.result.CompanyId; ;
            unitmaster.CompanyCode = _authResponse.result.Id;
            unitmaster.LastUpdatedDateandtime = CommonClasses.GetCurrentTime();
            unitmaster.UserName = _authResponse.result.Username;
            unitmaster.IsModify = false;
            unitmaster.IsDelete = false;

            UnitmasterGetAll unitmasterResponse = new UnitmasterGetAll();
            CancellationToken cancellationToken = new CancellationToken();
            string data = JsonConvert.SerializeObject(unitmaster);
            StringContent stringContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = httpClient.PutAsync(_baseURL + "api/Unitmaster/", stringContent).Result)
                {
                    var apiResponse =  await response.Content.ReadAsStringAsync();
                    unitmasterResponse = JsonConvert.DeserializeObject<UnitmasterGetAll>(apiResponse);
                    if (unitmasterResponse.IsError == false)
                    {
                        TempData["SuccessMessage"] = unitmasterResponse.Message;

                        //TempData["InfoMessage"] = string.Format("Hello {0}.\\nCurrent Date and Time: {1}", "Anshu", DateTime.Now.ToString());
                    }
                    else
                    {
                        TempData["ErrorMessage"] = unitmasterResponse.Message;
                        
                    }
                    if (unitmasterResponse.Message== "Duplicate Record")
                    {
                        return RedirectToAction("Edit",unitmaster.Id);
                    }
                    if (response.IsSuccessStatusCode)
                    {
                        
                        return RedirectToAction("Index");
                    }
                    
                    
                        
                }
            }
            return View("Edit",unitmaster);
        }
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {

            CancellationToken cancellationToken = new CancellationToken();
            
            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMinutes(10);
                
                using (var response = httpClient.GetAsync(_baseURL + "api/Unitmaster/Delete?id=" + id).Result)
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
            UnitmasterGetAll unitmasterResponse = new UnitmasterGetAll();

            using (var httpClient = new HttpClient())
            {
                
                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = await httpClient.GetAsync(_baseURL + "api/Unitmaster/" + id, cancellationToken))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    unitmasterResponse = JsonConvert.DeserializeObject<UnitmasterGetAll>(apiResponse);
                }
            }
            return View(unitmasterResponse.result);
        }
        
    }
}
