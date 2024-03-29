﻿using System;
using System.Collections.Generic;
using Awesomium.Core;
using Awesomium.Windows.Controls;
using Awesomium.Windows.Forms;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Markup;
using System.IO;
using System.Xml;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Windows.Interop;
using System.Windows.Threading;
using System.Threading;



namespace WpfApplication1
{
    class Widget
    {
        protected Canvas canvas;
        protected Grid LayoutRoot;

        protected WrapPanel window = new WrapPanel();
        protected Grid grid = new Grid();
        protected ScrollViewer scroller = new ScrollViewer();
        protected StackPanel instructions = new StackPanel();

        protected DispatcherTimer newTimer = new DispatcherTimer();
        private UIElement last;
        protected System.Windows.Shapes.Rectangle appTab =new System.Windows.Shapes.Rectangle();
        private int touchesOnTopPanel = 0;
        private int touchesOnWindow = 0;


        public Widget(Canvas cvs, Grid layoutRoot, System.Windows.Input.TouchEventArgs e)
        {
            LayoutRoot = layoutRoot;
            canvas = cvs;
            GlobalVariables.lastTouchTime = GlobalVariables.TotalTime;
            window.Height = GlobalVariables.widgetInitHeight;
            window.Width = GlobalVariables.widgetInitWidth;

            System.Windows.Media.Color maroon = (System.Windows.Media.Color)ColorConverter.ConvertFromString("#2E0000");
            window.Background = new SolidColorBrush(maroon);

            grid.Name = "WidgetGrid";
            
            //set orientation of window according to drag position
            double yCoordinate = LayoutRoot.ActualHeight / 2 - e.GetTouchPoint(canvas).Position.Y;
            double xCoordinate = e.GetTouchPoint(canvas).Position.X + - LayoutRoot.ActualWidth / 2;
            System.Windows.Media.Matrix matrix = new System.Windows.Media.Matrix(GlobalVariables.widgetInitScale, 0, 0, GlobalVariables.widgetInitScale, e.GetTouchPoint(canvas).Position.X - GlobalVariables.widgetInitWidth * GlobalVariables.widgetInitScale / 2, e.GetTouchPoint(canvas).Position.Y);
            matrix.RotateAt(CalculateRotationAngle(xCoordinate, yCoordinate), e.GetTouchPoint(canvas).Position.X, e.GetTouchPoint(canvas).Position.Y);
           // matrix.Translate((- GlobalVariables.widgetInitWidth * GlobalVariables.widgetInitScale / 2), 0);
            window.RenderTransform = new MatrixTransform(matrix);

            window.ManipulationDelta += window_ManipulationDelta;
            window.ManipulationStarting += window_ManipulationStarting;
            window.AddHandler(WrapPanel.TouchDownEvent, new EventHandler<TouchEventArgs>(window_TouchDown), true);
            window.AddHandler(WrapPanel.TouchUpEvent, new EventHandler<TouchEventArgs>(window_TouchUp), true);
            window.AddHandler(WrapPanel.TouchLeaveEvent, new EventHandler<TouchEventArgs>(window_TouchLeave), true);
            window.AddHandler(WrapPanel.ManipulationInertiaStartingEvent, new EventHandler<ManipulationInertiaStartingEventArgs>(window_ManipulationInertiaStarting), true);

            //Manipulation border
            DockPanel topTab = new DockPanel();
            topTab.AddHandler(DockPanel.TouchDownEvent, new EventHandler<TouchEventArgs>(topTab_TouchDown), true);
            topTab.Height = 30;
            topTab.Name = "TopTab";
            topTab.RenderTransform = new MatrixTransform(1, 0, 0, 1, 0, 0);
            topTab.VerticalAlignment = VerticalAlignment.Top;
            topTab.Margin = new Thickness(0);
            topTab.Background = new SolidColorBrush(maroon);
            topTab.HorizontalAlignment = HorizontalAlignment.Stretch;

            //app icon
            appTab.Name = "AppTab";   
            appTab.IsManipulationEnabled = false;
            appTab.Height = 30;
            appTab.Width = 30;
            appTab.HorizontalAlignment = HorizontalAlignment.Left;
            appTab.VerticalAlignment = VerticalAlignment.Center;
            topTab.Children.Add(appTab);

            //Spacer
            System.Windows.Shapes.Rectangle spacer = new System.Windows.Shapes.Rectangle();
            spacer.Height = 30;
            spacer.Width = 10;
            spacer.HorizontalAlignment = HorizontalAlignment.Left;
            spacer.VerticalAlignment = VerticalAlignment.Center;
            topTab.Children.Add(spacer);

            //manipulation text
            TextBlock manipulationText = new TextBlock();
            manipulationText.Text = "Touch here to manipulate";
            manipulationText.FontSize = 14;
            manipulationText.FontFamily = new FontFamily("Comic Sans MS");
            manipulationText.Foreground = Brushes.White;
            manipulationText.HorizontalAlignment = HorizontalAlignment.Center;
            manipulationText.VerticalAlignment = VerticalAlignment.Center;
            topTab.Children.Add(manipulationText);

            // Close Button
            System.Windows.Shapes.Rectangle closeTab = new System.Windows.Shapes.Rectangle();
            closeTab.Name = "CloseTab";
            ImageBrush closeIcon = new ImageBrush();    // image background for close tab
            closeIcon.ImageSource =
                new BitmapImage(
                    new Uri("pack://application:,,,/Images/closeIcon.png")
                );
            closeTab.IsManipulationEnabled = false;
            closeTab.AddHandler(System.Windows.Shapes.Rectangle.TouchDownEvent, new EventHandler<TouchEventArgs>(CloseTabTouchDown), true);
            closeTab.Height = 30;
            closeTab.Width = 30;
            closeTab.Fill = closeIcon;
            closeTab.HorizontalAlignment = HorizontalAlignment.Right;
            closeTab.VerticalAlignment = VerticalAlignment.Center;
            topTab.Children.Add(closeTab);

            // Instructions Panel
            instructions.Name = "InstructionsPanel";
            instructions.RenderTransform = new MatrixTransform(1, 0, 0, 1, 0, 0);
            instructions.Margin = new Thickness(0, 30, 0, 0);
            instructions.HorizontalAlignment = HorizontalAlignment.Stretch;
            instructions.VerticalAlignment = VerticalAlignment.Stretch;
            System.Windows.Shapes.Rectangle gesturesRect = new System.Windows.Shapes.Rectangle();
            gesturesRect.HorizontalAlignment = HorizontalAlignment.Center;
            gesturesRect.VerticalAlignment = VerticalAlignment.Center;
            gesturesRect.Width = 250;
            gesturesRect.Height = 270;
            gesturesRect.Name = "Gestures";
            ImageBrush gestures = new ImageBrush();    // image background for close tab
            gestures.ImageSource =
                new BitmapImage(
                    new Uri("pack://application:,,,/Images/handGestures.png")
                );
            gesturesRect.Fill = gestures;
            instructions.Visibility = Visibility.Hidden;
            instructions.Background = new SolidColorBrush(Colors.LightGray) { Opacity = 0.5 };
            instructions.Children.Add(gesturesRect);

            window.Children.Add(grid);
            canvas.Children.Add(window);
            grid.Children.Add(topTab);

            //Timer
            newTimer.Interval = TimeSpan.FromMilliseconds(Constants.closeInterval);
            newTimer.Tick += new EventHandler(onElapsedTimer);
            newTimer.Tag = window;
            newTimer.Start();
            GlobalVariables.timerList[window] = newTimer;

            //scroller settings
            scroller.ManipulationBoundaryFeedback += ManipulationBoundaryFeedbackHandler;
            scroller.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
            scroller.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            scroller.VerticalAlignment = VerticalAlignment.Top;
            scroller.HorizontalAlignment = HorizontalAlignment.Left;
            scroller.VerticalContentAlignment = VerticalAlignment.Stretch;
            scroller.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            scroller.Margin = new Thickness(0, 30, 0, 0);
            scroller.ManipulationBoundaryFeedback += ManipulationBoundaryFeedbackHandler;
            grid.ClipToBounds = true;
        }

        /*
         * Determines the angle of rotation for the window when a menu item is dragged and dropped to 
         * a specific location in the screen. 
         * 
         * Returns angle in degrees
         */
        public static double CalculateRotationAngle(double xCoordinate, double yCoordinate)
        {
            double angle = 0;
            if (xCoordinate > 0 && yCoordinate >= 0) // 1st quadrant
            {
                angle = Math.Atan(yCoordinate / xCoordinate);
                angle = 3 * Math.PI / 2 - angle;
            }
            if (xCoordinate < 0 && yCoordinate >= 0) // 2nd quadrant
            {
                angle = Math.Atan(yCoordinate / (-1 * xCoordinate));
                angle = Math.PI / 2 + angle;
            }
            if (xCoordinate < 0 && yCoordinate <= 0) // 3rd quadrant
            {
                angle = Math.Atan(yCoordinate / xCoordinate);
                angle = Math.PI / 2 - angle;
            }
            if (xCoordinate > 0 && yCoordinate <= 0) // 4rd quadrant
            {
                angle = Math.Atan((-1 * yCoordinate) / xCoordinate);
                angle = 3 * Math.PI / 2 + angle;
            }
            return angle * 180 / Math.PI;
        }

        protected void topTab_TouchDown(object sender, TouchEventArgs e)
        {
            UIElementCollection children = ((sender as DockPanel).Parent as Grid).Children;
            //Enable manipulation
            (((sender as DockPanel).Parent as Grid).Parent as WrapPanel).IsManipulationEnabled = true;
            touchesOnTopPanel++;

            for (int i = 0; i < children.Count; i++)
            {
                //Display instructions
                if (children[i] is StackPanel && (children[i] as StackPanel).Name == "InstructionsPanel")
                {
                    (children[i] as StackPanel).Visibility = Visibility.Visible;
                    // e.Handled = true;
                    break;
                }
            }
        }

        protected void CloseTabTouchDown(object sender, System.Windows.Input.TouchEventArgs e)
        {
            GlobalVariables.lastTouchTime = GlobalVariables.TotalTime;

            //stop timer, remove timer and remove window, decrement count
            (GlobalVariables.timerList[((((sender as System.Windows.Shapes.Rectangle).Parent as DockPanel).Parent as Grid).Parent as WrapPanel)]).Stop();
            GlobalVariables.timerList.Remove(sender);
            (((((sender as System.Windows.Shapes.Rectangle).Parent as DockPanel).Parent as Grid).Parent as WrapPanel).Parent as Canvas).Children.Remove(
                    ((((sender as System.Windows.Shapes.Rectangle).Parent as DockPanel).Parent as Grid).Parent as WrapPanel));
            GlobalVariables.count--;
        }

        protected void window_TouchDown(object sender, System.Windows.Input.TouchEventArgs e)
        {
            GlobalVariables.lastTouchTime = GlobalVariables.TotalTime;
            //reset timer
            GlobalVariables.timerList[sender].Interval = TimeSpan.FromMilliseconds(Constants.closeInterval);
            if(instructions.Visibility == Visibility.Visible) touchesOnWindow++;
        }

        /*
         * The touchUp event is not triggered on the topTab. It is triggered in the wrap panel (grandparent)
         * because isManipulationEnabled is true. A hit test is perfomed with the touched point and the topTab 
         * panel to disable the instructions panel visibility and disable isManipulationEnabled on the wrapPanel.
         * 
         */
        protected void window_TouchUp(object sender, System.Windows.Input.TouchEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("TouchUpEnter: touchesOnTopPanel: " + touchesOnTopPanel + " touchesOnWindow: " + touchesOnWindow);
            FrameworkElement element = e.Source as FrameworkElement;
            if (element is WrapPanel)
            {
                UIElementCollection windowChildren = (element as WrapPanel).Children;
                UIElementCollection gridChildren = (windowChildren[0] as Grid).Children;

                TouchPoint touchedPoint = e.GetTouchPoint(element as WrapPanel);

                for (int i = 0; i < gridChildren.Count; i++)
                {
                    if (gridChildren[i] is DockPanel && (gridChildren[i] as DockPanel).Name == "TopTab")
                    {
                        DockPanel topTabPanel = gridChildren[i] as DockPanel;
                        HitTestResult result = VisualTreeHelper.HitTest(topTabPanel, touchedPoint.Position);
                        if ((result != null && touchesOnTopPanel <= 1) || touchesOnWindow <= 1)
                        {
                            for (int j = 0; j < gridChildren.Count; j++)
                            {
                                if (gridChildren[j] is StackPanel && (gridChildren[j] as StackPanel).Name == "InstructionsPanel")
                                {
                                    (gridChildren[j] as StackPanel).Visibility = Visibility.Hidden;
                                    (element as WrapPanel).IsManipulationEnabled = false;
                                    touchesOnTopPanel = 0;
                                    touchesOnWindow = 0;
                                    e.Handled = true;
                                    break;
                                }
                            }
                        }
                        if (result != null && touchesOnTopPanel > 1) touchesOnTopPanel--;
                        break;
                    }
                }
            }
            if (touchesOnWindow > 1) touchesOnWindow--;
            System.Diagnostics.Debug.WriteLine("TouchUpLeave: touchesOnTopPanel: " + touchesOnTopPanel + " touchesOnWindow: " + touchesOnWindow);
        }


        protected void window_TouchLeave(object sender, System.Windows.Input.TouchEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("TouchLeaveEnter: touchesOnTopPanel: " + touchesOnTopPanel + " touchesOnWindow: " + touchesOnWindow);
            
            FrameworkElement element = e.Source as FrameworkElement;
            if (element is WrapPanel)
            {
                WrapPanel panel = element as WrapPanel;
                UIElementCollection windowChildren = panel.Children;
                UIElementCollection gridChildren = (windowChildren[0] as Grid).Children;
                TouchPoint touchedPoint = e.GetTouchPoint(panel);
                HitTestResult result = VisualTreeHelper.HitTest(panel, touchedPoint.Position);

                if (result == null && touchesOnWindow <= 1)
                {
                    for (int i = 0; i < gridChildren.Count; i++)
                    {
                        if (gridChildren[i] is DockPanel && (gridChildren[i] as DockPanel).Name == "TopTab")
                        {
                            for (int j = 0; j < gridChildren.Count; j++)
                            {
                                if (gridChildren[j] is StackPanel && (gridChildren[j] as StackPanel).Name == "InstructionsPanel")
                                {
                                    (gridChildren[j] as StackPanel).Visibility = Visibility.Collapsed;
                                    (element as WrapPanel).IsManipulationEnabled = false;
                                    touchesOnTopPanel = 0;
                                    touchesOnWindow = 0;
                                    e.Handled = true;
                                    break;
                                }
                            }
                        }
                        break;
                    }
                    if (touchesOnWindow > 1) touchesOnWindow--;
                }
                
            }     
            System.Diagnostics.Debug.WriteLine("TouchLeaveExit: touchesOnTopPanel: " + touchesOnTopPanel + " touchesOnWindow: " + touchesOnWindow);           
        }

        /**
         * This function manipulates the matrix transform of the widget. 
         * It performs scaling, rotation, and translation transformations. 
         * 
         */
        protected void window_ManipulationDelta(object sender, System.Windows.Input.ManipulationDeltaEventArgs e)
        {
            //this just gets the source. 
            // I cast it to FE because I wanted to use ActualWidth for Center. You could try RenderSize as alternate
            //this just gets the source. 
            // I cast it to FE because I wanted to use ActualWidth for Center. You could try RenderSize as alternate
            var element = e.Source as FrameworkElement;
            if (element != null)
            {
                //e.DeltaManipulation has the changes 
                // Scale is a delta multiplier; 1.0 is last size,  (so 1.1 == scale 10%, 0.8 = shrink 20%) 
                // Rotate = Rotation, in degrees


                // Pan = Translation, == Translate offset, in Device Independent Pixels 


                var deltaManipulation = e.DeltaManipulation;
                var matrix = ((MatrixTransform)element.RenderTransform).Matrix;
                // find the old center; arguaby this could be cached 
                System.Windows.Point center = new System.Windows.Point(element.ActualWidth / 2, element.ActualHeight / 2);
                // transform it to take into account transforms from previous manipulations 
                center = matrix.Transform(center);

                //this will be a Zoom. 
                // M11: width
                // M22: height
                var width = element.ActualWidth * deltaManipulation.Scale.X * matrix.M11;
                var height = element.ActualHeight * deltaManipulation.Scale.Y * matrix.M22;
                var m11 = matrix.M11;
                if (matrix.M11 < 0.3 && matrix.M11 > -0.3)
                {
                    width = element.ActualWidth * deltaManipulation.Scale.X * (matrix.M11 + GlobalVariables.widgetInitScale);
                    height = element.ActualHeight * deltaManipulation.Scale.Y * (matrix.M22 + GlobalVariables.widgetInitScale);
                }
                if (matrix.M11 > GlobalVariables.widgetInitScale)
                {
                    width = element.ActualWidth * deltaManipulation.Scale.X * (matrix.M11 - GlobalVariables.widgetInitScale);
                    height = element.ActualHeight * deltaManipulation.Scale.Y * (matrix.M22 - GlobalVariables.widgetInitScale);
                }
                if ((Math.Atan(matrix.M12 / matrix.M11) * 180 / Math.PI) == 90) width *= 10E17;
                if ((Math.Atan(matrix.M12 / matrix.M11) * 180 / Math.PI) == 270) width *= 10E17;

                // widget has been rotated >180 degrees
                if (width < 0) width *= -1;
                if (height < 0) height *= -1;

                double maxWidth = element.ActualWidth + 250;
                double minWidth = element.ActualWidth - 50;
                double maxHeight = element.ActualHeight + 250;
                double minHeight = element.ActualHeight - 50;

                System.Diagnostics.Debug.WriteLine("Manipulation matrix: " + matrix.ToString());         
                // maximum and minimum height and width. Only scale if it's within these values
                if (width >= minWidth && width <= maxWidth && height >= minHeight && height <= maxHeight)
                {
                    matrix.ScaleAt(deltaManipulation.Scale.X, deltaManipulation.Scale.Y, center.X, center.Y);
                }

                // Rotation 
                matrix.RotateAt(e.DeltaManipulation.Rotation, center.X, center.Y);
                //Translation (pan) 
                matrix.Translate(e.DeltaManipulation.Translation.X, e.DeltaManipulation.Translation.Y);

                ((MatrixTransform)element.RenderTransform).Matrix = matrix;

                e.Handled = true;

                // We are only checking boundaries during inertia 
                // in real world, we would check all the time 
                if (e.IsInertial)
                {
                    Rect containingRect = new Rect(((FrameworkElement)e.ManipulationContainer).RenderSize);

                    Rect shapeBounds = element.RenderTransform.TransformBounds(new Rect(element.RenderSize));

                    // Check if the element is completely in the window.
                    // If it is not and intertia is occuring, stop the manipulation.
                    if (e.IsInertial && !containingRect.Contains(shapeBounds))
                    {
                        //Report that we have gone over our boundary 
                        e.ReportBoundaryFeedback(e.DeltaManipulation);
                        // comment out this line to see the Window 'shake' or 'bounce' 
                        // similar to Win32 Windows when they reach a boundary; this comes for free in .NET 4                
                        e.Complete();
                    }
                }
            }
        }

        protected void ManipulationBoundaryFeedbackHandler(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }

        protected void window_ManipulationInertiaStarting(object sender, ManipulationInertiaStartingEventArgs e)
        {
            // Decrease the velocity of the Rectangle's movement by 
            // 10 inches per second every second.
            // (10 inches * 96 DIPS per inch / 1000ms^2)
            e.TranslationBehavior = new InertiaTranslationBehavior()
            {
                InitialVelocity = e.InitialVelocities.LinearVelocity,
                DesiredDeceleration = 7.0 * 96.0 / (1000.0 * 1000.0)
            };

            // Decrease the velocity of the Rectangle's resizing by 
            // 0.1 inches per second every second.
            // (0.1 inches * 96 DIPS per inch / (1000ms^2)
            e.ExpansionBehavior = new InertiaExpansionBehavior()
            {
                InitialVelocity = e.InitialVelocities.ExpansionVelocity,
                DesiredDeceleration = 0.1 * 96 / 1000.0 * 1000.0
            };

            // Decrease the velocity of the Rectangle's rotation rate by 
            // 2 rotations per second every second.
            // (2 * 360 degrees / (1000ms^2)
            e.RotationBehavior = new InertiaRotationBehavior()
            {
                InitialVelocity = e.InitialVelocities.AngularVelocity,
                DesiredDeceleration = 720 / (1000.0 * 1000.0)
            };
            e.Handled = true;
        }

        protected void window_ManipulationStarting(object sender, ManipulationStartingEventArgs e)
        {
            var uie = e.OriginalSource as UIElement;
            if (uie != null)
            {
                if (last != null) Canvas.SetZIndex(last, 0);
                Canvas.SetZIndex(uie, 2);
                last = uie;
            }
            e.ManipulationContainer = canvas;
            e.Handled = true;
        }

        protected void onElapsedTimer(Object sender, EventArgs args)
        {
            WrapPanel window = (((DispatcherTimer)sender).Tag as WrapPanel);
            GlobalVariables.timerList[window].Stop();
            ((window).Parent as Canvas).Children.Remove(window);
            GlobalVariables.timerList.Remove(sender);
            GlobalVariables.count--;
        }
    }
}