﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Models
{
    public class SentEmailConfirmation
    {
        public SentEmailConfirmation(Guid id, string email, string message)
        {
            Id = id;
            Email = email;
            Message = message;
        }

        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
    }
}
