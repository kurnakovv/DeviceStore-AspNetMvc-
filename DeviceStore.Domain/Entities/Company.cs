using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceStore.Domain.Entities
{
    public class Company : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public string Image { get; set; }
        public virtual ICollection<Device> DevicesList { get; set; }

        public Company()
        {
            DevicesList = new List<Device>();
        }
    }
}
