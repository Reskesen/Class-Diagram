﻿using ClassDiagram.Model;
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
    public class MainViewModel
    {
        private bool isAddingLine;
        private ClassShape addingLineFrom;

        private Point initialMousePosition;
        private Point initialShapePosition;

        public ObservableCollection<ClassShape> Shapes { get; set; }
        public ObservableCollection<Line> Lines { get; set; }

        public ICommand AddClassCommand { get; }
        public ICommand AddLineCommand { get; }
        public ICommand MouseDownShapeCommand { get; }
        public ICommand MouseMoveShapeCommand { get; }
        public ICommand MouseUpShapeCommand { get; }

        public MainViewModel()
        {
            Shapes = new ObservableCollection<ClassShape>() {
                new ClassShape() { X = 30, Y = 40, Width = 100, Height = 60 },
                new ClassShape() { X = 140, Y = 230, Width = 100, Height = 100 }
            };

            Lines = new ObservableCollection<Line>() {
                new Line() { From = Shapes.ElementAt(0), To = Shapes.ElementAt(1) }
            };

            AddClassCommand = new RelayCommand(AddShape);
            AddLineCommand = new RelayCommand(AddLine);
            MouseDownShapeCommand = new RelayCommand<MouseButtonEventArgs>(MouseDownShape);
            MouseMoveShapeCommand = new RelayCommand<MouseEventArgs>(MouseMoveShape);
            MouseUpShapeCommand = new RelayCommand<MouseButtonEventArgs>(MouseUpShape);
        }


        private void AddShape()
        {
            System.Diagnostics.Debug.WriteLine("added a class");
            Shapes.Add(new ClassShape());

        }

        private void AddLine()
        {
            System.Diagnostics.Debug.WriteLine("added a line");
            isAddingLine = true;
            

            //RaisePropertyChanged(() => ModeOpacity);
        }

        private void MouseDownShape(MouseButtonEventArgs e)
        {
            //if not adding a line
            if (!isAddingLine)
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
                System.Diagnostics.Debug.WriteLine("is adding line");
                //retrieves selected target
                var shape = TargetShape(e);

                //if no shape has been selected
                if (addingLineFrom == null)
                {
                    System.Diagnostics.Debug.WriteLine("no class selected");
                    addingLineFrom = shape;
                    addingLineFrom.IsSelected = true;
                }
                //  if the two shapes aren't the same
                else if (addingLineFrom.Number != shape.Number)
                {
                    System.Diagnostics.Debug.WriteLine("shapes are not the same");
                    //undoRedoController.AddAndExecute(new addLineCommand(Lines, new line() { from = addingLineFrom, to = shape }));
                    Lines.Add(new Line() { from = addingLineFrom, to = shape });

                    addingLineFrom.IsSelected = false;

                    isAddingLine = false;
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
                //shape.X = initialShapePosition.X;
                //shape.Y = initialShapePosition.Y;

                
        //        undoRedoController.AddAndExecute(new MoveShapeCommand(shape, mousePosition.X - initialMousePosition.X, mousePosition.Y - initialMousePosition.Y));

                // The mouse is released
                e.MouseDevice.Target.ReleaseMouseCapture();
            }
        }

        private ClassShape TargetShape(MouseEventArgs e)
        {
            var shapeVisualElement = (FrameworkElement)e.MouseDevice.Target;

            return (ClassShape)shapeVisualElement.DataContext;
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
