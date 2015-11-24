﻿using ClassDiagram.Model;
using GalaSoft.MvvmLight.Command;
using ClassDiagram.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace ClassDiagram.View_Model
{
    public class ShapeViewModel : BaseViewModel
    {
        public ClassShape Shape { get; set; }

        private static int counter = 0;

        public int Number { get; }

        private double x = 200;

        public double X { get { return x; } set { x = value; RaisePropertyChanged(); RaisePropertyChanged(() => CanvasCenterX); } }

        private double y = 200;

        public double Y { get { return y; } set { y = value; RaisePropertyChanged(); RaisePropertyChanged(() => CanvasCenterY); } }

        private double width = 100;

        public double Width { get { return width; } set { width = value; RaisePropertyChanged(); RaisePropertyChanged(() => CanvasCenterX); RaisePropertyChanged(() => CenterX); } }

        private double height = 100;

        public double Height { get { return height; } set { height = value; RaisePropertyChanged(); RaisePropertyChanged(() => CanvasCenterY); RaisePropertyChanged(() => CenterY); } }

        public double CanvasCenterX { get { return X + Width / 2; } set { X = value - Width / 2; RaisePropertyChanged(() => X); } }

        public double CanvasCenterY { get { return Y + Height / 2; } set { Y = value - Height / 2; RaisePropertyChanged(() => Y); } }

        public double CenterX => Width / 2;

        public double CenterY => Height / 2;

        private bool isSelected;
        public bool IsSelected { get { return isSelected; } set { isSelected = value; RaisePropertyChanged(); RaisePropertyChanged(() => SelectedColor); } }
        public Brush SelectedColor => IsSelected ? Brushes.Red : Brushes.Yellow;

        public ShapeViewModel(ClassShape _shape) : base()
        {
            Shape = _shape;

          //  RemoveCommand = new RelayCommand(Remove);
        }
        /*
        private void Remove()
        {
            undoRedoController.AddAndExecute(new RemoveShapesCommand(Shapes, Lines, new List<ShapeViewModel>() { this }));
        }
        */
    }
}