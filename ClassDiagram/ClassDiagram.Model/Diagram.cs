﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDiagram.Model
{
    public class Diagram
    {
        public List<ClassShape> Shapes { get; set; }
        public List<Line> Lines { get; set; }
    }
}
