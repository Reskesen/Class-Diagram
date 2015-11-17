using ClassDiagram.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDiagram.Command
{
    public class MoveClassCommand : IUndoRedoCommand
    {

        private ClassShape shape;

        private double offsetX;

        private double offsetY;

        public MoveClassCommand(ClassShape _shape, double _offsetX, double _offsetY)
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

       
        public void UnExecute()
        {
            shape.CanvasCenterX -= offsetX;
            shape.CanvasCenterY -= offsetY;
        }
    }
}
