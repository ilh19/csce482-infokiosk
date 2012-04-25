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
    class ButtonRoute : Button
    {
        Image img = new Image();
        public Image Image
        {
            get { return img;  }
            set { img = value; }
        }

        public ButtonRoute(String imgName, String buttonTitle)
            : base()
        {
            var uriSource = new Uri("pack://application:,,,/Images/" + imgName);
            img.Source = new BitmapImage(uriSource);
            img.Width = 210;

            FontSize = 6;
            Content = buttonTitle;
        }


    }


}
