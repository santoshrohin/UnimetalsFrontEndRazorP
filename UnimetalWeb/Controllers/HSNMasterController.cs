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
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace UnimetalWeb.Controllers
{
    public class HSNMasterController : Controller
    {
        private IConfiguration _configuration;
        private string _baseURL;
        public AuthResponse _authResponse;

        public HSNMasterController(IConfiguration configuration)
        {
            _configuration = configuration;
            _baseURL = _configuration.GetSection("baseULR").Value;

        }
        public async Task<IActionResult> Index()
        {
            CancellationToken cancellationToken = new CancellationToken();
            HSNMasterResponse hsnMasterResponse = new HSNMasterResponse();

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
                    using (var response = await httpClient.GetAsync(_baseURL + "api/HSNMaster", cancellationToken))
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        hsnMasterResponse = JsonConvert.DeserializeObject<HSNMasterResponse>(apiResponse);
                    }
                }
            }

            return View(hsnMasterResponse.result);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            CancellationToken cancellationToken = new CancellationToken();
            HSNMasterGetAll hsnMasterResponse = new HSNMasterGetAll();
            HSNMaster hsnMaster = new HSNMaster();

            using (var httpClient = new HttpClient())
            {

                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = await httpClient.GetAsync(_baseURL + "api/HSNMaster/" + id, cancellationToken))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    hsnMasterResponse = JsonConvert.DeserializeObject<HSNMasterGetAll>(apiResponse);
                    hsnMaster = hsnMasterResponse.result;

                }
            }
            string FieldNames = "id as Value,MaterialTypeTitle as Text";
            string TableNames = "materialtypemaster";
            string WhereCondition = "isdelete=0";
            string DropDownId = "dd";

            DropDownListItem dropDownListItem = new DropDownListItem();
            List<SelectListItem> listItems = new List<SelectListItem>();
            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = await httpClient.GetAsync(_baseURL + "api/CommonHelper/" + FieldNames + "/" + TableNames + "/" + WhereCondition + "/" + DropDownId + "/", cancellationToken))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    dropDownListItem = JsonConvert.DeserializeObject<DropDownListItem>(apiResponse);

                }
                foreach (var item in dropDownListItem.result)
                {
                    SelectListItem selectListItem = new SelectListItem();
                    selectListItem.Value = item.Value.ToString();
                    selectListItem.Text = item.Text;
                    listItems.Add(selectListItem);
                }

                if (listItems.Find(x => Convert.ToInt32(x.Value) == hsnMaster.HsnType) != null)
                {
                    listItems.Find(x => Convert.ToInt32(x.Value) == hsnMaster.HsnType).Selected = true;
                }

                ViewBag.HsnTypeItem = listItems;
            }
            return View(hsnMaster);
        }
        public async Task<ActionResult> Create()
        {
            CancellationToken cancellationToken = new CancellationToken();
            HSNMaster hSNMaster = new HSNMaster();
            string FieldNames = "id as Value,HSNType as Text";
            string TableNames = "hsntypeMaster";
            string WhereCondition = "isdelete=0";
            string DropDownId = "dd";

            DropDownListItem dropDownListItem = new DropDownListItem();
            List<SelectListItem> listItems = new List<SelectListItem>();
            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = await httpClient.GetAsync(_baseURL + "api/CommonHelper/" + FieldNames + "/" + TableNames + "/" + WhereCondition + "/" + DropDownId + "/", cancellationToken))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    dropDownListItem = JsonConvert.DeserializeObject<DropDownListItem>(apiResponse);

                }
                foreach (var item in dropDownListItem.result)
                {
                    SelectListItem selectListItem = new SelectListItem();
                    selectListItem.Value = item.Value.ToString();
                    selectListItem.Text = item.Text;
                    listItems.Add(selectListItem);
                }


                ViewBag.HsnTypeItem = listItems;
            }
            return View("Edit",hSNMaster);
        }
        [HttpPost]
        public async Task<ActionResult> Create(HSNMaster hsnMaster)
        {
            _authResponse = HttpContext.Session.GetObjectFromJson<AuthResponse>("loginDetails");
            hsnMaster.Id = 0;
            hsnMaster.CompanyId = _authResponse.result.CompanyId; ;
            hsnMaster.CompanyCode = _authResponse.result.Id;
            hsnMaster.LastUpdatedDateandtime = CommonClasses.GetCurrentTime();
            hsnMaster.UserName = _authResponse.result.Username;
            hsnMaster.IsModify = false;
            hsnMaster.IsDelete = false;
            HSNMasterGetAll hsnMasterResponse = new HSNMasterGetAll();
            CancellationToken cancellationToken = new CancellationToken();
            string data = JsonConvert.SerializeObject(hsnMaster);
            StringContent stringContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = httpClient.PostAsync(_baseURL + "api/HSNMaster/", stringContent).Result)
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    hsnMasterResponse = JsonConvert.DeserializeObject<HSNMasterGetAll>(apiResponse);
                    if (hsnMasterResponse.IsError == false)
                    {
                        TempData["SuccessMessage"] = hsnMasterResponse.Message;

                        //TempData["InfoMessage"] = string.Format("Hello {0}.\\nCurrent Date and Time: {1}", "Anshu", DateTime.Now.ToString());
                    }
                    else
                    {
                        TempData["ErrorMessage"] = hsnMasterResponse.Message;

                    }
                    if (hsnMasterResponse.IsError == false)
                    {

                        return RedirectToAction("Index");
                    }
                }
            }
            return View("Edit");
        }
        [HttpPost]
        public async Task<ActionResult> Edit(HSNMaster hsnMaster)
        {

            _authResponse = HttpContext.Session.GetObjectFromJson<AuthResponse>("loginDetails");
            hsnMaster.CompanyId = _authResponse.result.CompanyId; ;
            hsnMaster.CompanyCode = _authResponse.result.Id;
            hsnMaster.LastUpdatedDateandtime = CommonClasses.GetCurrentTime();
            hsnMaster.UserName = _authResponse.result.Username;
            hsnMaster.IsModify = false;
            hsnMaster.IsDelete = false;

            HSNMasterGetAll hsnMasterResponse = new HSNMasterGetAll();
            CancellationToken cancellationToken = new CancellationToken();
            string data = JsonConvert.SerializeObject(hsnMaster);
            StringContent stringContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = httpClient.PutAsync(_baseURL + "api/HSNMaster/", stringContent).Result)
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    hsnMasterResponse = JsonConvert.DeserializeObject<HSNMasterGetAll>(apiResponse);
                    if (hsnMasterResponse.IsError == false)
                    {
                        TempData["SuccessMessage"] = hsnMasterResponse.Message;

                        //TempData["InfoMessage"] = string.Format("Hello {0}.\\nCurrent Date and Time: {1}", "Anshu", DateTime.Now.ToString());
                    }
                    else
                    {
                        TempData["ErrorMessage"] = hsnMasterResponse.Message;

                    }
                    if (hsnMasterResponse.Message == "Duplicate Record")
                    {
                        return RedirectToAction("Edit", hsnMaster.Id);
                    }
                    if (response.IsSuccessStatusCode)
                    {

                        return RedirectToAction("Index");
                    }



                }
            }
            return View("Edit", hsnMaster);
        }
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {

            CancellationToken cancellationToken = new CancellationToken();

            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = httpClient.GetAsync(_baseURL + "api/HSNMaster/Delete?id=" + id).Result)
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
            HSNMasterGetAll hsnMasterResponse = new HSNMasterGetAll();
            HSNMaster hSNMaster = new HSNMaster();
            using (var httpClient = new HttpClient())
            {

                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = await httpClient.GetAsync(_baseURL + "api/HSNMaster/" + id, cancellationToken))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    hsnMasterResponse = JsonConvert.DeserializeObject<HSNMasterGetAll>(apiResponse);
                }
            }

            string FieldNames = "id as Value,MaterialTypeTitle as Text";
            string TableNames = "materialtypemaster";
            string WhereCondition = "isdelete=0";
            string DropDownId = "dd";

            DropDownListItem dropDownListItem = new DropDownListItem();
            List<SelectListItem> listItems = new List<SelectListItem>();
            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = await httpClient.GetAsync(_baseURL + "api/CommonHelper/" + FieldNames + "/" + TableNames + "/" + WhereCondition + "/" + DropDownId + "/", cancellationToken))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    dropDownListItem = JsonConvert.DeserializeObject<DropDownListItem>(apiResponse);

                }
                foreach (var item in dropDownListItem.result)
                {
                    SelectListItem selectListItem = new SelectListItem();
                    selectListItem.Value = item.Value.ToString();
                    selectListItem.Text = item.Text;
                    listItems.Add(selectListItem);
                }

                
                ViewBag.HsnTypeItem = listItems;
            }

            return View(hsnMasterResponse.result);
        }

    }
    
}
