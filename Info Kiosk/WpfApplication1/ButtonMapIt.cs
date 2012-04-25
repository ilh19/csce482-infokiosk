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
    class ButtonMapIt : Button
    {
        Image img = new Image();
        int xCordinate = 0;
        int yCordinate = 0;

        public Image Image
        {
            get { return img; }
            set { img = value; }
        }

        public int X
        {
            get { return xCordinate; }
            set { xCordinate = value; }
        }

        public int Y
        {
            get { return yCordinate; }
            set { yCordinate = value; }
        }

        public ButtonMapIt(int x, int y)//String imgName)
            : base()
        {
            var uriSource = new Uri("pack://application:,,,/Images/" + "currentMapDining.png");
            img.Source = new BitmapImage(uriSource);
            img.VerticalAlignment = VerticalAlignment.Center;
            img.HorizontalAlignment = HorizontalAlignment.Center;
            img.Width = 1195;
            xCordinate = x;
            yCordinate = y;
            //FontSize = 6;
            Content = "MAP IT";
        }
    }

}
