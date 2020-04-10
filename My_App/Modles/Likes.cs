using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_App.Modles
{
    public class Likes
    {
        public int LikerId { get; set; }

        public int LikeeId { get; set; }

        public User Liker { get; set; }

        public User Likee { get; set; }
    }
}
