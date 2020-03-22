using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_App.Dtos
{
    public class PhotoForCreationDto
    {
        public string Url { get; set; }
        public IFormFile File { get; set; }
        public string Description { get; set; }
        public DateTime DAteAdded { get; set; }
        public string PublicID { get; set; }

        public PhotoForCreationDto()
        {
            DAteAdded = DateTime.Now;
        }
    }
}
