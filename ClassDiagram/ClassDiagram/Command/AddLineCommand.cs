using ClassDiagram.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDiagram.Command
{
    public class AddLineCommand : IUndoRedoCommand
    {

        private ObservableCollection<Line> lines;

        private Line line;

        public AddLineCommand(ObservableCollection<Line> _lines, Line _line)
        {
            lines = _lines;
            line = _line;
        }

        public void Execute()
        {
            lines.Add(line);
        }

        public void UnExecute()
        {
            lines.Remove(line);
        }

    }
}
