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
    class TransitWidget : Widget
    {

        public TransitWidget(Canvas c, Grid g, System.Windows.Input.TouchEventArgs e)
            : base(c,g,e)
        {
           // webView = WebCore.CreateWebView(300, 1200);
           // webView.LoadURL("C:\\infoKiosk\\KioskRepository\\Info Kiosk\\WpfApplication1\\transit\\01.html");
           // webView.LoadCompleted += OnFinishLoading;
           // webView.ScrollDataReceived += new ScrollDataReceivedEventHandler(OnScrollDataReceived);
           //
           // while (!finishedLoading)
           // {
           //     Thread.Sleep(100);
           //     WebCore.Update();
           // }
           //
           // webView.Render().SaveToPNG("transit.png", true);
           // System.Drawing.Bitmap bmap = new System.Drawing.Bitmap(350, 1200);
           // webView.Render().DrawBuffer(ref bmap);
           // Image image = new Image();
           // image.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(bmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty,
           //                 System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
           // image.Height = 1200;
           // image.Width = 350;
           // image.IsManipulationEnabled = false;
           // scroller.Content = image;

            Image image = new Image();
            var uriSource = new Uri("pack://application:,,,/Images/" + "01.png");
            image.Source = new BitmapImage(uriSource);
            image.IsManipulationEnabled = false;
            image.Width = 210;

            scroller.Content = image;

           scroller.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
           scroller.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
           scroller.VerticalAlignment = VerticalAlignment.Bottom;
           scroller.HorizontalAlignment = HorizontalAlignment.Right;
           //scroller.Width = 200;
           scroller.Margin = new Thickness(40, 0, 0, 0);
           scroller.PanningMode = PanningMode.Both;
           scroller.IsManipulationEnabled = false;
           scroller.ManipulationBoundaryFeedback += ManipulationBoundaryFeedbackHandler;
           scroller.HorizontalContentAlignment = HorizontalAlignment.Stretch;
           contentGrid = new Grid();
           contentGrid.Margin = new Thickness(0, 30, 0, 0);
           contentGrid.Children.Add(scroller);
           grid.Children.Add(contentGrid);

           ScrollViewer listScroll = new ScrollViewer();
           ListBox list = new ListBox();
           list.Width = 40;
           list.Height = 270;

           System.Windows.Controls.Button route1 = new RouteButton("01.png", "Route 1");
           System.Windows.Controls.Button route3 = new RouteButton( "01.png","Route 3");
           System.Windows.Controls.Button route4e = new RouteButton("01.png", "Route 4E");
           System.Windows.Controls.Button route4n = new RouteButton("01.png", "Route 4N");
           System.Windows.Controls.Button route5 = new RouteButton( "01.png", "Route 5");
           System.Windows.Controls.Button route6 = new RouteButton( "01.png", "Route 6");
           System.Windows.Controls.Button route7 = new RouteButton( "01.png", "Route 7");
           System.Windows.Controls.Button route8 = new RouteButton( "01.png", "Route 8");
           System.Windows.Controls.Button route12 = new RouteButton("01.png", "Route 12");
           System.Windows.Controls.Button route15n =new RouteButton("01.png", "Route 15n");
           System.Windows.Controls.Button route15s =new RouteButton("01.png", "Route 15s");
           System.Windows.Controls.Button route22 = new RouteButton("01.png", "Route 22");
           System.Windows.Controls.Button route26 = new RouteButton("01.png", "Route 26");
           System.Windows.Controls.Button route27 = new RouteButton("01.png", "Route 27");
           System.Windows.Controls.Button route31 = new RouteButton("01.png", "Route 31");
           System.Windows.Controls.Button route33 = new RouteButton("01.png", "Route 33");
           System.Windows.Controls.Button route34 = new RouteButton("01.png", "Route 34");
           System.Windows.Controls.Button route36 = new RouteButton("01.png", "Route 36");

           list.Items.Add(route1);
           list.Items.Add(route3);
           list.Items.Add(route4e);
           list.Items.Add(route4n);
           list.Items.Add(route5);
           list.Items.Add(route6);
           list.Items.Add(route7);
           list.Items.Add(route8);
           list.Items.Add(route12);
           list.Items.Add(route15n);
           list.Items.Add(route15s);
           list.Items.Add(route22);
           list.Items.Add(route26);
           list.Items.Add(route27);
           list.Items.Add(route31);
           list.Items.Add(route33);
           list.Items.Add(route34);
           list.Items.Add(route36);

           foreach (RouteButton b in list.Items)
           {
               b.TouchDown +=new EventHandler<TouchEventArgs>(button_TouchDown);
           }

           list.HorizontalContentAlignment = HorizontalAlignment.Stretch;
           listScroll.Content = list;
           listScroll.VerticalAlignment = VerticalAlignment.Top;
           listScroll.HorizontalAlignment = HorizontalAlignment.Left;
           listScroll.VerticalContentAlignment = VerticalAlignment.Stretch;
           listScroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
           listScroll.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
           contentGrid.Children.Add(listScroll);
           grid.Children.Add(instructions);
        }

        protected void button_TouchDown(object sender, TouchEventArgs e)
        {
            scroller.Content = (sender as RouteButton).Image;
        }

    }
}
