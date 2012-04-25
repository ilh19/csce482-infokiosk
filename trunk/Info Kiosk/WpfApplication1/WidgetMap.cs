using Awesomium.Core;
using Awesomium.Windows.Controls;
using Awesomium.Windows.Forms;
using System;
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

    class WidgetMap : Widget
    {
        Image image = new Image();
        int touchCount = 0;
        double xCoord = 0;
        double yCoord = 0;
        double oldHeight = 0;
        double oldWidth = 0;

        public WidgetMap(Canvas c, Grid g, System.Windows.Input.TouchEventArgs e)
          : base(c,g,e)
        {
            ImageBrush appIcon = new ImageBrush();
            appIcon.ImageSource = new BitmapImage(
                new Uri("pack://application:,,,/Images/map.gif")
            );
            appTab.Fill = appIcon;

            var uriSource = new Uri("pack://application:,,,/Images/" + "currentMap2.png");
            image.Source = new BitmapImage(uriSource);
            //image.Width = 250;
            image.Width = 1195.5;
            image.Height = 1191.3;
            

            scroller.Content = image;
            scroller.PanningMode = PanningMode.Both;
            scroller.ManipulationBoundaryFeedback += ManipulationBoundaryFeedbackHandler;

            scroller.ManipulationStarting += new EventHandler<ManipulationStartingEventArgs>(map_ManipulationStarting);
            scroller.ManipulationDelta += new EventHandler<ManipulationDeltaEventArgs>(map_ManipulationDelta);
            scroller.ManipulationCompleted += new EventHandler<ManipulationCompletedEventArgs>(map_ManipulationCompleted);
            scroller.TouchDown += new EventHandler<TouchEventArgs>(img_TouchDown);
            scroller.TouchUp += new EventHandler<TouchEventArgs>(img_TouchUp);

            grid.Children.Add(scroller);
            grid.Children.Add(instructions);
        }

        void img_TouchDown(object sender, TouchEventArgs e)
        {
            touchCount++;
            if (touchCount == 1)
            {
                scroller.PanningMode = PanningMode.Both;
                image.IsManipulationEnabled = true;
                image.ReleaseTouchCapture(e.TouchDevice);
                scroller.CaptureTouch(e.TouchDevice);
            }
            else
            {
                foreach (TouchDevice device in scroller.TouchesOver)
                {
                    scroller.ReleaseTouchCapture(device);
                    image.CaptureTouch(device);
                }
                scroller.PanningMode = PanningMode.None;
                image.IsManipulationEnabled = true;
            }
            e.Handled = true;
        }

        private void img_TouchUp(object sender, TouchEventArgs e)
        {
            if (touchCount > 0) 
                touchCount--;
            if(touchCount==0)
                image.IsManipulationEnabled = false;
            scroller.PanningMode = PanningMode.Both;
            e.Handled = true;
        }

        private void map_ManipulationStarting(object sender, System.Windows.Input.ManipulationStartingEventArgs e)
        {
            e.ManipulationContainer = scroller;
            e.Handled = true;
        }

        private void map_ManipulationDelta(object sender, System.Windows.Input.ManipulationDeltaEventArgs e)
        {
            var element = e.Source as FrameworkElement;
            if (element != null)
            {
                var deltaManipulation = e.DeltaManipulation;
                var matrix = ((MatrixTransform)element.LayoutTransform).Matrix;
                System.Windows.Point center = new System.Windows.Point(element.ActualWidth / 2, element.ActualHeight / 2);
                center = matrix.Transform(center);

                var width = element.ActualWidth * deltaManipulation.Scale.X * matrix.M11;
                var height = element.ActualHeight * deltaManipulation.Scale.Y * matrix.M22;

                //Zoom. 
                double maxWidth = element.ActualWidth + 2000;
                double minWidth = element.ActualWidth - 700;
                double maxHeight = element.ActualHeight + 2000;
                double minHeight = element.ActualHeight - 700;
                //
                Point touchPointScroller = new Point(e.ManipulationOrigin.X, e.ManipulationOrigin.Y); //relative to scroller?
                //Point touchPointMap = new Point(scroller.HorizontalOffset*deltaManipulation.Scale.X

                if (width >= minWidth && width <= maxWidth && height >= minHeight && height <= maxHeight)
                {
                    Console.Write("X: " +deltaManipulation.Scale.X + ", Y: "+deltaManipulation.Scale.Y);
                    oldHeight = (element as Image).Height;
                    oldWidth = (element as Image).Width; 
                    matrix.ScaleAt(deltaManipulation.Scale.X, deltaManipulation.Scale.Y, center.X, center.Y);
                    //scroller.ScrollToHorizontalOffset((oldWidth));
                    //scroller.ScrollToVerticalOffset((oldHeight));
                    scroller.ScrollToVerticalOffset(scroller.VerticalOffset*deltaManipulation.Scale.Y);
                    scroller.ScrollToHorizontalOffset(scroller.HorizontalOffset*deltaManipulation.Scale.X);

                    //scroller.scroll
                    
                }
                //matrix.ScaleAt(deltaManipulation.Scale.X, deltaManipulation.Scale.Y, center.X, center.Y);

                element.LayoutTransform = new MatrixTransform(matrix);




                e.Handled = true;
            }
        }


        private void map_ManipulationCompleted(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            touchCount = 0;
        }
    }
}
