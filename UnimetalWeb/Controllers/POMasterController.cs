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
using UnimetalWeb.Models.Transactions;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace UnimetalWeb.Controllers
{
    
    public class POMasterController : Controller
    {
        private IConfiguration _configuration;
        private string _baseURL;
        public AuthResponse _authResponse;

        public POMasterController(IConfiguration configuration)
        {
            _configuration = configuration;
            _baseURL = _configuration.GetSection("baseULR").Value;

        }
        public async Task<JsonResult> getItems()
        {
            CancellationToken cancellationToken = new CancellationToken();
            string FieldNames = "id as Value,PartCode as Text";
            string TableNames = "itemMaster";
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



            }
            object Data = listItems;
            return new JsonResult(Data);
        }
        public async Task<JsonResult> getGSTItems()
        {
            CancellationToken cancellationToken = new CancellationToken();
            string FieldNames = "id as Value,HsnNumber as Text";
            string TableNames = "HSNMaster";
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



            }
            object Data = listItems;
            return new JsonResult(Data);
        }
        public async Task<JsonResult> getUnitItems()
        {
            CancellationToken cancellationToken = new CancellationToken();
            string FieldNames = "id as Value,UnitName as Text";
            string TableNames = "UnitMaster";
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



            }
            object Data = listItems;
            return new JsonResult(Data);
        }

        public async Task<JsonResult> getItemDetails(int id)
        {
            CancellationToken cancellationToken = new CancellationToken();
            ItemMasterGetAll itemmasterResponse = new ItemMasterGetAll();
            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMinutes(10);
                
                using (var response = await httpClient.GetAsync(_baseURL + "api/ItemMaster/" + id + "/", cancellationToken))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    itemmasterResponse = JsonConvert.DeserializeObject<ItemMasterGetAll>(apiResponse);

                }
                



            }
            object Data = itemmasterResponse;
            return new JsonResult(Data);
        }
        public async Task<IActionResult> Index()
        {
            CancellationToken cancellationToken = new CancellationToken();
            POMasterResponse poMasterResponse = new POMasterResponse();

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
                    using (var response = await httpClient.GetAsync(_baseURL + "api/POMaster", cancellationToken))
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        poMasterResponse = JsonConvert.DeserializeObject<POMasterResponse>(apiResponse);
                    }
                }
            }

            return View(poMasterResponse.result);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            CancellationToken cancellationToken = new CancellationToken();
            POMasterGetAll poMasterResponse = new POMasterGetAll();
            POMaster poMaster = new POMaster();

            using (var httpClient = new HttpClient())
            {

                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = await httpClient.GetAsync(_baseURL + "api/POMaster/" + id, cancellationToken))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    poMasterResponse = JsonConvert.DeserializeObject<POMasterGetAll>(apiResponse);
                    poMaster = poMasterResponse.result;

                }
            }
            return View(poMaster);
        }
        public async Task<ActionResult> CreateAsync()
        {
            CancellationToken cancellationToken = new CancellationToken();
            string FieldNames = "id as Value,CategoryName as Text";
            string TableNames = "ItemCategoryMaster";
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


                ViewBag.ItemSupplierMasterItems = listItems;
            }

            using (var httpClient = new HttpClient())
            {
                dropDownListItem = new DropDownListItem();
                FieldNames = "id as Value,POType as Text";
                TableNames = "potypemaster";


                listItems = new List<SelectListItem>();
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

                
                ViewBag.potypemasterItems = listItems;
            }
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> Create(POMaster poMaster)
        {
            bool status = false;
            _authResponse = HttpContext.Session.GetObjectFromJson<AuthResponse>("loginDetails");
            poMaster.Id = 0;
            poMaster.CompanyId = _authResponse.result.CompanyId; ;
            poMaster.CompanyCode = _authResponse.result.Id;
            poMaster.LastUpdatedDateandtime = CommonClasses.GetCurrentTime();
            poMaster.UserName = _authResponse.result.Username;
            poMaster.IsModify = false;
            poMaster.IsDelete = false;
            POMasterGetAll poMasterResponse = new POMasterGetAll();
            CancellationToken cancellationToken = new CancellationToken();
            string jsonData = JsonConvert.SerializeObject(poMaster);
            StringContent stringContent = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = httpClient.PostAsync(_baseURL + "api/POMaster/", stringContent).Result)
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    poMasterResponse = JsonConvert.DeserializeObject<POMasterGetAll>(apiResponse);
                    if (poMasterResponse.IsError == false)
                    {
                        TempData["SuccessMessage"] = poMasterResponse.Message;
                        status = true;
                        //TempData["InfoMessage"] = string.Format("Hello {0}.\\nCurrent Date and Time: {1}", "Anshu", DateTime.Now.ToString());
                    }
                    else
                    {
                        TempData["ErrorMessage"] = poMasterResponse.Message;
                        status = false;
                    }

                }
            }
            object Data = new { status = status };
            return new JsonResult(Data);
        }
        [HttpPost]
        public async Task<JsonResult> Edit(POMaster data)
        {
            bool status = false;
            _authResponse = HttpContext.Session.GetObjectFromJson<AuthResponse>("loginDetails");
            data.CompanyId = _authResponse.result.CompanyId; ;
            data.CompanyCode = _authResponse.result.Id;
            data.LastUpdatedDateandtime = CommonClasses.GetCurrentTime();
            data.UserName = _authResponse.result.Username;
            data.IsModify = false;
            data.IsDelete = false;

            POMasterGetAll poMasterResponse = new POMasterGetAll();
            CancellationToken cancellationToken = new CancellationToken();
            string jsonData = JsonConvert.SerializeObject(data);
            StringContent stringContent = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = httpClient.PutAsync(_baseURL + "api/POMaster/", stringContent).Result)
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    poMasterResponse = JsonConvert.DeserializeObject<POMasterGetAll>(apiResponse);
                    if (poMasterResponse.IsError == false)
                    {
                        TempData["SuccessMessage"] = poMasterResponse.Message;
                        status = true;
                        //TempData["InfoMessage"] = string.Format("Hello {0}.\\nCurrent Date and Time: {1}", "Anshu", DateTime.Now.ToString());
                    }
                    else
                    {
                        status = false;
                        TempData["ErrorMessage"] = poMasterResponse.Message;

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

                using (var response = httpClient.GetAsync(_baseURL + "api/POMaster/Delete?id=" + id).Result)
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
            POMasterGetAll poMasterResponse = new POMasterGetAll();

            using (var httpClient = new HttpClient())
            {

                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = await httpClient.GetAsync(_baseURL + "api/POMaster/" + id, cancellationToken))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    poMasterResponse = JsonConvert.DeserializeObject<POMasterGetAll>(apiResponse);
                }
            }
            return View(poMasterResponse.result);
        }

    }
}
