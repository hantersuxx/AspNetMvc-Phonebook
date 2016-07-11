using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AspNetMvc_Phonebook.Models
{
    public class Contact
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Номер телефона")]
        [StringLength(13)]
        [RegularExpression(@"^(\+\d{12}|\d{11})$", ErrorMessage = "Неправильно введён номер телефона")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Имя")]
        [RegularExpression(@"^([a-zA-Z0-9а-яА-Я .&'-]+)$", ErrorMessage = "Неправильно введено имя")]
        [StringLength(255)]
        public string FirstName { get; set; }
        [Display(Name = "Фамилия")]
        [RegularExpression(@"^([a-zA-Z0-9а-яА-Я .&'-]+)$", ErrorMessage = "Неправильно введена фамилия")]
        [StringLength(255)]
        public string LastName { get; set; }
        [Display(Name = "Электронная почта")]
        [RegularExpression(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?",
            ErrorMessage ="Неправильно введён адрес электронной почты")]
        [StringLength(320)]
        public string Email { get; set; }
    }
}