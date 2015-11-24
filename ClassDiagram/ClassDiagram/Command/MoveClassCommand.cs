using ClassDiagram.Model;
using ClassDiagram.View_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDiagram.Command
{
    public class MoveClassCommand : IUndoRedoCommand
    {

        private ShapeViewModel shape;

        private double offsetX;

        private double offsetY;

        public MoveClassCommand(ShapeViewModel _shape, double _offsetX, double _offsetY)
        {
            shape = _shape;
            offsetX = _offsetX;
            offsetY = _offsetY;
        }

        public void Execute()
        {
            shape.CanvasCenterX += offsetX;
            shape.CanvasCenterY += offsetY;
        }

        // For undoing the command.
        public void UnExecute()
        {
            shape.CanvasCenterX -= offsetX;
            shape.CanvasCenterY -= offsetY;
        }
    }
}
