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
    class MapItButton : Button
    {
        //Button backButton = new Button();
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

        //public Button BackButton
        //{
        //    get { return backButton; }
        //    set { backButton = value; }
        //}


        public MapItButton(int x, int y)//String imgName)
            : base()
        {
            var uriSource = new Uri("pack://application:,,,/Images/" + "currentMapDining.png");
            img.Source = new BitmapImage(uriSource);
            xCordinate = x;
            yCordinate = y;

            //img.ClipToBounds = true;
            //img.Width = 250;

            //img.ManipulationStarting += map_ManipulationStarting;
            //img.ManipulationDelta += map_ManipulationDelta;
            //img.IsManipulationEnabled = true;

            //FontSize = 6;
            Content = "MAP IT";
            //backButton.HorizontalAlignment = HorizontalAlignment.Left;
            //backButton.VerticalAlignment = VerticalAlignment.Top;
            //backButton.Margin = new Thickness(-35, 0, 0, 0);
            //backButton.Content = "BACK";
            //backButton.Height = 20;
        }




    }


}
