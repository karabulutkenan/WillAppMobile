using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WillAppMobile.Models
{
    public class Post
    {
        public int Id { get; set; }
        public int TombstoneId { get; set; }  // İlgili mezar taşının ID'si
        public string Content { get; set; }  // Gönderinin içeriği
        public DateTime DatePosted { get; set; }  // Gönderinin eklenme tarihi
        public Tombstone Tombstone { get; set; }  // İlişkilendirilmiş mezar taşı
    }

}
