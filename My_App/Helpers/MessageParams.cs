using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_App.Helpers
{
    public class MessageParams
    {
        private const int MaxSize = 50;
        public int PageNumber { get; set; } = 1;
        private int pagesize = 10;

        public int Pagesize
        {
            get { return pagesize; }
            set { pagesize = (value > MaxSize) ? MaxSize : value; }
        }

        public int UserId { get; set; }

        public string MessageContainer { get; set; } = "Unread";
    }
}
