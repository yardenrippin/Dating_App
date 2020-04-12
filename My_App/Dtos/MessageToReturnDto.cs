﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_App.Dtos
{
    public class MessageToReturnDto
    {
        public int Id { get; set; }

        public int SenderId { get; set; }

        public string SenderKnownas { get; set; }

        public string SenderPhotoUrl { get; set; }

        public int RecipientId { get; set; }

        public string RecipientKnownas { get; set; }

        public string RecipientPhotoUrl { get; set; }

        public string Content { get; set; }

        public bool IsRead { get; set; }

        public DateTime? DateRead { get; set; }

        public DateTime MessageSent { get; set; }
    }
}
