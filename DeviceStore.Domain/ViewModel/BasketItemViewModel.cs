using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceStore.Domain.ViewModel
{
    public class BasketItemViewModel
    {
        public string BasketItemId { get; set; }
        [Display(Name = "Название")]
        public string DeviceName { get; set; }
        [Display(Name = "Количество")]
        public int QuantityDevicesItems { get; set; }
        [Display(Name = "Цена")]
        public decimal Price { get; set; }
        [Display(Name = "Фотография")]
        public string Image { get; set; }
    }
}
