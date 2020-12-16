using DeviceStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DeviceStore.Domain.ViewModel
{
    public class AdminViewModel
    {
        public Device Device { get; set; }
        public IEnumerable<Company> DeviceCompany { get; set; }
    }
}
