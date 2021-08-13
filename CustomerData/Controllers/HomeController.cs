using CustomerData.Models;
using Data.Entities;
using Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerData.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICustomersRepository _customersRepository;
        //private readonly ICustomersRepository _customersRepository1;

        public HomeController(ICustomersRepository customersRepository)
        {
            _customersRepository = customersRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Dublicate Data 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult DublicateData()
        {
            var customerList = _customersRepository.DublicateDataCustomers("CustomersDublicateDataALL");
            //string customerList = "Kalpesh";
            return new JsonResult(new { data = customerList });
        }

        [HttpGet]
        public IActionResult ShowData(string ClientCustomerID, string AccountID)
        {
            var data = _customersRepository.GetDataById(ClientCustomerID, AccountID);
            return View(data);
        }

        public IActionResult Deletedata(string id)
        {
            if (id != null)
            {
                _customersRepository.Delete(id);
            }
            return Content("Success");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult AutoMateCustomers()
        {
            List<CustomerList> customerList = new List<CustomerList>();
            customerList = _customersRepository.DublicateDataCustomersAuto("CustomersDublicateDataALL").ToList();
            foreach (var item in customerList)
            {
                var data = _customersRepository.GetDataById(item.ClientCustomerID, item.AccountID);
                if(data.Count() == 2)
                {
                    var DataId = data.FirstOrDefault();
                    if(DataId != null)
                    {
                        _customersRepository.DeleteAndBackUp(DataId);
                    }
                }
            }
            return Content("Success");
        }

        [HttpGet]
        public IActionResult AutomateVehicles()
        {
            var vehiclesList = _customersRepository.DublicateDataVehiclesAutoLocal("GetAllCustomers").ToList();
            foreach (var item in vehiclesList)
            {
                var data = _customersRepository.GetVehiclesData(Convert.ToString(item.CustomerID));
                if(data.Count() > 0)
                {
                    var DataId = data.FirstOrDefault();
                    if(DataId != null)
                    {
                        _customersRepository.DeleteAndBackUpVehicles(DataId);
                    }
                    else
                    {
                       
                    }
                }
            }
            return Content("Suceess");
        }
    }
}
