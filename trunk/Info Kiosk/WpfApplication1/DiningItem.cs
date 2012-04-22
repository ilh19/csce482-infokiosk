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
        MapItButton mapIt;

        public MapItButton Button
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
            mapIt = new MapItButton(2206,858);
            if (mapString.Equals("langford"))
            mapIt = new MapItButton(2358, 1287);
            if (mapString.Equals("WestLibrary"))
            mapIt = new MapItButton(1192, 2064);
            if (mapString.Equals("Sbisa"))
            mapIt = new MapItButton(1814, 1475);
            if (mapString.Equals("Underground"))
            mapIt = new MapItButton(1741, 1461);
            if (mapString.Equals("CE"))
            mapIt = new MapItButton(2146, 1155);

            if (mapString.Equals("Agri"))
            mapIt = new MapItButton(1130, 2708);
            if (mapString.Equals("Duncan"))
            mapIt = new MapItButton(2545, 1999);
            if (mapString.Equals("Commons"))
            mapIt = new MapItButton(2523, 1669);
            if (mapString.Equals("Across Commons"))
            mapIt = new MapItButton(2436, 1536);
            if (mapString.Equals("Evans"))
            mapIt = new MapItButton(2245, 1466);
            if (mapString.Equals("Pavilion"))
            mapIt = new MapItButton(2319, 1506);

            if (mapString.Equals("Bush"))
            mapIt = new MapItButton(895, 3744);
            if (mapString.Equals("Rec"))
            mapIt = new MapItButton(1900, 2475);
            if (mapString.Equals("Vet"))
            mapIt = new MapItButton(960, 1920);
            if (mapString.Equals("AgCafe"))
            mapIt = new MapItButton(1290, 2164);
            if (mapString.Equals("Rudder"))
            mapIt = new MapItButton(2516, 1869);
            if (mapString.Equals("MSC"))
            mapIt = new MapItButton(2005, 1984);






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
