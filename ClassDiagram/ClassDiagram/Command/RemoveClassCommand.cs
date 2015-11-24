using ClassDiagram.View_Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDiagram.Command
{
    public class RemoveClassCommand : IUndoRedoCommand
    {
        private ObservableCollection<ShapeViewModel> shapes;

        private ObservableCollection<LineViewModel> lines;

        private ShapeViewModel shapeToRemove;

        private List<LineViewModel> linesToRemove;
        public RemoveClassCommand(ObservableCollection<ShapeViewModel> _shapes, ObservableCollection<LineViewModel> _lines, ShapeViewModel _shapeToRemove)
        {
            shapes = _shapes;
            lines = _lines;
            shapeToRemove = _shapeToRemove;
            linesToRemove = _lines.Where(x => _shapeToRemove.getNumber() == x.From.Number || _shapeToRemove.getNumber() == x.To.Number).ToList();
        }

        public void Execute()
        {
            linesToRemove.ForEach(x => lines.Remove(x));
            shapes.Remove(shapeToRemove);
        }

        public void UnExecute()
        {
            shapes.Add(shapeToRemove);
            linesToRemove.ForEach(x => lines.Add(x));
        }
    }
}
