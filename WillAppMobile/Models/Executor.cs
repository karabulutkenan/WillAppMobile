using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WillAppMobile.Models
{
    public class Executor
    {
        public int Id { get; set; }
        public int WillId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PostalAddress { get; set; }
        public string PhoneNumber { get; set; }

        public virtual Will Will { get; set; }
    }
}
