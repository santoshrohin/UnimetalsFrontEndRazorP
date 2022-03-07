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
                    if (_authResponse == null)
                    {

                        return RedirectToActionPermanent("Index", "Login");

                    }
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

                if (listItems.Find(x => Convert.ToInt32(x.Value) == itemMaster.ItemCategoryId) != null)
                {
                    listItems.Find(x => Convert.ToInt32(x.Value) == itemMaster.ItemCategoryId).Selected = true;
                }

                ViewBag.ItemCategoryMasterItem = listItems;
            }

            using (var httpClient = new HttpClient())
            {
                dropDownListItem = new DropDownListItem();
                FieldNames = "id as Value,HsnNumber as Text";
                TableNames = "hsnmaster";


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

                if (listItems.Find(x => Convert.ToInt32(x.Value) == itemMaster.GSTId) != null)
                {
                    listItems.Find(x => Convert.ToInt32(x.Value) == itemMaster.GSTId).Selected = true;
                }

                ViewBag.hsnmasterItem = listItems;
            }

            using (var httpClient = new HttpClient())
            {
                dropDownListItem = new DropDownListItem();
                FieldNames = "id as Value,UnitName as Text";
                TableNames = "UnitMaster";


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

                if (listItems.Find(x => Convert.ToInt32(x.Value) == itemMaster.UOM) != null)
                {
                    listItems.Find(x => Convert.ToInt32(x.Value) == itemMaster.UOM).Selected = true;
                }

                ViewBag.UOMItem = listItems;
            }

            using (var httpClient = new HttpClient())
            {
                dropDownListItem = new DropDownListItem();
                FieldNames = "id as Value,UnitName as Text";
                TableNames = "UnitMaster";


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

                if (listItems.Find(x => Convert.ToInt32(x.Value) == itemMaster.UOMAlternateID) != null)
                {
                    listItems.Find(x => Convert.ToInt32(x.Value) == itemMaster.UOMAlternateID).Selected = true;
                }

                ViewBag.UOMAlternateItem = listItems;
            }
            return View(itemMaster);
        }
        public async Task<ActionResult> Create()
        {
            CancellationToken cancellationToken = new CancellationToken();
            ItemMaster itemMaster = new ItemMaster();
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

                if (listItems.Find(x => Convert.ToInt32(x.Value) == itemMaster.ItemCategoryId) != null)
                {
                    listItems.Find(x => Convert.ToInt32(x.Value) == itemMaster.ItemCategoryId).Selected = true;
                }

                ViewBag.ItemCategoryMasterItem = listItems;
            }

            using (var httpClient = new HttpClient())
            {
                dropDownListItem = new DropDownListItem();
                FieldNames = "id as Value,HsnNumber as Text";
                TableNames = "hsnmaster";


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

                if (listItems.Find(x => Convert.ToInt32(x.Value) == itemMaster.GSTId) != null)
                {
                    listItems.Find(x => Convert.ToInt32(x.Value) == itemMaster.GSTId).Selected = true;
                }

                ViewBag.hsnmasterItem = listItems;
            }

            using (var httpClient = new HttpClient())
            {
                dropDownListItem = new DropDownListItem();
                FieldNames = "id as Value,UnitName as Text";
                TableNames = "UnitMaster";


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

                if (listItems.Find(x => Convert.ToInt32(x.Value) == itemMaster.UOM) != null)
                {
                    listItems.Find(x => Convert.ToInt32(x.Value) == itemMaster.UOM).Selected = true;
                }

                ViewBag.UOMItem = listItems;
            }

            using (var httpClient = new HttpClient())
            {
                dropDownListItem = new DropDownListItem();
                FieldNames = "id as Value,UnitName as Text";
                TableNames = "UnitMaster";


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

                if (listItems.Find(x => Convert.ToInt32(x.Value) == itemMaster.UOMAlternateID) != null)
                {
                    listItems.Find(x => Convert.ToInt32(x.Value) == itemMaster.UOMAlternateID).Selected = true;
                }

                ViewBag.UOMAlternateItem = listItems;
            }
            return View("Edit",itemMaster);
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
                    if (itemMasterResponse.Message == "Duplicate Record")
                    {
                        return RedirectToAction("Edit", itemMaster.Id);
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
        public async Task<ActionResult> Delete(int id)
        {

            CancellationToken cancellationToken = new CancellationToken();
            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = httpClient.GetAsync(_baseURL + "api/ItemMaster/Delete?id=" + id).Result)
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
            ItemMasterGetAll itemMasterResponse = new ItemMasterGetAll();
            ItemMaster itemMaster = new ItemMaster();
            using (var httpClient = new HttpClient())
            {

                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = await httpClient.GetAsync(_baseURL + "api/ItemMaster/" + id, cancellationToken))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    itemMasterResponse = JsonConvert.DeserializeObject<ItemMasterGetAll>(apiResponse);
                }
            }


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

                if (listItems.Find(x => Convert.ToInt32(x.Value) == itemMaster.ItemCategoryId) != null)
                {
                    listItems.Find(x => Convert.ToInt32(x.Value) == itemMaster.ItemCategoryId).Selected = true;
                }

                ViewBag.ItemCategoryMasterItem = listItems;
            }

            using (var httpClient = new HttpClient())
            {
                dropDownListItem = new DropDownListItem();
                FieldNames = "id as Value,HsnNumber as Text";
                TableNames = "hsnmaster";


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

                if (listItems.Find(x => Convert.ToInt32(x.Value) == itemMaster.GSTId) != null)
                {
                    listItems.Find(x => Convert.ToInt32(x.Value) == itemMaster.GSTId).Selected = true;
                }

                ViewBag.hsnmasterItem = listItems;
            }

            using (var httpClient = new HttpClient())
            {
                dropDownListItem = new DropDownListItem();
                FieldNames = "id as Value,UnitName as Text";
                TableNames = "UnitMaster";


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

                if (listItems.Find(x => Convert.ToInt32(x.Value) == itemMaster.UOM) != null)
                {
                    listItems.Find(x => Convert.ToInt32(x.Value) == itemMaster.UOM).Selected = true;
                }

                ViewBag.UOMItem = listItems;
            }

            using (var httpClient = new HttpClient())
            {
                dropDownListItem = new DropDownListItem();
                FieldNames = "id as Value,UnitName as Text";
                TableNames = "UnitMaster";


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

                if (listItems.Find(x => Convert.ToInt32(x.Value) == itemMaster.UOMAlternateID) != null)
                {
                    listItems.Find(x => Convert.ToInt32(x.Value) == itemMaster.UOMAlternateID).Selected = true;
                }

                ViewBag.UOMAlternateItem = listItems;
            }
            return View(itemMasterResponse.result);
        }

    }
}
