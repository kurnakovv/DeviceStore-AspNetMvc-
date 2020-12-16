using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DeviceStore.Domain.Entities
{
    public class Device : BaseEntity
    {

        [Required(ErrorMessage = "Укажите название устройства!")]
        [Display(Name = "Название")]        
        public string DeviceName { get; set; }

        [Required(ErrorMessage = "Укажите описание устройства!")]
        [Display(Name = "Описание")]
        [DataType(DataType.MultilineText)]
        public string DeviceDescription { get; set; }

        [Required(ErrorMessage = "Укажите категорию устройства!")]
        [Display(Name = "Категория")]
        public string DeviceCategory { get; set; }

        public string CompanyId { get; set; }


        [Required(ErrorMessage = "Укажите количество устройств!")]
        [Display(Name = "Количество")]
        public int DeviceQuantity { get; set; }   
        
        [Display(Name = "Закладка")]
        public bool DeviceFavorites { get; set; }

        [Required(ErrorMessage = "Укажите цену устройства!")]
        [Display(Name = "Цена")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Пожалуйста, введите положительное значение для цены!")]
        public decimal DevicePrice { get; set; }

        [Display(Name = "Фотография")]
        public string DeviceImage { get; set; }
    }
}
