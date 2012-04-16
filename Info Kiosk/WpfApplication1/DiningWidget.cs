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
    class DiningWidget : Widget
    {
        public DiningWidget(Canvas c,Grid g, System.Windows.Input.TouchEventArgs e)
            : base(c,g,e)
        {
            //webView = WebCore.CreateWebView(300, 4000);
            //webView.LoadURL("C:\\infoKiosk\\KioskRepository\\Info Kiosk\\WpfApplication1\\webpages\\dining.htm");
            //webView.LoadCompleted += OnFinishLoading;
            //webView.ScrollDataReceived += new ScrollDataReceivedEventHandler(OnScrollDataReceived);
            //
            //while (!finishedLoading)
            //{
            //    Thread.Sleep(100);
            //    WebCore.Update();
            //}

            //System.Drawing.Bitmap bmap = new System.Drawing.Bitmap(300, 4000);
            //webView.Render().DrawBuffer(ref bmap);
            //Image image = new Image();
            //image.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(bmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty,
            //                System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
            //image.Height = 4000;
            //image.Width = 300;
            //image.IsManipulationEnabled = false;
            //scroller.Content = image;
            //}

            scroller.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
            scroller.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            scroller.VerticalAlignment = VerticalAlignment.Bottom;
            scroller.Margin = new Thickness(0, 30, 0, 0);
            scroller.PanningMode = PanningMode.VerticalOnly;
            scroller.IsManipulationEnabled = false;
            scroller.ManipulationBoundaryFeedback += ManipulationBoundaryFeedbackHandler;

            StackPanel list = new StackPanel();
            //list.Height = 270;

            DiningItem item1 = new DiningItem("1 sidewalk.png");
            list.Children.Add(item1);
            DiningItem item2 = new DiningItem("2.png");
            list.Children.Add(item2);
            DiningItem item3 = new DiningItem("3.png");
            list.Children.Add(item3);
            DiningItem item4 = new DiningItem("4.png");
            list.Children.Add(item4);
            DiningItem item5 = new DiningItem("5.png");
            list.Children.Add(item5);
            DiningItem item6 = new DiningItem("6.png");
            list.Children.Add(item6);
            DiningItem item7 = new DiningItem("7.png");
            list.Children.Add(item7);
            DiningItem item8 = new DiningItem("8.png");
            list.Children.Add(item8);

            scroller.Content = list;
            grid.Children.Add(scroller);

            grid.Children.Add(instructions);
        }

    }
}
