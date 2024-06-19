using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WillAppMobile.Models
{
    public class Tombstone
    {
        public int Id { get; set; }
        public int UserId { get; set; }  // İlgili kullanıcının ID'si
        public string Title { get; set; }  // Mezar taşının başlığı
        public List<Post> Posts { get; set; } = new List<Post>();  // Mezar taşına eklenen gönderiler
        public User User { get; set; }  // İlişkilendirilmiş kullanıcı
    }

}
