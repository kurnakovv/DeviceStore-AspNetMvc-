using DeviceStore.Domain.AbstractModel;
using DeviceStore.Domain.EFConcrete;
using DeviceStore.Domain.Entities;
using DeviceStore.Domain.Services;
using DeviceStore.Domain.Services.Interfaces;
using DeviceStore.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Data.Entity;

namespace DeviceStore.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        public ViewResult Index(string categoryDevices, int page = 1)
        {
            FilteredDevicesListViewModel filteredDevicesList = new FilteredDevicesListViewModel
            {
                CurrentCategory = categoryDevices,

                Devices = _homeService.ListDevices(categoryDevices, page),

                PagingInfo = _homeService.PagingInfo(categoryDevices, page),

                Company = _homeService.DeviceCompany(),
            };

            return View(filteredDevicesList);
        }

        public ActionResult DevicesSearch(string deviceName)
        {
            FilteredDevicesListViewModel model = new FilteredDevicesListViewModel
            {
                Devices = _homeService.DeviceSearch(deviceName),
                Company = _homeService.DeviceCompany(),
            };

            if (model.Devices != null)
            {
                return View(model);
            }
            else
            {
                return View("DevicesNotFound");
            }
        }

        public ActionResult DeviceDetails(string id)
        {
            if (id != null)
            {
                DetailsDeviceViewModel model = new DetailsDeviceViewModel
                {
                    Device = _homeService.GetDeviceById(id),
                    Company = _homeService.DeviceCompany(),
                };
                return PartialView(model);
            }

            return HttpNotFound();
        }
    }
}