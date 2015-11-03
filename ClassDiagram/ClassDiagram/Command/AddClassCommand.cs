﻿using ClassDiagram.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classdiagram.Command
{
    public class AddShapeCommand
    {
        
        private ObservableCollection<ClassShape> shapes;
   
        private ClassShape shape;

        public AddShapeCommand(ObservableCollection<ClassShape> _shapes, ClassShape _shape) 
        { 
            shapes = _shapes;
            shape = _shape;
        }

        public void Execute()
        {
            shapes.Add(shape);
        }

        public void UnExecute()
        {
            shapes.Remove(shape);
        }

    }
}