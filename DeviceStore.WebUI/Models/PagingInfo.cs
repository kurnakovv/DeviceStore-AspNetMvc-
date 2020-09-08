using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeviceStore.WebUI.Models
{
    public class PagingInfo
    {
        public int TotalDevices { get; set; }
        public int DevicesPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages 
        {
            get
            {
                return (int)Math.Ceiling((decimal)TotalDevices / DevicesPerPage);
            }
        }

    }
}