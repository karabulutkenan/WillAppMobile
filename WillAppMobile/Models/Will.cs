using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WillAppMobile.Models
{
    public class Will
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Details { get; set; }
        public List<string> Files { get; set; } = new List<string>(); // Dosyaları tutacak liste
        public int ExecutorId { get; set; } // Vasi için yabancı anahtar
        public Executor Executor { get; set; } // İlişkilendirilmiş vasi
    }





}
