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

    class MapWidget : Widget
    {
        //protected Map map = new Map();
        Image image = new Image();
        int touchCount = 0;

        public MapWidget(Canvas c, Grid g, System.Windows.Input.TouchEventArgs e)
          : base(c,g,e)
        {
            ImageBrush appIcon = new ImageBrush();
            appIcon.ImageSource = new BitmapImage(
                new Uri("pack://application:,,,/Images/map.gif")
            );
            appTab.Fill = appIcon;

            var uriSource = new Uri("pack://application:,,,/Images/" + "currentMap2.png");
            image.Source = new BitmapImage(uriSource);
            //image.IsManipulationEnabled = false;
            image.Width = 250;

            scroller.Content = image;
           //scroller.PanningMode = PanningMode.Both;
           //scroller.IsManipulationEnabled = false;
           //scroller.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
           //scroller.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
           //scroller.VerticalAlignment = VerticalAlignment.Bottom;
           //scroller.HorizontalAlignment = HorizontalAlignment.Right;


            scroller.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
            scroller.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            scroller.VerticalAlignment = VerticalAlignment.Bottom;
            scroller.Margin = new Thickness(0, 30, 0, 0);
            scroller.PanningMode = PanningMode.Both;
            scroller.ManipulationBoundaryFeedback += ManipulationBoundaryFeedbackHandler;
            //scroller.ClipToBounds = true;

            scroller.ManipulationStarting += new EventHandler<ManipulationStartingEventArgs>(map_ManipulationStarting);
            scroller.ManipulationDelta += new EventHandler<ManipulationDeltaEventArgs>(map_ManipulationDelta);
            scroller.ManipulationCompleted += new EventHandler<ManipulationCompletedEventArgs>(map_ManipulationCompleted);
            scroller.TouchDown += new EventHandler<TouchEventArgs>(img_TouchDown);
            scroller.TouchUp += new EventHandler<TouchEventArgs>(img_TouchUp);

            //contentGrid = new Grid();
            //contentGrid.Margin = new Thickness(0, 30, 0, 0);
            grid.Children.Add(scroller);
            grid.Children.Add(instructions);
            //grid.Children.Add(contentGrid);


          /*  map.VerticalAlignment = VerticalAlignment.Bottom;
            map.Margin = new Thickness(0, 30, 0, 0);
            map.Height = 500;
            map.Width = 250;
            grid.Children.Add(map);
            grid.Children.Add(instructions);*/
        }


        void img_TouchDown(object sender, TouchEventArgs e)
        {
            touchCount++;
            System.Diagnostics.Debug.WriteLine("Touch: " + touchCount);
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

                //Zoom. 
                matrix.ScaleAt(deltaManipulation.Scale.X, deltaManipulation.Scale.Y, center.X, center.Y);

                //matrix.Translate(e.DeltaManipulation.Translation.X, e.DeltaManipulation.Translation.Y);

                element.LayoutTransform = new MatrixTransform(matrix);
                e.Handled = true;
             //   System.Diagnostics.Debug.WriteLine("Matrix: " + matrix.ToString());
            }
        }

        private void map_ManipulationCompleted(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            touchCount = 0;
        }
    }
}
