using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using UnimetalWeb.Models;
using Microsoft.Extensions.Configuration;

namespace UnimetalWeb.Controllers
{
    public class HomeController : Controller
    {
        private IConfiguration _configuration;
        private string _baseURL;
        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
            _baseURL = _configuration.GetSection("baseULR").Value;
        }
        public async Task<IActionResult> IndexAsync()
        {
            CancellationToken cancellationToken = new CancellationToken();
            CompanyMasterResponse reservationList = new CompanyMasterResponse();
            using (var httpClient = new HttpClient())
            {
                
                httpClient.Timeout = TimeSpan.FromMinutes(10);
                //try
                //{
                //    var response = await httpClient.GetAsync(_baseURL+"/api/CompanyMaster", cancellationToken);
                //    string apiResponse = await response.Content.ReadAsStringAsync();
                //    reservationList = JsonConvert.DeserializeObject<List<Reservation>>(apiResponse);
                //}
                //catch (Exception ex)
                //{

                //    throw;
                //}

                using (var response = await httpClient.GetAsync(_baseURL+"api/CompanyMaster", cancellationToken))
                {
                    var apiResponse = await response. Content.ReadAsStringAsync();
                    reservationList = JsonConvert.DeserializeObject<CompanyMasterResponse>(apiResponse);
                }
            }
            return View(reservationList);
        }


    }
}
