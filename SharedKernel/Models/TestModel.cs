﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Models
{
    public class TestModel
    {
        public TestModel(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
