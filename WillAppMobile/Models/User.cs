using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WillAppMobile.Models
{
    public class User
    {
        public int Id { get; set; }
        public string IdentityNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }

        // Kullanıcının vasiyetleri için bir koleksiyon
        public List<Will> Wills { get; set; } = new List<Will>();
    }


}
