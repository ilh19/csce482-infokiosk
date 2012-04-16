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
using Microsoft.Maps.MapControl.WPF;

namespace WpfApplication1
{

    class MapWidget : Widget
    {
        protected Map map = new Map();


        public MapWidget(Canvas c, Grid g, System.Windows.Input.TouchEventArgs e)
          : base(c,g,e)
        {
            map.VerticalAlignment = VerticalAlignment.Bottom;
            map.Margin = new Thickness(0, 30, 0, 0);
            map.Height = 500;
            map.Width = 250;
            grid.Children.Add(map);
            grid.Children.Add(instructions);
        }
    }
}
