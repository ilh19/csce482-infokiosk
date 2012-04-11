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
    class WeatherWidget : Widget
    {
        protected WebView webView;

        public WeatherWidget(Canvas c,Grid g, System.Windows.Input.TouchEventArgs e)
            : base(c, g, e)
        {

            webView = WebCore.CreateWebView(300, 700);
            webView.LoadURL("http://theshinyspoonpay.appspot.com");
            webView.LoadCompleted += OnFinishLoading;

            while (!finishedLoading)
            {
                Thread.Sleep(100);
                WebCore.Update();
            }

            System.Drawing.Bitmap bmap = new System.Drawing.Bitmap(300, 700);
            webView.Render().DrawBuffer(ref bmap);
            Image image = new Image();
            image.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(bmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty,
                            System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
            image.Height = 700;
            image.Width = 300;
            image.IsManipulationEnabled = false;
            scroller.Content = image;
            //}

            scroller.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
            scroller.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            scroller.VerticalAlignment = VerticalAlignment.Bottom;
            scroller.Margin = new Thickness(0, 30, 0, 0);
            scroller.PanningMode = PanningMode.VerticalOnly;
            scroller.IsManipulationEnabled = false;
            scroller.ManipulationBoundaryFeedback += ManipulationBoundaryFeedbackHandler;
            grid.Children.Add(scroller);

            grid.Children.Add(instructions);
   
        }

    }
}
