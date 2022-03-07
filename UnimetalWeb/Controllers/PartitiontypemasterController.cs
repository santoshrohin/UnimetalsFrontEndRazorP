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

namespace UnimetalWeb.Models
{
    
    public class PartitiontypemasterController : Controller
    {
        private IConfiguration _configuration;
        private string _baseURL;
        public AuthResponse _authResponse;

        public PartitiontypemasterController(IConfiguration configuration)
        {
            _configuration = configuration;
            _baseURL = _configuration.GetSection("baseULR").Value;

        }
        public async Task<IActionResult> Index()
        {
            CancellationToken cancellationToken = new CancellationToken();
            PartitiontypemasterResponse partitiontypemasterResponse = new PartitiontypemasterResponse();

            using (HttpClientHandler handler = new HttpClientHandler())
            {
                using (var httpClient = new HttpClient(handler))
                {
                    _authResponse = HttpContext.Session.GetObjectFromJson<AuthResponse>("loginDetails");
                    httpClient.Timeout = TimeSpan.FromMinutes(10);
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authResponse.result.Token);
                    using (var response = await httpClient.GetAsync(_baseURL + "api/Partitiontypemaster", cancellationToken))
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        partitiontypemasterResponse = JsonConvert.DeserializeObject<PartitiontypemasterResponse>(apiResponse);
                    }
                }
            }

            return View(partitiontypemasterResponse.result);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            CancellationToken cancellationToken = new CancellationToken();
            PartitiontypemasterGetAll partitiontypemasterResponse = new PartitiontypemasterGetAll();
            Partitiontypemaster partitiontypemaster = new Partitiontypemaster();

            using (var httpClient = new HttpClient())
            {

                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = await httpClient.GetAsync(_baseURL + "api/Partitiontypemaster/" + id, cancellationToken))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    partitiontypemasterResponse = JsonConvert.DeserializeObject<PartitiontypemasterGetAll>(apiResponse);
                    partitiontypemaster = partitiontypemasterResponse.result;

                }
            }
            return View(partitiontypemaster);
        }
        public ActionResult Create()
        {
            return View("Edit");
        }
        [HttpPost]
        public async Task<ActionResult> Create(Partitiontypemaster partitiontypemaster)
        {
            _authResponse = HttpContext.Session.GetObjectFromJson<AuthResponse>("loginDetails");
            partitiontypemaster.Id = 0;
            partitiontypemaster.CompanyId = _authResponse.result.CompanyId; ;
            partitiontypemaster.CompanyCode = _authResponse.result.Id;
            partitiontypemaster.LastUpdatedDateandtime = CommonClasses.GetCurrentTime();
            partitiontypemaster.UserName = _authResponse.result.Username;
            partitiontypemaster.IsModify = false;
            partitiontypemaster.IsDelete = false;
            PartitiontypemasterGetAll partitiontypemasterResponse = new PartitiontypemasterGetAll();
            CancellationToken cancellationToken = new CancellationToken();
            string data = JsonConvert.SerializeObject(partitiontypemaster);
            StringContent stringContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = httpClient.PostAsync(_baseURL + "api/Partitiontypemaster/", stringContent).Result)
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    partitiontypemasterResponse = JsonConvert.DeserializeObject<PartitiontypemasterGetAll>(apiResponse);
                    if (partitiontypemasterResponse.IsError == false)
                    {
                        TempData["SuccessMessage"] = partitiontypemasterResponse.Message;

                        //TempData["InfoMessage"] = string.Format("Hello {0}.\\nCurrent Date and Time: {1}", "Anshu", DateTime.Now.ToString());
                    }
                    else
                    {
                        TempData["ErrorMessage"] = partitiontypemasterResponse.Message;

                    }
                    if (partitiontypemasterResponse.IsError == false)
                    {

                        return RedirectToAction("Index");
                    }
                }
            }
            return View("Edit");
        }
        [HttpPost]
        public async Task<ActionResult> Edit(Partitiontypemaster partitiontypemaster)
        {

            _authResponse = HttpContext.Session.GetObjectFromJson<AuthResponse>("loginDetails");
            partitiontypemaster.CompanyId = _authResponse.result.CompanyId; ;
            partitiontypemaster.CompanyCode = _authResponse.result.Id;
            partitiontypemaster.LastUpdatedDateandtime = CommonClasses.GetCurrentTime();
            partitiontypemaster.UserName = _authResponse.result.Username;
            partitiontypemaster.IsModify = false;
            partitiontypemaster.IsDelete = false;

            PartitiontypemasterGetAll partitiontypemasterResponse = new PartitiontypemasterGetAll();
            CancellationToken cancellationToken = new CancellationToken();
            string data = JsonConvert.SerializeObject(partitiontypemaster);
            StringContent stringContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = httpClient.PutAsync(_baseURL + "api/Partitiontypemaster/", stringContent).Result)
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    partitiontypemasterResponse = JsonConvert.DeserializeObject<PartitiontypemasterGetAll>(apiResponse);
                    if (partitiontypemasterResponse.IsError == false)
                    {
                        TempData["SuccessMessage"] = partitiontypemasterResponse.Message;

                        //TempData["InfoMessage"] = string.Format("Hello {0}.\\nCurrent Date and Time: {1}", "Anshu", DateTime.Now.ToString());
                    }
                    else
                    {
                        TempData["ErrorMessage"] = partitiontypemasterResponse.Message;

                    }
                    if (partitiontypemasterResponse.Message == "Duplicate Record")
                    {
                        return RedirectToAction("Edit", partitiontypemaster.Id);
                    }
                    if (response.IsSuccessStatusCode)
                    {

                        return RedirectToAction("Index");
                    }



                }
            }
            return View("Edit", partitiontypemaster);
        }
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {

            CancellationToken cancellationToken = new CancellationToken();
            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = httpClient.GetAsync(_baseURL + "api/Partitiontypemaster/Delete?id=" + id).Result)
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
            PartitiontypemasterGetAll partitiontypemasterResponse = new PartitiontypemasterGetAll();

            using (var httpClient = new HttpClient())
            {

                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = await httpClient.GetAsync(_baseURL + "api/Partitiontypemaster/" + id, cancellationToken))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    partitiontypemasterResponse = JsonConvert.DeserializeObject<PartitiontypemasterGetAll>(apiResponse);
                }
            }
            return View(partitiontypemasterResponse.result);
        }

    }
}
