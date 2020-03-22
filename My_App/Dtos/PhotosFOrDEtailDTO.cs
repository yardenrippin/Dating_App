using My_App.Modles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_App.Dtos
{
    public class PhotosFOrDEtailDTO
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DAteAdded { get; set; }
        public bool Ismain { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
