﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOProyect
{
    public class ValidacionDto
    {
        public bool Success { get; set; }
        public List<Error> Errors { get; set; }
    }
}
