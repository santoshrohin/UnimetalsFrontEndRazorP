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
    
    public class ItemMasterController : Controller
    {
        private IConfiguration _configuration;
        private string _baseURL;
        public AuthResponse _authResponse;

        public ItemMasterController(IConfiguration configuration)
        {
            _configuration = configuration;
            _baseURL = _configuration.GetSection("baseULR").Value;

        }
        public async Task<IActionResult> Index()
        {
            CancellationToken cancellationToken = new CancellationToken();
            ItemMasterResponse itemMasterResponse = new ItemMasterResponse();

            using (HttpClientHandler handler = new HttpClientHandler())
            {
                using (var httpClient = new HttpClient(handler))
                {
                    _authResponse = HttpContext.Session.GetObjectFromJson<AuthResponse>("loginDetails");
                    httpClient.Timeout = TimeSpan.FromMinutes(10);
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authResponse.result.Token);
                    using (var response = await httpClient.GetAsync(_baseURL + "api/ItemMaster", cancellationToken))
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        itemMasterResponse = JsonConvert.DeserializeObject<ItemMasterResponse>(apiResponse);
                    }
                }
            }

            return View(itemMasterResponse.result);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            CancellationToken cancellationToken = new CancellationToken();
            ItemMasterGetAll itemMasterResponse = new ItemMasterGetAll();
            ItemMaster itemMaster = new ItemMaster();

            using (var httpClient = new HttpClient())
            {

                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = await httpClient.GetAsync(_baseURL + "api/ItemMaster/" + id, cancellationToken))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    itemMasterResponse = JsonConvert.DeserializeObject<ItemMasterGetAll>(apiResponse);
                    itemMaster = itemMasterResponse.result;

                }
            }
            return View(itemMaster);
        }
        public ActionResult Create()
        {
            return View("Edit");
        }
        [HttpPost]
        public async Task<ActionResult> Create(ItemMaster itemMaster)
        {
            _authResponse = HttpContext.Session.GetObjectFromJson<AuthResponse>("loginDetails");
            itemMaster.Id = 0;
            itemMaster.CompanyId = _authResponse.result.CompanyId; ;
            itemMaster.CompanyCode = _authResponse.result.Id;
            itemMaster.LastUpdatedDateandtime = CommonClasses.GetCurrentTime();
            itemMaster.UserName = _authResponse.result.Username;
            itemMaster.IsModify = false;
            itemMaster.IsDelete = false;
            ItemMasterGetAll itemMasterResponse = new ItemMasterGetAll();
            CancellationToken cancellationToken = new CancellationToken();
            string data = JsonConvert.SerializeObject(itemMaster);
            StringContent stringContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = httpClient.PostAsync(_baseURL + "api/ItemMaster/", stringContent).Result)
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    itemMasterResponse = JsonConvert.DeserializeObject<ItemMasterGetAll>(apiResponse);
                    if (itemMasterResponse.IsError == false)
                    {
                        TempData["SuccessMessage"] = itemMasterResponse.Message;

                        //TempData["InfoMessage"] = string.Format("Hello {0}.\\nCurrent Date and Time: {1}", "Anshu", DateTime.Now.ToString());
                    }
                    else
                    {
                        TempData["ErrorMessage"] = itemMasterResponse.Message;

                    }
                    if (itemMasterResponse.IsError == false)
                    {

                        return RedirectToAction("Index");
                    }
                }
            }
            return View("Edit");
        }
        [HttpPost]
        public async Task<ActionResult> Edit(ItemMaster itemMaster)
        {

            _authResponse = HttpContext.Session.GetObjectFromJson<AuthResponse>("loginDetails");
            itemMaster.CompanyId = _authResponse.result.CompanyId; ;
            itemMaster.CompanyCode = _authResponse.result.Id;
            itemMaster.LastUpdatedDateandtime = CommonClasses.GetCurrentTime();
            itemMaster.UserName = _authResponse.result.Username;
            itemMaster.IsModify = false;
            itemMaster.IsDelete = false;

            ItemMasterGetAll itemMasterResponse = new ItemMasterGetAll();
            CancellationToken cancellationToken = new CancellationToken();
            string data = JsonConvert.SerializeObject(itemMaster);
            StringContent stringContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = httpClient.PutAsync(_baseURL + "api/ItemMaster/", stringContent).Result)
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    itemMasterResponse = JsonConvert.DeserializeObject<ItemMasterGetAll>(apiResponse);
                    if (itemMasterResponse.IsError == false)
                    {
                        TempData["SuccessMessage"] = itemMasterResponse.Message;

                        //TempData["InfoMessage"] = string.Format("Hello {0}.\\nCurrent Date and Time: {1}", "Anshu", DateTime.Now.ToString());
                    }
                    else
                    {
                        TempData["ErrorMessage"] = itemMasterResponse.Message;

                    }
                    if (response.IsSuccessStatusCode)
                    {

                        return RedirectToAction("Index");
                    }



                }
            }
            return View("Edit", itemMaster);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {

            CancellationToken cancellationToken = new CancellationToken();

            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = httpClient.GetAsync(_baseURL + "api/ItemMaster/" + id).Result)
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
            ItemMasterGetAll itemMasterResponse = new ItemMasterGetAll();

            using (var httpClient = new HttpClient())
            {

                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = await httpClient.GetAsync(_baseURL + "api/ItemMaster/" + id, cancellationToken))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    itemMasterResponse = JsonConvert.DeserializeObject<ItemMasterGetAll>(apiResponse);
                }
            }
            return View(itemMasterResponse.result);
        }

    }
}
