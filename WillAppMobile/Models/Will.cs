using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WillAppMobile.Models
{
    public class Will
    {
        public string Title { get; set; }
        public string Details { get; set; }
        public List<UploadedFile> Files { get; set; }
    }
}
