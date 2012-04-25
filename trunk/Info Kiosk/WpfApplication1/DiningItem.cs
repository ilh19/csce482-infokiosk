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
        Image img = new Image();
        ButtonMapIt mapIt;

        public ButtonMapIt Button
        {
            get { return mapIt; }
            set { mapIt = value; }
        }

        public Image Image
        {
            get { return img; }
            set { img = value; }
        }

        public DiningItem(String str, String mapString)
        {
            if(mapString.Equals("ETED"))
            mapIt = new ButtonMapIt(2206,858);
            if (mapString.Equals("langford"))
            mapIt = new ButtonMapIt(2358, 1287);
            if (mapString.Equals("WestLibrary"))
            mapIt = new ButtonMapIt(1192, 2064);
            if (mapString.Equals("Sbisa"))
            mapIt = new ButtonMapIt(1814, 1475);
            if (mapString.Equals("Underground"))
            mapIt = new ButtonMapIt(1741, 1461);
            if (mapString.Equals("CE"))
            mapIt = new ButtonMapIt(2146, 1155);

            if (mapString.Equals("Agri"))
            mapIt = new ButtonMapIt(1130, 2708);
            if (mapString.Equals("Duncan"))
            mapIt = new ButtonMapIt(2545, 1999);
            if (mapString.Equals("Commons"))
            mapIt = new ButtonMapIt(2523, 1669);
            if (mapString.Equals("Across Commons"))
            mapIt = new ButtonMapIt(2436, 1536);
            if (mapString.Equals("Evans"))
            mapIt = new ButtonMapIt(2245, 1466);
            if (mapString.Equals("Pavilion"))
            mapIt = new ButtonMapIt(2319, 1506);

            if (mapString.Equals("Bush"))
            mapIt = new ButtonMapIt(895, 3744);
            if (mapString.Equals("Rec"))
            mapIt = new ButtonMapIt(1900, 2475);
            if (mapString.Equals("Vet"))
            mapIt = new ButtonMapIt(960, 1920);
            if (mapString.Equals("AgCafe"))
            mapIt = new ButtonMapIt(1290, 2164);
            if (mapString.Equals("Rudder"))
            mapIt = new ButtonMapIt(2516, 1869);
            if (mapString.Equals("MSC"))
            mapIt = new ButtonMapIt(2005, 1984);






            var uriSource = new Uri("pack://application:,,,/Images/dining/" + str);
            img.Source = new BitmapImage(uriSource);
            img.Width = 250;

            mapIt.VerticalAlignment = VerticalAlignment.Bottom;
            mapIt.HorizontalAlignment = HorizontalAlignment.Center;
            Children.Add(img);
            Children.Add(mapIt);
        }

        public DiningItem(String str)
        {
            var uriSource = new Uri("pack://application:,,,/Images/dining/" + str);
            img.Source = new BitmapImage(uriSource);
            img.Width = 250;
            Children.Add(img);
        }

    }
}
