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
    class WidgetWeather : Widget
    {

        public WidgetWeather(Canvas c,Grid g, System.Windows.Input.TouchEventArgs e)
            : base(c, g, e)
        {
            ImageBrush appIcon = new ImageBrush();
            appIcon.ImageSource = new BitmapImage(
                new Uri("pack://application:,,,/Images/weather.gif")
            );
            appTab.Fill = appIcon;

            Image image = new Image();
            //image.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(bmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty,
            //                System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
            var uriSource = new Uri("C:/infoKiosk/KioskRepository/Info Kiosk/WpfApplication1/Images/weatherApp.png", UriKind.Absolute);

            //var uriSource = new Uri("pack://application:,,,/Images/" + "weatherApp.png");
            image.Source = new BitmapImage(uriSource);
            image.Height = 700;
            image.Width = 300;
            image.IsManipulationEnabled = false;
            scroller.Content = image;
            
            scroller.PanningMode = PanningMode.VerticalOnly;
            grid.Children.Add(scroller);
            grid.Children.Add(instructions);
        }
    }
}
