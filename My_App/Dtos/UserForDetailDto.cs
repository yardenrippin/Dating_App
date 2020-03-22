using My_App.Modles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_App.Dtos
{
    public class UserForDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
      
        public string Gender { get; set; }
        public int Age { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Coutnry { get; set; }
        public string Photourl { get; set; }

        public ICollection<PhotosFOrDEtailDTO> Photos { get; set; }
    }
}
