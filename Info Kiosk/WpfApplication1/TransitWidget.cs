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

            canvas = c;
            //using (webView = WebCore.CreateWebView(300, 800))
            webView = WebCore.CreateWebView(300, 1500);
            //{
            webView.LoadURL("C:\\infoKiosk\\KioskRepository\\Info Kiosk\\WpfApplication1\\transit\\01.html");
            webView.LoadCompleted += OnFinishLoading;
            webView.ScrollDataReceived += new ScrollDataReceivedEventHandler(OnScrollDataReceived);

            while (!finishedLoading)
            {
                Thread.Sleep(100);
                WebCore.Update();
            }

            //webView.Render().SaveToJPEG("test.jpg", 10);

            // webView.RequestScrollData();
            // while (!finishedResizing)
            // {
            //     Thread.Sleep(100);
            //     WebCore.Update();
            // }

            System.Drawing.Bitmap bmap = new System.Drawing.Bitmap(300, 1500);
            webView.Render().DrawBuffer(ref bmap);
            Image image = new Image();
            image.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(bmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty,
                            System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
            image.Height = 1500;
            image.Width = 300;
            image.IsManipulationEnabled = false;
            scroller.Content = image;
            //}


        }

    }
}
