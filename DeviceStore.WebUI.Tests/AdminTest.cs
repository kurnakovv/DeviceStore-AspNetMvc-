using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DeviceStore.Domain.AbstractModel;
using DeviceStore.Domain.Entities;
using Moq;
using DeviceStore.WebUI.Controllers;
using DeviceStore.Domain.Services.Interfaces;
using System.Web.Mvc;

namespace DeviceStore.WebUI.Tests
{
    [TestClass]
    public class AdminTest
    {
        [TestMethod]
        public void CanEditValidDevice()
        {
            Mock<IDeviceRepository> mock = new Mock<IDeviceRepository>();
            Mock<IAdminService> adminService = new Mock<IAdminService>();
            mock.Setup(m => m.Devices).Returns(new List<Device>
            {
                new Device { Id = "Guid1", DeviceName = "Device1"},
                new Device { Id = "Guid2", DeviceName = "Device2"},
                new Device { Id = "Guid3", DeviceName = "Device3"},
            });

            AdminController adminController = new AdminController(mock.Object, adminService.Object);

            Device device1 = adminController.EditDevice(null, "Guid1").ViewData.Model as Device;
            Device device2 = adminController.EditDevice(null, "Guid2").ViewData.Model as Device;
            Device device3 = adminController.EditDevice(null, "Guid3").ViewData.Model as Device;
            Device device4 = adminController.EditDevice(null, "Guid4").ViewData.Model as Device;

            Assert.AreEqual("Guid1", device1.Id);
            Assert.AreEqual("Guid2", device2.Id);
            Assert.AreEqual("Guid3", device3.Id);
        }

        [TestMethod]
        public void CanSaveValidChangesDevice()
        {
            Mock<IDeviceRepository> mock = new Mock<IDeviceRepository>();
            Mock<IAdminService> adminService = new Mock<IAdminService>();
            AdminController adminController = new AdminController(mock.Object, adminService.Object);

            Device device = new Device
            {
                Id = "Guid1",
                DeviceName = "DeviceName",
                DeviceDescription = "DeviceDescription",
                DeviceCategory = "DeviceCategory",
                DeviceQuantity = 1,
                DevicePrice = 1,
                DeviceImage = "Image.jpg",
            };

            ActionResult actionResult = adminController.EditDevice(device);

            adminService.Verify(m => m.EditDevice(device));

            Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public void CannotSaveInValidChangesDevice()
        {
            Mock<IDeviceRepository> mock = new Mock<IDeviceRepository>();
            Mock<IAdminService> adminService = new Mock<IAdminService>();
            AdminController adminController = new AdminController(mock.Object, adminService.Object);

            Device device = new Device
            {
                Id = "Guid1",
                DeviceName = "DeviceName",
                DeviceDescription = "DeviceDescription",
                DeviceCategory = "DeviceCategory",
                DeviceQuantity = 1,
                DevicePrice = 1,
                DeviceImage = "Image.jpg",
            };

            adminController.ModelState.AddModelError("error", "error");
            ActionResult actionResult = adminController.EditDevice(device);
            adminService.Verify(m => m.EditDevice(It.IsAny<Device>()), Times.Never());


            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
        }
    }
}