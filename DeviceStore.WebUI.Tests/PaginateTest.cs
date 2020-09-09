using DeviceStore.Domain.AbstractModel;
using DeviceStore.Domain.Entities;
using DeviceStore.WebUI.Controllers;
using DeviceStore.WebUI.HtmlHelpers;
using DeviceStore.WebUI.Models;
using DeviceStore.WebUI.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DeviceStore.WebUI.Tests
{
    [TestClass]
    public class PaginateTest
    {
        [TestMethod]
        public void CanPaginate()
        {
            Mock<IDeviceRepository> mock = new Mock<IDeviceRepository>();

            mock.Setup(m => m.Devices).Returns(new List<Device>
            {
                new Device {DeviceId = 1, DeviceName = "Device1", },
                new Device {DeviceId = 2, DeviceName = "Device2", },
                new Device {DeviceId = 3, DeviceName = "Device3", },
                new Device {DeviceId = 4, DeviceName = "Device4", },
                new Device {DeviceId = 5, DeviceName = "Device5", },
                new Device {DeviceId = 6, DeviceName = "Device6", },
            });

            HomeController homeController = new HomeController(mock.Object);
            homeController.pageSize = 3;

            FilteredDevicesList action = (FilteredDevicesList)homeController.Index(null, 1).Model;

            List<Device> devices = action.Devices.ToList();

            Assert.IsTrue(devices.Count == 3);

            Assert.AreEqual(devices[0].DeviceName, "Device1");            
            Assert.AreEqual(devices[1].DeviceName, "Device2");           
        }

        [TestMethod]
        public void CanGeneratePageLinks()
        {
            HtmlHelper htmlHelper = null;

            PagingInfo pagingInfo = new PagingInfo
            { 
                CurrentPage = 2,
                TotalDevices = 28,
                DevicesPerPage = 10,
            };

            Func<int, string> pageUrl = i => "/?page=" + i;

            MvcHtmlString result = htmlHelper.GeneratesHtmlForPageLinks(pagingInfo, pageUrl);

            Assert.AreEqual(@"<a class=""btn btn-default"" href=""/?page=1"">1</a>"
                + @"<a class=""btn btn-default btn btn-warning selected"" href=""/?page=2"">2</a>"
                + @"<a class=""btn btn-default"" href=""/?page=3"">3</a>", result.ToString());
        }

        [TestMethod]
        public void CanSendPaginationViewModel()
        {
            Mock<IDeviceRepository> mock = new Mock<IDeviceRepository>();

            mock.Setup(m => m.Devices).Returns(new List<Device>
            {
                new Device {DeviceId = 1, DeviceName = "Device1", },
                new Device {DeviceId = 2, DeviceName = "Device2", },
                new Device {DeviceId = 3, DeviceName = "Device3", },
                new Device {DeviceId = 4, DeviceName = "Device4", },
                new Device {DeviceId = 5, DeviceName = "Device5", },
                new Device {DeviceId = 6, DeviceName = "Device6", },
            });

            HomeController homeController = new HomeController(mock.Object);
            homeController.pageSize = 3;

            FilteredDevicesList action = (FilteredDevicesList)homeController.Index(null, 1).Model;

            PagingInfo pagingInfo = action.PagingInfo;
            Assert.AreEqual(pagingInfo.DevicesPerPage, 3);
            Assert.AreEqual(pagingInfo.TotalDevices, 6);
            Assert.AreEqual(pagingInfo.CurrentPage, 1);
            Assert.AreEqual(pagingInfo.TotalPages, 2);
        }
    }
}
