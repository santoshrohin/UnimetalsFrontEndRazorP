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
    

    public class SupplierMasterController : Controller
    {
        private IConfiguration _configuration;
        private string _baseURL;
        public AuthResponse _authResponse;

        public SupplierMasterController(IConfiguration configuration)
        {
            _configuration = configuration;
            _baseURL = _configuration.GetSection("baseULR").Value;

        }
        public async Task<IActionResult> Index()
        {
            CancellationToken cancellationToken = new CancellationToken();
            SupplierMasterResponse supplierMasterResponse = new SupplierMasterResponse();

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
                    using (var response = await httpClient.GetAsync(_baseURL + "api/SupplierMaster", cancellationToken))
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        supplierMasterResponse = JsonConvert.DeserializeObject<SupplierMasterResponse>(apiResponse);
                    }
                }
            }

            return View(supplierMasterResponse.result);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            CancellationToken cancellationToken = new CancellationToken();
            SupplierMasterGetAll supplierMasterResponse = new SupplierMasterGetAll();
            SupplierMaster supplierMaster = new SupplierMaster();

            using (var httpClient = new HttpClient())
            {

                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = await httpClient.GetAsync(_baseURL + "api/SupplierMaster/" + id, cancellationToken))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    supplierMasterResponse = JsonConvert.DeserializeObject<SupplierMasterGetAll>(apiResponse);
                    supplierMaster = supplierMasterResponse.result;

                }
            }
            return View(supplierMaster);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> Create(SupplierMaster supplierMaster)
        {
            bool status = false;
            _authResponse = HttpContext.Session.GetObjectFromJson<AuthResponse>("loginDetails");
            supplierMaster.Id = 0;
            supplierMaster.CompanyId = _authResponse.result.CompanyId; ;
            supplierMaster.CompanyCode = _authResponse.result.Id;
            supplierMaster.LastUpdatedDateandtime = CommonClasses.GetCurrentTime();
            supplierMaster.UserName = _authResponse.result.Username;
            supplierMaster.IsModify = false;
            supplierMaster.IsDelete = false;
            SupplierMasterGetAll supplierMasterResponse = new SupplierMasterGetAll();
            CancellationToken cancellationToken = new CancellationToken();
            string jsonData = JsonConvert.SerializeObject(supplierMaster);
            StringContent stringContent = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = httpClient.PostAsync(_baseURL + "api/SupplierMaster/", stringContent).Result)
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    supplierMasterResponse = JsonConvert.DeserializeObject<SupplierMasterGetAll>(apiResponse);
                    if (supplierMasterResponse.IsError == false)
                    {
                        TempData["SuccessMessage"] = supplierMasterResponse.Message;
                        status = true;
                        //TempData["InfoMessage"] = string.Format("Hello {0}.\\nCurrent Date and Time: {1}", "Anshu", DateTime.Now.ToString());
                    }
                    else
                    {
                        TempData["ErrorMessage"] = supplierMasterResponse.Message;
                        status = false;
                    }
                    
                }
            }
            object Data = new { status = status };
            return new JsonResult(Data);
        }
        [HttpPost]
        public async Task<JsonResult> Edit(SupplierMaster data)
        {
            bool status = false;
            _authResponse = HttpContext.Session.GetObjectFromJson<AuthResponse>("loginDetails");
            data.CompanyId = _authResponse.result.CompanyId; ;
            data.CompanyCode = _authResponse.result.Id;
            data.LastUpdatedDateandtime = CommonClasses.GetCurrentTime();
            data.UserName = _authResponse.result.Username;
            data.IsModify = false;
            data.IsDelete = false;

            SupplierMasterGetAll supplierMasterResponse = new SupplierMasterGetAll();
            CancellationToken cancellationToken = new CancellationToken();
            string jsonData = JsonConvert.SerializeObject(data);
            StringContent stringContent = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = httpClient.PutAsync(_baseURL + "api/SupplierMaster/", stringContent).Result)
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    supplierMasterResponse = JsonConvert.DeserializeObject<SupplierMasterGetAll>(apiResponse);
                    if (supplierMasterResponse.IsError == false)
                    {
                        TempData["SuccessMessage"] = supplierMasterResponse.Message;
                        status = true;
                        //TempData["InfoMessage"] = string.Format("Hello {0}.\\nCurrent Date and Time: {1}", "Anshu", DateTime.Now.ToString());
                    }
                    else
                    {
                        status = false;
                        TempData["ErrorMessage"] = supplierMasterResponse.Message;

                    }
                    


                }
            }
            object Data = new { status = status };
            return new JsonResult(Data);
        }
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {

            CancellationToken cancellationToken = new CancellationToken();
            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = httpClient.GetAsync(_baseURL + "api/SupplierMaster/Delete?id=" + id).Result)
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
            SupplierMasterGetAll supplierMasterResponse = new SupplierMasterGetAll();

            using (var httpClient = new HttpClient())
            {

                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = await httpClient.GetAsync(_baseURL + "api/SupplierMaster/" + id, cancellationToken))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    supplierMasterResponse = JsonConvert.DeserializeObject<SupplierMasterGetAll>(apiResponse);
                }
            }
            return View(supplierMasterResponse.result);
        }

    }

}
