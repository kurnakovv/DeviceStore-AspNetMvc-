using DeviceStore.Domain.AbstractModel;
using DeviceStore.Domain.Entities;
using DeviceStore.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
using DeviceStore.Domain.EFConcrete;

namespace DeviceStore.Domain.Services
{
    public class AdminService : IAdminService
    {
        private readonly IDeviceRepository _deviceRepository;

        public AdminService(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public void EditDevice(Device device)
        {
            _deviceRepository.SaveOrEditDevice(device);
        }

        public void RemoveDevice(string id)
        {
            _deviceRepository.DeleteDevice(id);       
        }
    }
}
