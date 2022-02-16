using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using UnimetalWeb.Models;

namespace UnimetalWeb.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> IndexAsync()
        {
            CancellationToken cancellationToken = new CancellationToken();
            CompanyMasterResponse reservationList = new CompanyMasterResponse();
            using (var httpClient = new HttpClient())
            {
                //http://localhost:44350/api/CompanyMaster
                httpClient.Timeout = TimeSpan.FromMinutes(10);
                //try
                //{
                //    var response = await httpClient.GetAsync("https://localhost:44350/api/CompanyMaster", cancellationToken);
                //    string apiResponse = await response.Content.ReadAsStringAsync();
                //    reservationList = JsonConvert.DeserializeObject<List<Reservation>>(apiResponse);
                //}
                //catch (Exception ex)
                //{

                //    throw;
                //}

                using (var response = await httpClient.GetAsync("http://localhost:44350/api/CompanyMaster", cancellationToken))
                {
                    var apiResponse = await response. Content.ReadAsStringAsync();
                    reservationList = JsonConvert.DeserializeObject<CompanyMasterResponse>(apiResponse);
                }
            }
            return View(reservationList);
        }


    }
}
