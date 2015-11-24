using ClassDiagram.Model;
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

namespace ClassDiagram.View_Model
{
    public class BaseViewModel : ViewModelBase
    {
        public UndoRedoController undoRedoController = UndoRedoController.Instance;

        public bool isAddingLine;
        public static Type addingLineType;
        public ShapeViewModel addingLineFrom;

        public ObservableCollection<ShapeViewModel> Shapes { get; set; }
        public ObservableCollection<LineViewModel> Lines { get; set; }

        public ICommand UndoCommand { get; }
        public ICommand RedoCommand { get; }

        public ICommand AddClassCommand { get; }
        public ICommand AddLineCommand { get; }
       

        public BaseViewModel()
        {
            // , undoRedoController.CanUndo     , undoRedoController.CanRedo
            UndoCommand = new RelayCommand(undoRedoController.Undo);
            RedoCommand = new RelayCommand(undoRedoController.Redo);

            AddClassCommand = new RelayCommand(AddShape);
            AddLineCommand = new RelayCommand(AddLine);
           

        }

        private void AddShape()
        {
            //Shapes.Add(new ClassShape());
            undoRedoController.AddAndExecute(new AddClassCommand(Shapes, new ShapeViewModel(new ClassShape())));

        }

        private void AddLine()
        {
            isAddingLine = true;
            //Lines.Add(new Line());

            //RaisePropertyChanged(() => ModeOpacity);
        }

    }
   
}
