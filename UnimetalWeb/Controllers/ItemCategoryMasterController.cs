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
    
    public class ItemCategoryMasterController : Controller
    {
        private IConfiguration _configuration;
        private string _baseURL;
        public AuthResponse _authResponse;

        public ItemCategoryMasterController(IConfiguration configuration)
        {
            _configuration = configuration;
            _baseURL = _configuration.GetSection("baseULR").Value;

        }
        public async Task<IActionResult> Index()
        {
            CancellationToken cancellationToken = new CancellationToken();
            ItemCategoryMasterResponse itemCategoryMasterResponse = new ItemCategoryMasterResponse();

            using (HttpClientHandler handler = new HttpClientHandler())
            {
                using (var httpClient = new HttpClient(handler))
                {
                    _authResponse = HttpContext.Session.GetObjectFromJson<AuthResponse>("loginDetails");
                    httpClient.Timeout = TimeSpan.FromMinutes(10);
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authResponse.result.Token);
                    using (var response = await httpClient.GetAsync(_baseURL + "api/ItemCategoryMaster", cancellationToken))
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        itemCategoryMasterResponse = JsonConvert.DeserializeObject<ItemCategoryMasterResponse>(apiResponse);
                    }
                }
            }

            return View(itemCategoryMasterResponse.result);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            CancellationToken cancellationToken = new CancellationToken();
            ItemCategoryMasterGetAll itemCategoryMasterResponse = new ItemCategoryMasterGetAll();
            ItemCategoryMaster itemCategoryMaster = new ItemCategoryMaster();

            using (var httpClient = new HttpClient())
            {

                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = await httpClient.GetAsync(_baseURL + "api/ItemCategoryMaster/" + id, cancellationToken))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    itemCategoryMasterResponse = JsonConvert.DeserializeObject<ItemCategoryMasterGetAll>(apiResponse);
                    itemCategoryMaster = itemCategoryMasterResponse.result;

                }
            }
            return View(itemCategoryMaster);
        }
        public ActionResult Create()
        {
            return View("Edit");
        }
        [HttpPost]
        public async Task<ActionResult> Create(ItemCategoryMaster itemCategoryMaster)
        {
            _authResponse = HttpContext.Session.GetObjectFromJson<AuthResponse>("loginDetails");
            itemCategoryMaster.Id = 0;
            itemCategoryMaster.CompanyId = _authResponse.result.CompanyId; ;
            //itemCategoryMaster.CompanyCode = _authResponse.result.Id;
            //itemCategoryMaster.LastUpdatedDateandtime = CommonClasses.GetCurrentTime();
            //itemCategoryMaster.UserName = _authResponse.result.Username;
            //itemCategoryMaster.IsModify = false;
            //itemCategoryMaster.IsDelete = false;
            ItemCategoryMasterGetAll itemCategoryMasterResponse = new ItemCategoryMasterGetAll();
            CancellationToken cancellationToken = new CancellationToken();
            string data = JsonConvert.SerializeObject(itemCategoryMaster);
            StringContent stringContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = httpClient.PostAsync(_baseURL + "api/ItemCategoryMaster/", stringContent).Result)
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    itemCategoryMasterResponse = JsonConvert.DeserializeObject<ItemCategoryMasterGetAll>(apiResponse);
                    if (itemCategoryMasterResponse.IsError == false)
                    {
                        TempData["SuccessMessage"] = itemCategoryMasterResponse.Message;

                        //TempData["InfoMessage"] = string.Format("Hello {0}.\\nCurrent Date and Time: {1}", "Anshu", DateTime.Now.ToString());
                    }
                    else
                    {
                        TempData["ErrorMessage"] = itemCategoryMasterResponse.Message;

                    }
                    if (itemCategoryMasterResponse.IsError == false)
                    {

                        return RedirectToAction("Index");
                    }
                }
            }
            return View("Edit");
        }
        [HttpPost]
        public async Task<ActionResult> Edit(ItemCategoryMaster itemCategoryMaster)
        {

            _authResponse = HttpContext.Session.GetObjectFromJson<AuthResponse>("loginDetails");
            itemCategoryMaster.CompanyId = _authResponse.result.CompanyId; ;
            //itemCategoryMaster.CompanyCode = _authResponse.result.Id;
            //itemCategoryMaster.LastUpdatedDateandtime = CommonClasses.GetCurrentTime();
            //itemCategoryMaster.UserName = _authResponse.result.Username;
            //itemCategoryMaster.IsModify = false;
            //itemCategoryMaster.IsDelete = false;

            ItemCategoryMasterGetAll itemCategoryMasterResponse = new ItemCategoryMasterGetAll();
            CancellationToken cancellationToken = new CancellationToken();
            string data = JsonConvert.SerializeObject(itemCategoryMaster);
            StringContent stringContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = httpClient.PutAsync(_baseURL + "api/ItemCategoryMaster/", stringContent).Result)
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    itemCategoryMasterResponse = JsonConvert.DeserializeObject<ItemCategoryMasterGetAll>(apiResponse);
                    if (itemCategoryMasterResponse.IsError == false)
                    {
                        TempData["SuccessMessage"] = itemCategoryMasterResponse.Message;

                        //TempData["InfoMessage"] = string.Format("Hello {0}.\\nCurrent Date and Time: {1}", "Anshu", DateTime.Now.ToString());
                    }
                    else
                    {
                        TempData["ErrorMessage"] = itemCategoryMasterResponse.Message;

                    }
                    if (response.IsSuccessStatusCode)
                    {

                        return RedirectToAction("Index");
                    }



                }
            }
            return View("Edit", itemCategoryMaster);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {

            CancellationToken cancellationToken = new CancellationToken();

            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = httpClient.GetAsync(_baseURL + "api/ItemCategoryMaster/" + id).Result)
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
            ItemCategoryMasterGetAll itemCategoryMasterResponse = new ItemCategoryMasterGetAll();

            using (var httpClient = new HttpClient())
            {

                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = await httpClient.GetAsync(_baseURL + "api/ItemCategoryMaster/" + id, cancellationToken))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    itemCategoryMasterResponse = JsonConvert.DeserializeObject<ItemCategoryMasterGetAll>(apiResponse);
                }
            }
            return View(itemCategoryMasterResponse.result);
        }

    }
}
