using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ClassDiagram.Model
{

    public class ClassShape : NotifyBase
    {
        /*
        private static int counter = 0;

        public int Number { get; }

        private double x = 200;

        public double X { get { return x; } set { x = value; NotifyPropertyChanged(); NotifyPropertyChanged(() => CanvasCenterX); } }

        private double y = 200;

        public double Y { get { return y; } set { y = value; NotifyPropertyChanged(); NotifyPropertyChanged(() => CanvasCenterY); } }

        private double width = 100;

        public double Width { get { return width; } set { width = value; NotifyPropertyChanged(); NotifyPropertyChanged(() => CanvasCenterX); NotifyPropertyChanged(() => CenterX); } }

        private double height = 100;

        public double Height { get { return height; } set { height = value; NotifyPropertyChanged(); NotifyPropertyChanged(() => CanvasCenterY); NotifyPropertyChanged(() => CenterY); } }

        public double CanvasCenterX { get { return X + Width / 2; } set { X = value - Width / 2; NotifyPropertyChanged(() => X); } }

        public double CanvasCenterY { get { return Y + Height / 2; } set { Y = value - Height / 2; NotifyPropertyChanged(() => Y); } }

        public double CenterX => Width / 2;

        public double CenterY => Height / 2;

        // ViewModel properties.
        // These properties should be in the ViewModel layer, but it is easier for the demo to put them here, 
        //  to avoid unnecessary complexity.
        // NOTE: This breaks the seperation of layers of the MVVM architecture pattern.
        //       To avoid this a ShapeViewModel class should be created that wraps all Shape objects, 
        //        but it adds to the complexity of the ViewModel layer and this demo and a simpler solution was chosen for the demo.
        //        (this also adds a reference to the PresentationCore class library which is part of .NET, 
        //         but should not be used in the Model layer, creating an unnecessary dependency for the Model layer class library).
        //       To learn how to avoid this and create an application with a more pure MVVM architecture pattern, 
        //        please ask the Teaching Assistants.
        private bool isSelected;
        public bool IsSelected { get { return isSelected; } set { isSelected = value; NotifyPropertyChanged(); NotifyPropertyChanged(() => SelectedColor); } }
        public Brush SelectedColor => IsSelected ? Brushes.Red : Brushes.Yellow;
        */
        private static int counter = 0;
        public int Number { get; set; } = ++counter;

        public double X { get; set; } = 200;
        public double Y { get; set; } = 200;
        public double Width { get; set; } = 200;
        public double Height { get; set; } = 200;
        public List<string> Data { get; set; }

        public ClassShape()
        {
            //Number = ++counter;
        }
        public override string ToString() => Number.ToString();
    }
}
