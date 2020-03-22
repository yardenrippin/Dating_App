using System;

namespace My_App.Modles
{
    public class photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DAteAdded { get; set; }
        public bool  Ismain { get; set; }
        public string PublicID { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }

    }
}