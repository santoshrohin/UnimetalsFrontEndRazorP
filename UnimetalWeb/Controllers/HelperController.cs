using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using UnimetalWeb.Models;

namespace UnimetalWeb.Controllers
{
    public class HelperController : Controller
    {
        private IConfiguration _configuration;
        private string _baseURL;
        public HelperController(IConfiguration configuration)
        {
            _configuration = configuration;
            _baseURL = _configuration.GetSection("baseULR").Value;
        }
        public async Task<List<SelectListItem>> DropDownValuesAsync(string FieldNames, string TableNames, string WhereCondition, string DropDownId)
        {
            CancellationToken cancellationToken = new CancellationToken();
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
                    SelectListItem selectListItem1 = new SelectListItem();
                    selectListItem1.Value = item.Value.ToString();
                    selectListItem1.Text = item.Text;
                    listItems.Add(selectListItem1);
                }
            }
            return listItems;
        }
    }
}
