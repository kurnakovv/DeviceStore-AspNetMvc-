using DeviceStore.Domain.Entities;
using DeviceStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeviceStore.WebUI.ViewModel
{
    public class FilteredDevicesList
    {
        public string CurrentCategory { get; set; }
        public IEnumerable<Device> Devices { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}