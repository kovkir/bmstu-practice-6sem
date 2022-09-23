using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cars.ViewModels
{
    public class OrderViewModels
    {
        [Display(Name = "Имя")]
        [StringLength(10)]
        [Required(ErrorMessage = "Длина имени не менее 2 символов")]
        public string Name { get; set; }

        [Display(Name = "Фамилия")]
        [StringLength(10)]
        [Required(ErrorMessage = "Длина фамилии не менее 5 символов")]
        public string Surname { get; set; }

        [Display(Name = "Адрес доставки")]
        [Required(ErrorMessage = "Длина адреса доставки не менее 5 символов")]
        public string Adress { get; set; }

        [Display(Name = "Номер телефона")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(11)]
        [Required(ErrorMessage = "Длина номера телефона не менее 11 символов")]
        public string Phone { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [StringLength(25)]
        [Required(ErrorMessage = "Длина email не менее 10 символов")]
        public string Email { get; set; }
    }
}
