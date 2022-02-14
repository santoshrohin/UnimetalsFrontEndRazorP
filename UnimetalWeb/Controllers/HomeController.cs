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
            List<Reservation> reservationList = new List<Reservation>();
            using (var httpClient = new HttpClient())
            {
            //http://localhost:44350/api/CompanyMaster
                using (var response = await httpClient.GetAsync("http://localhost:44350/api/CompanyMaster", cancellationToken))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reservationList = JsonConvert.DeserializeObject<List<Reservation>>(apiResponse);
                }
            }
            return View(reservationList);
        }


    }
}
