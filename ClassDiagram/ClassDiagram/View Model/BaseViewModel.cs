using ClassDiagram.Model;
using ClassDiagram.Serialization;
using ClassDiagram.ViewModel;
using ClassDiagram.View;
//using ClassDiagram.Serialization;
using ClassDiagram.Command;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;
//using Microsoft.Practices.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;

namespace ClassDiagram.View_Model
{
    public class BaseViewModel : ViewModelBase
    {
        public UndoRedoController undoRedoController = UndoRedoController.Instance;

        public bool isAddingLine;
        public bool isDeleting;
        public static Type addingLineType;
        public ShapeViewModel addingLineFrom;

        public DialogViews dialogVM { get; set; }

        public ObservableCollection<ShapeViewModel> Shapes { get; set; }
        public ObservableCollection<LineViewModel> Lines { get; set; }

        public ICommand NewDiagramCommand { get; }
        public ICommand OpenDiagramCommand { get; }
        public ICommand SaveDiagramCommand { get; }

        public ICommand ExitCommand { get; set; }

        public ICommand UndoCommand { get; }
        public ICommand RedoCommand { get; }

        public ICommand AddClassCommand { get; }
        public ICommand AddLineCommand { get; }
        public ICommand RemoveClassCommand { get; }
        public ICommand RemoveLinesCommand { get; }
        public ICommand DeleteCommand { get; }


        public BaseViewModel()
        {

            NewDiagramCommand = new RelayCommand(NewDiagram);
            OpenDiagramCommand = new RelayCommand(OpenDiagram);
            SaveDiagramCommand = new RelayCommand(SaveDiagram);

            // , undoRedoController.CanUndo     , undoRedoController.CanRedo
            UndoCommand = new RelayCommand(undoRedoController.Undo);
            RedoCommand = new RelayCommand(undoRedoController.Redo);
            RemoveClassCommand = new RelayCommand<ShapeViewModel>(RemoveShape);
            RemoveLinesCommand = new RelayCommand<IList>(RemoveLines, CanRemoveLines);

            DeleteCommand = new RelayCommand(Delete);

            ExitCommand = new RelayCommand(Exit);

            AddClassCommand = new RelayCommand(AddShape);
            AddLineCommand = new RelayCommand(AddLine);
           

        }

        private static OpenFileDialog openDialog = new OpenFileDialog() { Title = "Open Diagram", Filter = "XML Document (.xml)|*.xml", DefaultExt = "xml", InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), CheckFileExists = true };
        private static SaveFileDialog saveDialog = new SaveFileDialog() { Title = "Save Diagram", Filter = "XML Document (.xml)|*.xml", DefaultExt = "xml", InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) };

        private void NewDiagram()
        {
            if (MessageBox.Show("Are you sure?", "Warning", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Shapes.Clear();
                Lines.Clear();
            }
        }

        private async void OpenDiagram()
        {
            string path = openDialog.ShowDialog() == true ? openDialog.FileName : null;
            if (path != null)
            {
                Diagram diagram = await SerializerXML.Instance.AsyncDeserializeFromFile(path);

                Shapes.Clear();
                diagram.Shapes.Select(x => new ShapeViewModel(x)).ToList().ForEach(x => Shapes.Add(x));
                Lines.Clear();
                diagram.Lines.Select(x => new LineViewModel(x)).ToList().ForEach(x => Lines.Add(x));

                // Reconstruct object graph.
                foreach (LineViewModel line in Lines)
                {
                    line.From = Shapes.Single(s => s.Number == line.Line.FromNumber);
                    line.To = Shapes.Single(s => s.Number == line.Line.ToNumber);
                }
            }
        }

        private void SaveDiagram()
        {
            string path = saveDialog.ShowDialog() == true ? saveDialog.FileName : null;
            if (path != null)
            {
                Diagram diagram = new Diagram() { Shapes = Shapes.Select(x => x.Shape).ToList(), Lines = Lines.Select(x => x.Line).ToList() };
                SerializerXML.Instance.AsyncSerializeToFile(diagram, path);
            }
        }

        private void AddShape()
        {
            //Shapes.Add(new ClassShape());
            undoRedoController.AddAndExecute(new AddClassCommand(Shapes, new ShapeViewModel(new ClassShape() { Data = new List<string> { "text1", "text2", "text3", "text4", "text5" } })));


        }

        private void AddLine()
        {
            isAddingLine = true;
            //Lines.Add(new Line());

            //RaisePropertyChanged(() => ModeOpacity);
        }

        public bool CanRemoveShape(IList _shapes) => _shapes.Count == 1;

        public void RemoveShape(ShapeViewModel _shape)
        {
            undoRedoController.AddAndExecute(new RemoveClassCommand(Shapes, Lines, _shape));
        }

        public bool CanRemoveLines(IList _edges) => _edges.Count >= 1;

        public void RemoveLines(IList _lines)
        {
            undoRedoController.AddAndExecute(new RemoveLineCommand(Lines, _lines.Cast<LineViewModel>().ToList()));
        }

        public void Delete()
        {
            isDeleting = true;
        }

        private void Exit()
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Warning", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
               Application.Current.Shutdown();
            }
            
        }

    }
   
}
