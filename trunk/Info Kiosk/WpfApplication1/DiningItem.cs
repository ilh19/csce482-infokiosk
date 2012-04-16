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
    class DiningItem : StackPanel
    {
        Image img;
        Button mapIt = new Button();
        //public DockPanel dock = new DockPanel();

        public DiningItem(String str)
        {   
            img = new Image();
            var uriSource = new Uri("pack://application:,,,/Images/" + str);
            img.Source = new BitmapImage(uriSource);
            img.Width = 250;
            mapIt.Content = "MAP IT";
            //on clicking mapIt, open new window with map
            mapIt.VerticalAlignment = VerticalAlignment.Bottom;
            mapIt.HorizontalAlignment = HorizontalAlignment.Center;
            Children.Add(img);
            Children.Add(mapIt);
        }
    }
}
