using ClassDiagram.Model;
using ClassDiagram.Command;
using GalaSoft.MvvmLight;
//using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using GalaSoft.MvvmLight.Command;

namespace ClassDiagram.View_Model
{
    public class MainViewModel : BaseViewModel
    {
       

        private Point initialMousePosition;
        private Point initialShapePosition;

        public ICommand MouseDownShapeCommand { get; }
        public ICommand MouseMoveShapeCommand { get; }
        public ICommand MouseUpShapeCommand { get; }

        

        public MainViewModel() : base()

        {
            MouseDownShapeCommand = new RelayCommand<MouseButtonEventArgs>(MouseDownShape);
            MouseMoveShapeCommand = new RelayCommand<MouseEventArgs>(MouseMoveShape);
            MouseUpShapeCommand = new RelayCommand<MouseButtonEventArgs>(MouseUpShape);

            Shapes = new ObservableCollection<ShapeViewModel>() {
                new ShapeViewModel(new ClassShape() { X = 30, Y = 40, Width = 100, Height = 60 }),
                new ShapeViewModel(new ClassShape() { X = 140, Y = 230, Width = 100, Height = 100 })
            };

            Lines = new ObservableCollection<LineViewModel>() {
                new LineViewModel(new Line()) { From = Shapes.ElementAt(0), To = Shapes.ElementAt(1) }
            };

            
        }
        
        private void MouseDownShape(MouseButtonEventArgs e)
        {  

            if (isDeleting)
            {
                var shape = TargetShape(e);
                
                RemoveShape(shape);
                isDeleting = false;
            }
            //if not adding a line
            else if (!isAddingLine)
            {   
                var shape = TargetShape(e);
               
                var mousePosition = RelativeMousePosition(e);

                initialMousePosition = mousePosition;
                initialShapePosition = new Point(shape.X, shape.Y);

                e.MouseDevice.Target.CaptureMouse();
            }
        }

        private void MouseMoveShape(MouseEventArgs e)
        {
            //if mouse captured and not adding a line
            if (Mouse.Captured != null && !isAddingLine)
            {
                var shape = TargetShape(e);
             
                var mousePosition = RelativeMousePosition(e);

                //shape moved by offset
                shape.X = initialShapePosition.X + (mousePosition.X - initialMousePosition.X);
                shape.Y = initialShapePosition.Y + (mousePosition.Y - initialMousePosition.Y);
            }
        }

        private void MouseUpShape(MouseButtonEventArgs e)
        {
            //addingline
            if (isAddingLine)
            {
                //retrieves selected target
                var shape = TargetShape(e);

                //if no shape has been selected
                if (addingLineFrom == null)
                {
                    addingLineFrom = shape;
                    addingLineFrom.IsSelected = true;
                }
                //  if the two shapes aren't the same
                else if (addingLineFrom != shape)
                {
                    //Lines.Add(new Line() { from = addingLineFrom, to = shape });
                    LineViewModel lineToAdd = new LineViewModel(
                        addingLineType == typeof(Line) ? new Line() : new Line()
                    )
                    { From = addingLineFrom, To = shape };
                    undoRedoController.AddAndExecute(new AddLineCommand(Lines, lineToAdd));

                    addingLineFrom.IsSelected = false;

                    isAddingLine = false;
                    addingLineType = null;
                    addingLineFrom = null;

                    //raisepropertychanged(() => modeopacity);
                } 
            } 
            //moving a Shape.
            else
            {
                // shape is retrieved
                var shape = TargetShape(e);
                // The mouse position relative to the target of the mouse event.
                var mousePosition = RelativeMousePosition(e);

                // The Shape is moved back to its original position, so the offset given to the move command works.
                shape.X = initialShapePosition.X;
                shape.Y = initialShapePosition.Y;

                undoRedoController.AddAndExecute(new MoveClassCommand(shape, mousePosition.X - initialMousePosition.X, mousePosition.Y - initialMousePosition.Y));

                // The mouse is released
                e.MouseDevice.Target.ReleaseMouseCapture();
            }
        }

        private ShapeViewModel TargetShape(MouseEventArgs e)
        {
            var shapeVisualElement = (FrameworkElement)e.MouseDevice.Target;

            return (ShapeViewModel)shapeVisualElement.DataContext;
        }

        private Point RelativeMousePosition(MouseEventArgs e)
        {
            var shapeVisualElement = (FrameworkElement)e.MouseDevice.Target;

            var canvas = FindParentOfType<Canvas>(shapeVisualElement);

            return Mouse.GetPosition(canvas);
        }

        private static T FindParentOfType<T>(DependencyObject o)
        {
            dynamic parent = VisualTreeHelper.GetParent(o);
            return parent.GetType().IsAssignableFrom(typeof(T)) ? parent : FindParentOfType<T>(parent);
        }
    }
}
