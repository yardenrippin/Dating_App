using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_App.Dtos
{
    public class Userforlistdto
    {
        public int Id { get; set; }

        public string Name { get; set; }
       
        public string Gender { get; set; }

        public int Age { get; set; }

        public string KnownAs { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastActive { get; set; }

        public string City { get; set; }

        public string Coutnry { get; set; }

        public string PohtoUrl { get; set; }
      
    }
}
