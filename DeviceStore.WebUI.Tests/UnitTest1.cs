using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using DeviceStore.Domain.AbstractModel;
using DeviceStore.Domain.Entities;
using DeviceStore.WebUI.Controllers;
using DeviceStore.WebUI.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DeviceStore.WebUI.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CanFilterDevicesByCategory()
        {
            Mock<IDeviceRepository> mock = new Mock<IDeviceRepository>();

            mock.Setup(m => m.Devices).Returns(new List<Device>
            {
                new Device {DeviceId = 1, DeviceName = "Device", DeviceCategory = "Category"},
                new Device {DeviceId = 2, DeviceName = "1212Device", DeviceCategory = "Category"},
                new Device {DeviceId = 3, DeviceName = "322", DeviceCategory = "Cat"},
            });
            HomeController controller = new HomeController(mock.Object);

            // TODO: Complete the category filtering check

            List<string> action = ((IEnumerable<string>)controller.Index("Category").Model).ToList();

            Assert.AreEqual(action[0], "Category");
        }

        [TestMethod]
        public void CanCreateCategories()
        {
            // TODO: Finish creating a category.
            Mock<IDeviceRepository> mock = new Mock<IDeviceRepository>();

            mock.Setup(m => m.Devices).Returns(new List<Device>
            {
                new Device {DeviceId = 1, DeviceName = "Device1", DeviceCategory = "Смарт часы"},
                new Device {DeviceId = 2, DeviceName = "Device2", DeviceCategory = "Смарт часы"},
                new Device {DeviceId = 3, DeviceName = "Device3", DeviceCategory = "ПК"},
                new Device {DeviceId = 4, DeviceName = "Device4", DeviceCategory = "Нетбуки"},
            });
            NavigationController controller = new NavigationController(mock.Object);

            List<string> results = ((IEnumerable<string>)controller.CategoryMenu().Model).ToList();

            Assert.AreEqual(results.Count(), 1);
            Assert.AreEqual(results[0], "Смарт часы");
            Assert.AreEqual(results[1], "ПК");
            Assert.AreEqual(results[2], "Нетбуки");
        }
    }
}