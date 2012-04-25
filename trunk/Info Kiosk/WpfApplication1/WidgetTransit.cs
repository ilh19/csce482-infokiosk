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
    class WidgetTransit : Widget
    {

        public WidgetTransit(Canvas c, Grid g, System.Windows.Input.TouchEventArgs e)
            : base(c,g,e)
        {
           ImageBrush appIcon = new ImageBrush();
           appIcon.ImageSource = new BitmapImage(
           new Uri("pack://application:,,,/Images/transit.gif")
           );
           appTab.Fill = appIcon;

           //scroller.VerticalAlignment = VerticalAlignment.Bottom;
           //scroller.HorizontalAlignment = HorizontalAlignment.Right;
           scroller.Margin = new Thickness(40, 0, 0, 0);
           scroller.PanningMode = PanningMode.Both;
           scroller.HorizontalContentAlignment = HorizontalAlignment.Stretch;

           ScrollViewer listScroll = new ScrollViewer();
           ListBox list = new ListBox();
           list.Width = 40;
           list.Height = 270;
           Grid contentGrid = new Grid();
           contentGrid.Margin = new Thickness(0, 30, 0, 0);

           contentGrid.Children.Add(scroller);
           grid.Children.Add(contentGrid);
           contentGrid.Children.Add(listScroll);
           grid.Children.Add(instructions);

           ButtonRoute route1 = new ButtonRoute("01.png", "Route 1");
           ButtonRoute route3 = new ButtonRoute("03.png", "Route 3");
           ButtonRoute route4e = new ButtonRoute("04E.png", "Route 4E");
           ButtonRoute route4n = new ButtonRoute("04W.png", "Route 4W");
           ButtonRoute route5 = new ButtonRoute("05.png", "Route 5");
           ButtonRoute route6 = new ButtonRoute("06.png", "Route 6");
           ButtonRoute route7 = new ButtonRoute("07.png", "Route 7");
           ButtonRoute route8 = new ButtonRoute("08.png", "Route 8");
           ButtonRoute route12 = new ButtonRoute("12.png", "Route 12");
           ButtonRoute route15n = new ButtonRoute("15N.png", "Route 15n");
           ButtonRoute route15s = new ButtonRoute("15S.png", "Route 15s");
           ButtonRoute route22 = new ButtonRoute("22.png", "Route 22");
           ButtonRoute route26 = new ButtonRoute("26.png", "Route 26");
           ButtonRoute route27 = new ButtonRoute("27.png", "Route 27");
           ButtonRoute route31 = new ButtonRoute("31.png", "Route 31");
           ButtonRoute route33 = new ButtonRoute("33.png", "Route 33");
           ButtonRoute route34 = new ButtonRoute("34.png", "Route 34");
           ButtonRoute route36 = new ButtonRoute("36.png", "Route 36");
           scroller.Content = route1.Image;

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

           foreach (ButtonRoute b in list.Items)
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
           
        }

        protected void button_TouchDown(object sender, TouchEventArgs e)
        {
            scroller.Content = (sender as ButtonRoute).Image;
            scroller.ScrollToHorizontalOffset(0);
            scroller.ScrollToVerticalOffset(0);
        }

    }
}
