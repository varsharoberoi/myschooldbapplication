﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace myschooldbapplication.Controllers
{
    public class TestModel
    {
        [Key]
        public string Name { get; set; }
    }
}
