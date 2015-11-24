using ClassDiagram.View_Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDiagram.Command
{
    public class RemoveLineCommand : IUndoRedoCommand
    {
        private ObservableCollection<LineViewModel> lines;

        private List<LineViewModel> linesToRemove;

        public RemoveLineCommand(ObservableCollection<LineViewModel> _lines, List<LineViewModel> _linesToRemove)
        {
            lines = _lines;
            linesToRemove = _linesToRemove;
        }

        public void Execute()
        {
            linesToRemove.ForEach(x => lines.Remove(x));
        }

        public void UnExecute()
        {
            linesToRemove.ForEach(x => lines.Add(x));
        }
    }
}
