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
    class WidgetDining : Widget
    {
        //warning, the code below is ugly

        TabControl location = new TabControl();
        int touchCount = 0;
        Image mapBackButton = new Image();

        public WidgetDining(Canvas c,Grid g, System.Windows.Input.TouchEventArgs e)
            : base(c,g,e)
        {
            ImageBrush appIcon = new ImageBrush();
            appIcon.ImageSource = new BitmapImage(
                        new Uri("pack://application:,,,/Images/dining.gif")
            );
            appTab.Fill = appIcon;

            mapBackButton.HorizontalAlignment = HorizontalAlignment.Left;
            mapBackButton.VerticalAlignment = VerticalAlignment.Top;
            mapBackButton.Margin = new Thickness(0, 30, 0, 0);
            mapBackButton.Visibility = Visibility.Collapsed; 
            mapBackButton.Width = 30;
            mapBackButton.Height = 30;
            mapBackButton.Source = new BitmapImage(
                        new Uri("pack://application:,,,/Images/arrow_back.png"));
            mapBackButton.AddHandler(Image.TouchDownEvent, new EventHandler<TouchEventArgs>(backbButton_TouchDown), true);

            

            StackPanel listNorth = new StackPanel();
            StackPanel listSouth = new StackPanel();
            StackPanel listWest = new StackPanel();
            StackPanel listCentral = new StackPanel();

            scroller.PanningMode = PanningMode.VerticalOnly;

            window.Background = new SolidColorBrush(Colors.White);

            //Adding tabs
            TabItem northTab = new TabItem();
            northTab.Header = "North";
            northTab.Content = listNorth;
            TabItem southTab = new TabItem();
            southTab.Header = "South";
            southTab.Content = listSouth;
            TabItem westTab = new TabItem();
            westTab.Header = "West";
            westTab.Content = listWest;
            TabItem centralTab = new TabItem();
            centralTab.Header = "Central";
            centralTab.Content = listCentral;
            location.Items.Add(northTab);
            location.Items.Add(southTab);
            location.Items.Add(westTab);
            location.Items.Add(centralTab);

            //NORTH DINING//
            DiningItem north = new DiningItem("north.png");
            listNorth.Children.Add(north);
            DiningItem nitem1 = new DiningItem("north/n1.png", "ETED");
            listNorth.Children.Add(nitem1);
            DiningItem nitem2 = new DiningItem("north/n2.png", "langford");
            listNorth.Children.Add(nitem2);
            DiningItem nitem3 = new DiningItem("north/n3.png", "Underground");
            listNorth.Children.Add(nitem3);
            DiningItem nitem4 = new DiningItem("north/n4.png", "Sbisa");
            listNorth.Children.Add(nitem4);
            DiningItem nitem5 = new DiningItem("north/n5.png", "Underground");
            listNorth.Children.Add(nitem5);
            DiningItem nitem6 = new DiningItem("north/n6.png", "Underground");//duplicate
            listNorth.Children.Add(nitem6);
            DiningItem nitem7 = new DiningItem("north/n7.png", "CE");
            listNorth.Children.Add(nitem7);
            DiningItem nitem8 = new DiningItem("north/n8.png", "Underground");
            listNorth.Children.Add(nitem8);

            //SOUTH DINING//
            DiningItem south = new DiningItem("south.png");
            listSouth.Children.Add(south);
            DiningItem sitem1 = new DiningItem("south/s01.png", "Commons");
            listSouth.Children.Add(sitem1);
            DiningItem sitem2 = new DiningItem("south/s02.png", "Duncan");
            listSouth.Children.Add(sitem2);
            DiningItem sitem3 = new DiningItem("south/s03.png", "Commons");
            listSouth.Children.Add(sitem3);
            DiningItem sitem4 = new DiningItem("south/s04.png", "Commons");
            listSouth.Children.Add(sitem4);
            DiningItem sitem5 = new DiningItem("south/s05.png", "Across Commons");
            listSouth.Children.Add(sitem5);
            DiningItem sitem6 = new DiningItem("south/s06.png", "Evans");
            listSouth.Children.Add(sitem6);
            DiningItem sitem7 = new DiningItem("south/s07.png", "Pavilion");
            listSouth.Children.Add(sitem7);
            DiningItem sitem8 = new DiningItem("south/s08.png", "Commons");
            listSouth.Children.Add(sitem8);

            //WEST DINING//
            DiningItem west = new DiningItem("west.png");
            listWest.Children.Add(west);
            DiningItem witem1 = new DiningItem("west/w01.png", "Agri");
            listWest.Children.Add(witem1);
            DiningItem witem2 = new DiningItem("west/w02.png", "WestLibrary");
            listWest.Children.Add(witem2);
            DiningItem witem3 = new DiningItem("west/w03.png", "Bush");
            listWest.Children.Add(witem3);
            DiningItem witem4 = new DiningItem("west/w04.png", "Rec");
            listWest.Children.Add(witem4);
            DiningItem witem5 = new DiningItem("west/w05.png", "Vet");
            listWest.Children.Add(witem5);
            DiningItem witem6 = new DiningItem("west/w06.png", "AgCafe");
            listWest.Children.Add(witem6);

            //CENTRAL DINING//
            DiningItem central = new DiningItem("central.png");
            listCentral.Children.Add(central);
            DiningItem citem1 = new DiningItem("central/c01.png", "Rudder");
            listCentral.Children.Add(citem1);
            DiningItem citem2 = new DiningItem("central/c02.png", "Rudder");
            listCentral.Children.Add(citem2);
            //later buildings listed as MSC


            scroller.ManipulationStarting += new EventHandler<ManipulationStartingEventArgs>(map_ManipulationStarting);
            scroller.ManipulationDelta += new EventHandler<ManipulationDeltaEventArgs>(map_ManipulationDelta);
            scroller.ManipulationCompleted += new EventHandler<ManipulationCompletedEventArgs>(map_ManipulationCompleted);

            foreach (DiningItem item in listNorth.Children)
            {
                if (item.Button != null)
                {
                    item.Button.TouchDown += new EventHandler<TouchEventArgs>(mapIt_TouchDown);
                }
            }
            foreach (DiningItem item in listCentral.Children)
            {
                if (item.Button != null)
                {
                    item.Button.TouchDown += new EventHandler<TouchEventArgs>(mapIt_TouchDown);
                }
            }
            foreach (DiningItem item in listSouth.Children)
            {
                if (item.Button != null)
                {
                    item.Button.TouchDown += new EventHandler<TouchEventArgs>(mapIt_TouchDown);
                }
            }
            foreach (DiningItem item in listWest.Children)
            {
                if (item.Button != null)
                {
                    item.Button.TouchDown += new EventHandler<TouchEventArgs>(mapIt_TouchDown);
                }
            }
            scroller.Content = location;
            grid.Children.Add(scroller);
            grid.Children.Add(mapBackButton);
            grid.Children.Add(instructions);
        }

        public void mapIt_TouchDown(object sender, TouchEventArgs e)
        {
            scroller.Content = (sender as ButtonMapIt).Image;
            scroller.TouchDown += new EventHandler<TouchEventArgs>(img_TouchDown);
            scroller.TouchUp += new EventHandler<TouchEventArgs>(img_TouchUp);
            
            scroller.ScrollToHorizontalOffset((sender as ButtonMapIt).X*0.3-75);
            scroller.ScrollToVerticalOffset((sender as ButtonMapIt).Y*0.3-150);

            scroller.PanningMode = PanningMode.None;
            scroller.IsManipulationEnabled = true;
            (sender as ButtonMapIt).Image.IsManipulationEnabled = true;
            mapBackButton.Visibility = Visibility.Visible;
        }

        void backbButton_TouchDown(object sender, TouchEventArgs e)
        {
            scroller.Content = location;
            grid.Children.Remove(sender as Button);
            scroller.PanningMode = PanningMode.VerticalOnly;
            mapBackButton.Visibility = Visibility.Collapsed;
        }

        void img_TouchDown(object sender, TouchEventArgs e)
        {
            if (scroller.Content is Image)
            {
                touchCount++;
                if (touchCount == 1)
                {
                    scroller.PanningMode = PanningMode.Both;
                    (scroller.Content as Image).IsManipulationEnabled = true;
                    (scroller.Content as Image).ReleaseTouchCapture(e.TouchDevice);
                    scroller.CaptureTouch(e.TouchDevice);
                }
                else
                {
                    foreach (TouchDevice device in scroller.TouchesOver)
                    {
                        scroller.ReleaseTouchCapture(device);
                        (scroller.Content as Image).CaptureTouch(device);
                    }
                    scroller.PanningMode = PanningMode.None;
                    (scroller.Content as Image).IsManipulationEnabled = true;
                }
                e.Handled = true;
            }
        }

        private void img_TouchUp(object sender, TouchEventArgs e)
        {
            if (scroller.Content is Image)
            {
                if (touchCount > 0)
                    touchCount--;
                if (touchCount == 0)
                    (scroller.Content as Image).IsManipulationEnabled = false;

                scroller.PanningMode = PanningMode.Both;
                e.Handled = true;
            }
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
                var width = element.ActualWidth * deltaManipulation.Scale.X * matrix.M11;
                var height = element.ActualHeight * deltaManipulation.Scale.Y * matrix.M22;

                double maxWidth = element.ActualWidth + 2000;
                double minWidth = element.ActualWidth - 700;
                double maxHeight = element.ActualHeight + 2000;
                double minHeight = element.ActualHeight - 700;
                //
                if (width >= minWidth && width <= maxWidth && height >= minHeight && height <= maxHeight)
                {
                    matrix.ScaleAt(deltaManipulation.Scale.X, deltaManipulation.Scale.Y, center.X, center.Y);
                }
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
