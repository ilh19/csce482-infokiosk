using System;
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
        protected Grid LayoutRoot;
        protected WrapPanel window = new WrapPanel();
        protected ScrollViewer scroller = new ScrollViewer();
        public StackPanel list = new StackPanel();

        protected DispatcherTimer newTimer = new DispatcherTimer();
        protected WebView webView;
        protected Canvas canvas;
        protected bool finishedLoading;
        protected bool finishedResizing;
        protected int imageHeight;
        UIElement last;

        protected ScrollData sdata;

        public Widget(Canvas c, Grid g, System.Windows.Input.TouchEventArgs e)
        {
            LayoutRoot = g;
            canvas = c;
            GlobalVariables.lastTouchTime = GlobalVariables.TotalTime;

            window.Height = 300;
            window.Width = 250;

            Grid grid = new Grid();
            grid.Name = "WidgetGrid";
            window.Children.Add(grid);

            System.Windows.Media.Color maroon = (System.Windows.Media.Color)ColorConverter.ConvertFromString("#2E0000");
            window.Background = new SolidColorBrush(maroon);

            double yCoordinate = LayoutRoot.ActualHeight / 2 - e.GetTouchPoint(canvas).Position.Y;
            double xCoordinate = e.GetTouchPoint(canvas).Position.X - LayoutRoot.ActualWidth / 2;

            System.Windows.Media.Matrix matrix = new System.Windows.Media.Matrix(GlobalVariables.widgetInitScale, 0, 0, GlobalVariables.widgetInitScale, e.GetTouchPoint(canvas).Position.X, e.GetTouchPoint(canvas).Position.Y);

            matrix.RotateAt(CalculateRotationAngle(xCoordinate, yCoordinate), e.GetTouchPoint(canvas).Position.X, e.GetTouchPoint(canvas).Position.Y);
            window.RenderTransform = new MatrixTransform(matrix);

            //window.Background = System.Windows.Media.Brushes.LightSlateGray;
            //window.RenderTransform = new MatrixTransform(1.2, 0.5, -0.5, 1.2, e.GetTouchPoint(canvas).Position.X, e.GetTouchPoint(canvas).Position.Y);
            //window.AddHandler(WrapPanel.ManipulationDeltaEvent, new EventHandler<ManipulationDeltaEventArgs>(window_ManipulationDelta), true);//("circle_ManipulationDelta");
            window.ManipulationDelta += window_ManipulationDelta;
            window.AddHandler(WrapPanel.ManipulationStartingEvent, new EventHandler<ManipulationStartingEventArgs>(window_ManipulationStarting), true);
            window.AddHandler(WrapPanel.TouchDownEvent, new EventHandler<TouchEventArgs>(window_TouchDown), true);
            window.AddHandler(WrapPanel.TouchUpEvent, new EventHandler<TouchEventArgs>(window_TouchUp), true);
            window.AddHandler(WrapPanel.ManipulationInertiaStartingEvent, new EventHandler<ManipulationInertiaStartingEventArgs>(window_ManipulationInertiaStarting), true);

            canvas.Children.Add(window);

            //Manipulation border
            DockPanel topTab = new DockPanel();
            topTab.AddHandler(DockPanel.TouchDownEvent, new EventHandler<TouchEventArgs>(topTab_TouchDown), true);
            topTab.Height = 30;
            topTab.Name = "TopTab";
            topTab.RenderTransform = new MatrixTransform(1, 0, 0, 1, 0, 0);
            topTab.VerticalAlignment = VerticalAlignment.Top;
            topTab.Margin = new Thickness(0);

            // Spacer
            System.Windows.Shapes.Rectangle spacer = new System.Windows.Shapes.Rectangle();
            spacer.Height = 30;
            spacer.Width = 30;
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
            grid.Children.Add(topTab);

            // Panning Window
            scroller.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
            scroller.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            scroller.VerticalAlignment = VerticalAlignment.Bottom;
            scroller.Margin = new Thickness(0, 30, 0, 0);
            scroller.PanningMode = PanningMode.VerticalOnly;
            scroller.IsManipulationEnabled = false;
            scroller.ManipulationBoundaryFeedback += ManipulationBoundaryFeedbackHandler;
            grid.Children.Add(scroller);

            //Timer
            newTimer.Interval = TimeSpan.FromMilliseconds(Constants.closeInterval);
            newTimer.Tick += new EventHandler(onElapsedTimer);
            newTimer.Tag = window;
            newTimer.Start();
            GlobalVariables.timerList[window] = newTimer;

            // Instructions Panel
            StackPanel instructions = new StackPanel();
            instructions.Name = "InstructionsPanel";

            instructions.RenderTransform = new MatrixTransform(1, 0, 0, 1, 0, 0);
            instructions.Margin = new Thickness(0, 30, 0, 0);
            instructions.HorizontalAlignment = HorizontalAlignment.Stretch;
            instructions.VerticalAlignment = VerticalAlignment.Stretch;
            TextBlock instructionsText = new TextBlock();
            instructionsText.Text = "Instructions \n Resize \n Rotate \n Translate";
            instructionsText.FontSize = 20;
            instructions.Visibility = Visibility.Collapsed;
            instructions.Background = new SolidColorBrush(Colors.LightGray) { Opacity = 0.5 };
            instructions.Children.Add(instructionsText);
            grid.Children.Add(instructions);
        }

        /*
         * Determines the angle of rotation for the window when a menu item is dragged and dropped to 
         * a specific location in the screen. 
         * 
         * Returns angle in radians
         */
        private double CalculateRotationAngle(double xCoordinate, double yCoordinate)
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

        protected void OnScrollDataReceived(object sender, ScrollDataEventArgs e)
        {
            sdata = e.ScrollData;
            webView.Resize(e.ScrollData.ContentWidth, e.ScrollData.ContentHeight);
            while (webView.IsResizing)
            {

            }
            webView.Render().SaveToJPEG("blabla1.jpg", 100);
            imageHeight = e.ScrollData.ContentHeight;
            finishedResizing = true;
        }

        protected void OnFinishLoading(object sender, EventArgs e)
        {
            finishedLoading = true;
        }

        protected void Initialize()
        {

        }

        protected void topTab_TouchUp(object sender, TouchEventArgs e)
        {

        }

        protected void topTab_TouchDown(object sender, TouchEventArgs e)
        {
            UIElementCollection children = ((sender as DockPanel).Parent as Grid).Children;
            //Enable manipulation
            (((sender as DockPanel).Parent as Grid).Parent as WrapPanel).IsManipulationEnabled = true;

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
        }

        /*
         * The touchUp event is not triggered on the topTab. It is triggered in the wrap panel (grandparent)
         * because isManipulationEnabled is true. A hit test is perfomed with the touched point and the topTab 
         * panel to disable the instructions panel visibility and disable isManipulationEnabled on the wrapPanel.
         * 
         */
        protected void window_TouchUp(object sender, System.Windows.Input.TouchEventArgs e)
        {
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
                        if (result != null)
                        {
                            for (int j = 0; j < gridChildren.Count; j++)
                            {
                                if (gridChildren[j] is StackPanel && (gridChildren[j] as StackPanel).Name == "InstructionsPanel")
                                {
                                    (gridChildren[j] as StackPanel).Visibility = Visibility.Collapsed;
                                    (element as WrapPanel).IsManipulationEnabled = false;
                                    e.Handled = true;
                                    break;
                                }
                            }
                        }
                        break;
                    }
                }
            }
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

                // widget has been rotated >180 degrees
                if (width < 0) width *= -1;
                if (height < 0) height *= -1;

                double maxWidth = element.ActualWidth + 250;
                double minWidth = element.ActualWidth - 50;
                double maxHeight = element.ActualHeight + 250;
                double minHeight = element.ActualHeight - 50;

                // maximum and minimum height and width
                if (width >= minWidth && width <= maxWidth && height >= minHeight && height <= maxHeight)
                {
                    matrix.ScaleAt(deltaManipulation.Scale.X, deltaManipulation.Scale.Y, center.X, center.Y);

                    /*  if (element is WrapPanel)
                      {
                          UIElementCollection windowChildren = (element as WrapPanel).Children;
                          UIElementCollection children = (windowChildren[0] as Grid).Children;  //get the grid
                          for (int i = 0; i < children.Count; i++)
                          {
                              if (children[i] is StackPanel && (children[i] as StackPanel).Name == "TopTab")
                              {
                                  var instructionsMatrix = ((MatrixTransform)(children[i] as StackPanel).RenderTransform).Matrix;
                                  instructionsMatrix.Scale(1, 1 / deltaManipulation.Scale.Y);
                                  ((MatrixTransform)(children[i] as StackPanel).RenderTransform).Matrix = instructionsMatrix;
                              }
                          }
                      }*/
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

            //canvas is the parent of the image starting the manipulation;
            //Container does not have to be parent, but that is the most common scenario
            e.ManipulationContainer = canvas;
            e.Handled = true;
            // you could set the mode here too 
            // e.Mode = ManipulationModes.All;        
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
