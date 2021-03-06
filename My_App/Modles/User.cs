﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_App.Modles
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte [] PasswordHash { get; set; }
        public byte[] PaswwordSalt { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; }
        public DateTime  LastActive { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Coutnry { get; set; }
        public ICollection<photo> Photos { get; set; }
        public ICollection<Likes> Likers { get; set; }
        public ICollection<Likes> Likees { get; set; }
        public ICollection<Message> MessagesSent { get; set; }
        public ICollection<Message> MessagesRecived { get; set; }


    }
}
