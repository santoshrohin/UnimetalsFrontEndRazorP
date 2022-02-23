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
    public class ItemsubcategorymasterController : Controller
    {
        private IConfiguration _configuration;
        private string _baseURL;
        public AuthResponse _authResponse;

        public ItemsubcategorymasterController(IConfiguration configuration)
        {
            _configuration = configuration;
            _baseURL = _configuration.GetSection("baseULR").Value;

        }
        public async Task<IActionResult> Index()
        {
            CancellationToken cancellationToken = new CancellationToken();
            ItemsubcategorymasterResponse itemsubcategorymasterResponse = new ItemsubcategorymasterResponse();

            using (HttpClientHandler handler = new HttpClientHandler())
            {
                using (var httpClient = new HttpClient(handler))
                {
                    _authResponse = HttpContext.Session.GetObjectFromJson<AuthResponse>("loginDetails");
                    httpClient.Timeout = TimeSpan.FromMinutes(10);
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authResponse.result.Token);
                    using (var response = await httpClient.GetAsync(_baseURL + "api/Itemsubcategorymaster", cancellationToken))
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        itemsubcategorymasterResponse = JsonConvert.DeserializeObject<ItemsubcategorymasterResponse>(apiResponse);
                    }
                }
            }

            return View(itemsubcategorymasterResponse.result);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            CancellationToken cancellationToken = new CancellationToken();
            ItemsubcategorymasterGetAll itemsubcategorymasterResponse = new ItemsubcategorymasterGetAll();
            Itemsubcategorymaster itemsubcategorymaster = new Itemsubcategorymaster();

            using (var httpClient = new HttpClient())
            {

                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = await httpClient.GetAsync(_baseURL + "api/Itemsubcategorymaster/" + id, cancellationToken))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    itemsubcategorymasterResponse = JsonConvert.DeserializeObject<ItemsubcategorymasterGetAll>(apiResponse);
                    itemsubcategorymaster = itemsubcategorymasterResponse.result;

                }
            }
            return View(itemsubcategorymaster);
        }
        public ActionResult Create()
        {
            return View("Edit");
        }
        [HttpPost]
        public async Task<ActionResult> Create(Itemsubcategorymaster itemsubcategorymaster)
        {
            _authResponse = HttpContext.Session.GetObjectFromJson<AuthResponse>("loginDetails");
            itemsubcategorymaster.id = 0;
            itemsubcategorymaster.company_id = _authResponse.result.CompanyId; ;
            itemsubcategorymaster.company_code = _authResponse.result.Id;
            itemsubcategorymaster.last_updated_dateandtime = CommonClasses.GetCurrentTime();
            itemsubcategorymaster.username = _authResponse.result.Username;
            itemsubcategorymaster.is_modify = false;
            itemsubcategorymaster.is_delete = false;
            ItemsubcategorymasterGetAll itemsubcategorymasterResponse = new ItemsubcategorymasterGetAll();
            CancellationToken cancellationToken = new CancellationToken();
            string data = JsonConvert.SerializeObject(itemsubcategorymaster);
            StringContent stringContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = httpClient.PostAsync(_baseURL + "api/Itemsubcategorymaster/", stringContent).Result)
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    itemsubcategorymasterResponse = JsonConvert.DeserializeObject<ItemsubcategorymasterGetAll>(apiResponse);
                    if (itemsubcategorymasterResponse.IsError == false)
                    {
                        TempData["SuccessMessage"] = itemsubcategorymasterResponse.Message;

                        //TempData["InfoMessage"] = string.Format("Hello {0}.\\nCurrent Date and Time: {1}", "Anshu", DateTime.Now.ToString());
                    }
                    else
                    {
                        TempData["ErrorMessage"] = itemsubcategorymasterResponse.Message;

                    }
                    if (itemsubcategorymasterResponse.IsError == false)
                    {

                        return RedirectToAction("Index");
                    }
                }
            }
            return View("Edit");
        }
        [HttpPost]
        public async Task<ActionResult> Edit(Itemsubcategorymaster itemsubcategorymaster)
        {

            _authResponse = HttpContext.Session.GetObjectFromJson<AuthResponse>("loginDetails");
            itemsubcategorymaster.id = 0;
            itemsubcategorymaster.company_id = _authResponse.result.CompanyId; ;
            itemsubcategorymaster.company_code = _authResponse.result.Id;
            itemsubcategorymaster.last_updated_dateandtime = CommonClasses.GetCurrentTime();
            itemsubcategorymaster.username = _authResponse.result.Username;
            itemsubcategorymaster.is_modify = false;
            itemsubcategorymaster.is_delete = false;

            ItemsubcategorymasterGetAll itemsubcategorymasterResponse = new ItemsubcategorymasterGetAll();
            CancellationToken cancellationToken = new CancellationToken();
            string data = JsonConvert.SerializeObject(itemsubcategorymaster);
            StringContent stringContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = httpClient.PutAsync(_baseURL + "api/Itemsubcategorymaster/", stringContent).Result)
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    itemsubcategorymasterResponse = JsonConvert.DeserializeObject<ItemsubcategorymasterGetAll>(apiResponse);
                    if (itemsubcategorymasterResponse.IsError == false)
                    {
                        TempData["SuccessMessage"] = itemsubcategorymasterResponse.Message;

                        //TempData["InfoMessage"] = string.Format("Hello {0}.\\nCurrent Date and Time: {1}", "Anshu", DateTime.Now.ToString());
                    }
                    else
                    {
                        TempData["ErrorMessage"] = itemsubcategorymasterResponse.Message;

                    }
                    if (response.IsSuccessStatusCode)
                    {

                        return RedirectToAction("Index");
                    }



                }
            }
            return View("Edit", itemsubcategorymaster);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {

            CancellationToken cancellationToken = new CancellationToken();

            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = httpClient.GetAsync(_baseURL + "api/Itemsubcategorymaster/" + id).Result)
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
            ItemsubcategorymasterGetAll itemsubcategorymasterResponse = new ItemsubcategorymasterGetAll();

            using (var httpClient = new HttpClient())
            {

                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = await httpClient.GetAsync(_baseURL + "api/Itemsubcategorymaster/" + id, cancellationToken))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    itemsubcategorymasterResponse = JsonConvert.DeserializeObject<ItemsubcategorymasterGetAll>(apiResponse);
                }
            }
            return View(itemsubcategorymasterResponse.result);
        }

    }
}
