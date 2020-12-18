using DeviceStore.Domain.AbstractModel;
using DeviceStore.Domain.Entities;
using DeviceStore.Domain.Services;
using DeviceStore.Domain.Services.Interfaces;
using DeviceStore.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeviceStore.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IAdminService _adminService;
        private readonly IRepository<Company> _companyRepository;

        public AdminController(IDeviceRepository deviceRepository,
                               IAdminService adminService,
                               IRepository<Company> companyRepository)
        {
            _deviceRepository = deviceRepository;
            _adminService = adminService;
            _companyRepository = companyRepository;
        }

        public ViewResult Index()
        {
            return View(_deviceRepository.Devices);
        }

        public ViewResult EditDevice(string id)
        {
            var model = new AdminViewModel
            {
                Device = _deviceRepository.Devices.FirstOrDefault(d => d.Id == id),
                DeviceCompany = _companyRepository.Collection(),
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDevice(Device device, HttpPostedFileBase deviceImage = null)
        {            
            if (ModelState.IsValid)
            {
                if (deviceImage != null)
                {
                    AddImage(device, deviceImage);
                }

                _adminService.EditDevice(device);
                TempData["messageEdit"] = string.Format("Изменения в устройстве \"{0}\" были сохранены", device.DeviceName);
                
                return RedirectToAction("Index");
            }
            else
            {
                return View(device);
            }
        }

        public ActionResult DetailsDevice(string id)
        {
            AdminViewModel model = new AdminViewModel
            {
                Device = _deviceRepository.Devices.FirstOrDefault(d => d.Id == id),
                DeviceCompany = _companyRepository.Collection(),
            };
            return PartialView(model);
        }

        public ViewResult CreateDevice()
        {
            var model = new AdminViewModel
            {
                Device = new Device(),
                DeviceCompany = _companyRepository.Collection(),
            };
            return View("EditDevice", model);
        }

        public ActionResult RemoveDevice(Device device, string id)
        {
            AdminViewModel model = new AdminViewModel
            {
                Device = _deviceRepository.Devices.FirstOrDefault(d => d.Id == id),
                DeviceCompany = _companyRepository.Collection(),
            };

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveDevice(string id)
        {
            _adminService.RemoveDevice(id);
            TempData["messageRemove"] = string.Format("Устройство было удалено успешно");

            return RedirectToAction("Index");
        }

        private void AddImage(Device device, HttpPostedFileBase deviceImage)
        {
            if (deviceImage != null)
            {
                var allowedExtensions = new List<string> { ".jpeg", ".png", ".jpg" };
                var fileExtension = Path.GetExtension(deviceImage.FileName);
                if (allowedExtensions.Contains(fileExtension))
                {
                    var fileName = Path.GetFileName(deviceImage.FileName);
                    var directoryToSave = Server.MapPath(Url.Content("~/Content/ProductImages/"));

                    var pathToSave = Path.Combine(directoryToSave, fileName);
                    deviceImage.SaveAs(pathToSave);
                    device.DeviceImage = fileName;
                }
            }
        }
    }
}