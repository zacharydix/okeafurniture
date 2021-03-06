using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace okeafurniture.CORE.Entites
{
    public class Account
    {
        public int AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public bool IsAdmin { get; set; }

        //Navigational Properties
        public List<PaymentMethod> PaymentMethods { get; set; }
        public List<Cart> Carts { get; set; }

        public Account()
        {
            PaymentMethods = new List<PaymentMethod>();
            Carts = new List<Cart>();
        }
    }
}
