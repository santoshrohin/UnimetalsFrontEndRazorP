using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using UnimetalWeb.Models;

namespace UnimetalWeb.Helpers
{
    public class CommonClasses
    {
        private IConfiguration _configuration;
        private string _baseURL;
        public CommonClasses()
        {

        }
        public CommonClasses(IConfiguration configuration)
        {
            _configuration = configuration;
            _baseURL = _configuration.GetSection("baseULR").Value;
        }
        public static DateTime GetCurrentTime()
        {
            DateTime serverTime = DateTime.Now;
            DateTime _localTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(serverTime, TimeZoneInfo.Local.Id, "India Standard Time");
            return _localTime;
        }

        public async Task<List<SelectListItem>> DropDownValuesAsync(string FieldNames, string TableNames, string WhereCondition, string DropDownId)
        {
            CancellationToken cancellationToken = new CancellationToken();
            DropDownListItem dropDownListItem= new DropDownListItem();
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
