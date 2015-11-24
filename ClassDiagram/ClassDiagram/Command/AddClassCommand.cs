using ClassDiagram.Model;
using ClassDiagram.View_Model;
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
        
        private ObservableCollection<ShapeViewModel> shapes;
   
        private ShapeViewModel shape;

        public AddClassCommand(ObservableCollection<ShapeViewModel> _shapes, ShapeViewModel _shape)
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
