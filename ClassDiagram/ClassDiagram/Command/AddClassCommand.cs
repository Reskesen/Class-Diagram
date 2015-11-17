using ClassDiagram.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDiagram.Command
{
    public class AddClassCommand : IUndoRedoCommand
    {
        
        private ObservableCollection<ClassShape> shapes;
   
        private ClassShape shape;

        public AddClassCommand(ObservableCollection<ClassShape> _shapes, ClassShape _shape) 
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
