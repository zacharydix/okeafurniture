﻿using okeafurniture.CORE.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace okeafurniture.WEB.Models
{
    public class AccountModel
    {
        public int AccountId { get; set; }

        [Required(ErrorMessage ="First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email address is required"),
            EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Admin permission status is required")]
        public bool IsAdmin { get; set; }
        public List<PaymentMethod> PaymentMethods { get; set; }
        public List<Cart> Carts { get; set; }
    }
}
