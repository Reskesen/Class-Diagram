using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDiagram.Command
{
    public class UndoRedoController
    {
        private readonly Stack<IUndoRedoCommand> undoStack = new Stack<IUndoRedoCommand>();
        private readonly Stack<IUndoRedoCommand> redoStack = new Stack<IUndoRedoCommand>();

        public static UndoRedoController Instance { get; } = new UndoRedoController();

        private UndoRedoController() { }

        public void AddAndExecute(IUndoRedoCommand command)
        {
            //Console.WriteLine(command.ToString());
            undoStack.Push(command);
            redoStack.Clear();
            command.Execute();
        }

        public bool CanUndo() => undoStack.Any();
        public void Undo()
        {
            try {
                if (!undoStack.Any()) throw new InvalidOperationException();
                var command = undoStack.Pop();
                redoStack.Push(command);
                command.UnExecute();
            }
            catch (Exception e)
            { Console.WriteLine("No object to undo!"); }
        }

        public bool CanRedo() => redoStack.Any();
        public void Redo()
        {
            try {
                if (!redoStack.Any()) throw new InvalidOperationException();
                var command = redoStack.Pop();
                undoStack.Push(command);
                command.Execute();
            }
            catch (Exception e)
            { Console.WriteLine("No object to redo!"); }
        }
    }
}
