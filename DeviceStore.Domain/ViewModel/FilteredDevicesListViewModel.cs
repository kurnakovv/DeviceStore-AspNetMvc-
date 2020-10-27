using DeviceStore.Domain.Entities;
using DeviceStore.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceStore.Domain.ViewModel
{
    public class FilteredDevicesListViewModel
    {
        public string CurrentCategory { get; set; }
        public IEnumerable<Device> Devices { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
