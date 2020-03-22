using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_App.Dtos
{
    public class PhotoForReturnDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DAteAdded { get; set; }
        public bool Ismain { get; set; }
     public string PublicID { get; set; }
    }
}
