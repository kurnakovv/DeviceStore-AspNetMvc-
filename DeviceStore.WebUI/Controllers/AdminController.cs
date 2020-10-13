using DeviceStore.Domain.AbstractModel;
using DeviceStore.Domain.Entities;
using DeviceStore.Domain.Services;
using DeviceStore.Domain.Services.Interfaces;
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

        public AdminController(IDeviceRepository deviceRepository,
                               IAdminService adminService)
        {
            _deviceRepository = deviceRepository;
            _adminService = adminService;
        }

        public ViewResult Index()
        {
            return View(_deviceRepository.Devices);
        }

        public ViewResult EditDevice(Device device, string id)
        {
            device = _deviceRepository.Devices
               .FirstOrDefault(d => d.Id == id);

            return View(device);
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
            Device device = _deviceRepository.Devices.FirstOrDefault(d => d.Id == id);
            return PartialView(device);
        }

        public ViewResult CreateDevice()
        {
            return View("EditDevice", new Device());
        }

        public ActionResult RemoveDevice(Device device, string id)
        {
            device = _deviceRepository.Devices.FirstOrDefault(d => d.Id == id);

            return PartialView(device);
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
                var fileName = Path.GetFileName(deviceImage.FileName);
                var directoryToSave = Server.MapPath(Url.Content("~/Content/ProductImages/"));

                var pathToSave = Path.Combine(directoryToSave, fileName);
                deviceImage.SaveAs(pathToSave);
                device.DeviceImage = fileName;
            }
        }
    }
}