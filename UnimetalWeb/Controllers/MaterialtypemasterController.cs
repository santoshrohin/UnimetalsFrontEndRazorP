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
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace UnimetalWeb.Controllers
{

    public class MaterialtypemasterController : Controller
    {
        private IConfiguration _configuration;
        private string _baseURL;
        public AuthResponse _authResponse;

        public MaterialtypemasterController(IConfiguration configuration)
        {
            _configuration = configuration;
            _baseURL = _configuration.GetSection("baseULR").Value;

        }
        public async Task<IActionResult> Index()
        {
            CancellationToken cancellationToken = new CancellationToken();
            MaterialtypemasterResponse materialtypemasterResponse = new MaterialtypemasterResponse();

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
                    using (var response = await httpClient.GetAsync(_baseURL + "api/Materialtypemaster", cancellationToken))
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        materialtypemasterResponse = JsonConvert.DeserializeObject<MaterialtypemasterResponse>(apiResponse);
                    }
                }
            }





            return View(materialtypemasterResponse.result);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            CancellationToken cancellationToken = new CancellationToken();
            MaterialtypemasterGetAll materialtypemasterResponse = new MaterialtypemasterGetAll();
            Materialtypemaster materialtypemaster = new Materialtypemaster();

            using (var httpClient = new HttpClient())
            {

                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = await httpClient.GetAsync(_baseURL + "api/Materialtypemaster/" + id, cancellationToken))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    materialtypemasterResponse = JsonConvert.DeserializeObject<MaterialtypemasterGetAll>(apiResponse);
                    materialtypemaster = materialtypemasterResponse.result;

                }
            }




            #region AllDropDownData
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

                if (listItems.Find(x => Convert.ToInt32(x.Value) == materialtypemaster.MaterialTypeCategoryID) != null)
                {
                    listItems.Find(x => Convert.ToInt32(x.Value) == materialtypemaster.MaterialTypeCategoryID).Selected = true;
                }

                ViewBag.MaterialTypeCategoryItem = listItems;
            }


            using (var httpClient = new HttpClient())
            {
                dropDownListItem = new DropDownListItem();
                FieldNames = "id as Value,ProductLineTitle as Text";
                TableNames = "ProductLineMaster";


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
                if (listItems.Find(x => Convert.ToInt32(x.Value) == materialtypemaster.MaterialTypeProductLineID) != null)
                {
                    listItems.Find(x => Convert.ToInt32(x.Value) == materialtypemaster.MaterialTypeProductLineID).Selected = true;
                }

                ViewBag.MaterialTypeProductLineItem = listItems;
            }
            #endregion



            return View(materialtypemaster);
        }
        public async Task<ActionResult> Create()
        {
            CancellationToken cancellationToken = new CancellationToken();
            MaterialtypemasterGetAll materialtypemasterResponse = new MaterialtypemasterGetAll();
            Materialtypemaster materialtypemaster = new Materialtypemaster();

            #region AllDropDownData
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
                materialtypemaster.MaterialTypeCategoryID = 0;
                materialtypemaster.MaterialTypeProductLineID = 0;

                if (listItems.Find(x => Convert.ToInt32(x.Value) == materialtypemaster.MaterialTypeCategoryID) != null)
                {
                    listItems.Find(x => Convert.ToInt32(x.Value) == materialtypemaster.MaterialTypeCategoryID).Selected = true;
                }

                ViewBag.MaterialTypeCategoryItem = listItems;
            }


            using (var httpClient = new HttpClient())
            {
                dropDownListItem = new DropDownListItem();
                FieldNames = "id as Value,ProductLineTitle as Text";
                TableNames = "ProductLineMaster";


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
                if (listItems.Find(x => Convert.ToInt32(x.Value) == materialtypemaster.MaterialTypeProductLineID) != null)
                {
                    listItems.Find(x => Convert.ToInt32(x.Value) == materialtypemaster.MaterialTypeProductLineID).Selected = true;
                }

                ViewBag.MaterialTypeProductLineItem = listItems;
            }
            #endregion

            return View("Edit",materialtypemaster);
        }
        [HttpPost]
        public async Task<ActionResult> Create(Materialtypemaster materialtypemaster)
        {
            _authResponse = HttpContext.Session.GetObjectFromJson<AuthResponse>("loginDetails");
            materialtypemaster.Id = 0;
            materialtypemaster.CompanyId = _authResponse.result.CompanyId; ;
            materialtypemaster.CompanyCode = _authResponse.result.Id;
            materialtypemaster.LastUpdatedDateandtime = CommonClasses.GetCurrentTime();
            materialtypemaster.UserName = _authResponse.result.Username;
            materialtypemaster.IsModify = false;
            materialtypemaster.IsDelete = false;
            MaterialtypemasterGetAll materialtypemasterResponse = new MaterialtypemasterGetAll();
            CancellationToken cancellationToken = new CancellationToken();
            string data = JsonConvert.SerializeObject(materialtypemaster);
            StringContent stringContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = httpClient.PostAsync(_baseURL + "api/Materialtypemaster/", stringContent).Result)
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    materialtypemasterResponse = JsonConvert.DeserializeObject<MaterialtypemasterGetAll>(apiResponse);
                    if (materialtypemasterResponse.IsError == false)
                    {
                        TempData["SuccessMessage"] = materialtypemasterResponse.Message;

                        //TempData["InfoMessage"] = string.Format("Hello {0}.\\nCurrent Date and Time: {1}", "Anshu", DateTime.Now.ToString());
                    }
                    else
                    {
                        TempData["ErrorMessage"] = materialtypemasterResponse.Message;

                    }
                    if (materialtypemasterResponse.IsError == false)
                    {

                        return RedirectToAction("Index");
                    }
                }
            }
            return View("Edit");
        }
        [HttpPost]
        public async Task<ActionResult> Edit(Materialtypemaster materialtypemaster)
        {

            _authResponse = HttpContext.Session.GetObjectFromJson<AuthResponse>("loginDetails");
            materialtypemaster.CompanyId = _authResponse.result.CompanyId; ;
            materialtypemaster.CompanyCode = _authResponse.result.Id;
            materialtypemaster.LastUpdatedDateandtime = CommonClasses.GetCurrentTime();
            materialtypemaster.UserName = _authResponse.result.Username;
            materialtypemaster.IsModify = false;
            materialtypemaster.IsDelete = false;

            MaterialtypemasterGetAll materialtypemasterResponse = new MaterialtypemasterGetAll();
            CancellationToken cancellationToken = new CancellationToken();
            string data = JsonConvert.SerializeObject(materialtypemaster);
            StringContent stringContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = httpClient.PutAsync(_baseURL + "api/Materialtypemaster/", stringContent).Result)
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    materialtypemasterResponse = JsonConvert.DeserializeObject<MaterialtypemasterGetAll>(apiResponse);
                    if (materialtypemasterResponse.IsError == false)
                    {
                        TempData["SuccessMessage"] = materialtypemasterResponse.Message;

                        //TempData["InfoMessage"] = string.Format("Hello {0}.\\nCurrent Date and Time: {1}", "Anshu", DateTime.Now.ToString());
                    }
                    else
                    {
                        TempData["ErrorMessage"] = materialtypemasterResponse.Message;

                    }
                    if (materialtypemasterResponse.Message == "Duplicate Record")
                    {
                        return RedirectToAction("Edit", materialtypemaster.Id);
                    }
                    if (response.IsSuccessStatusCode)
                    {

                        return RedirectToAction("Index");
                    }



                }
            }
            return View("Edit", materialtypemaster);
        }
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {

            CancellationToken cancellationToken = new CancellationToken();
            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = httpClient.GetAsync(_baseURL + "api/Materialtypemaster/Delete?id=" + id).Result)
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
            MaterialtypemasterGetAll materialtypemasterResponse = new MaterialtypemasterGetAll();
            Materialtypemaster materialtypemaster = new Materialtypemaster();
            using (var httpClient = new HttpClient())
            {

                httpClient.Timeout = TimeSpan.FromMinutes(10);

                using (var response = await httpClient.GetAsync(_baseURL + "api/Materialtypemaster/" + id, cancellationToken))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    materialtypemasterResponse = JsonConvert.DeserializeObject<MaterialtypemasterGetAll>(apiResponse);
                }
            }

            #region AllDropDownData
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

                if (listItems.Find(x => Convert.ToInt32(x.Value) == materialtypemaster.MaterialTypeCategoryID) != null)
                {
                    listItems.Find(x => Convert.ToInt32(x.Value) == materialtypemaster.MaterialTypeCategoryID).Selected = true;
                }

                ViewBag.MaterialTypeCategoryItem = listItems;
            }


            using (var httpClient = new HttpClient())
            {
                dropDownListItem = new DropDownListItem();
                FieldNames = "id as Value,ProductLineTitle as Text";
                TableNames = "ProductLineMaster";


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
                if (listItems.Find(x => Convert.ToInt32(x.Value) == materialtypemaster.MaterialTypeProductLineID) != null)
                {
                    listItems.Find(x => Convert.ToInt32(x.Value) == materialtypemaster.MaterialTypeProductLineID).Selected = true;
                }

                ViewBag.MaterialTypeProductLineItem = listItems;
            }
            #endregion
            return View(materialtypemasterResponse.result);
        }

    }
}
