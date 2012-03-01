using System;
using System.Collections.Generic;
using Awesomium.Core;
using Awesomium.Windows.Controls;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Markup;
using System.IO;
using System.Xml;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Dictionary<UIElement, int> movingEllipses = new Dictionary<UIElement, int>();

        public Ellipse DeepCopy(Ellipse element)
        {
            string shapestring = XamlWriter.Save(element);
            StringReader stringReader = new StringReader(shapestring);
            XmlTextReader xmlTextReader = new XmlTextReader(stringReader);
            Ellipse DeepCopyobject = (Ellipse)XamlReader.Load(xmlTextReader);
            return DeepCopyobject;
        }

        public MainWindow()
        {
            this.InitializeComponent();

            // Insert code required on object creation below this point.
        }

        private void circle_ManipulationStarting(object sender, System.Windows.Input.ManipulationStartingEventArgs e)
        {
            e.ManipulationContainer = canvas;
        }

        private void circle_ManipulationDelta(object sender, System.Windows.Input.ManipulationDeltaEventArgs e)
        {
            // TODO: Add event handler implementation here.
            //this just gets the source. 
            // I cast it to FE because I wanted to use ActualWidth for Center. You could try RenderSize as alternate
            var element = e.Source as FrameworkElement;
            if (element != null)
            {
                //e.DeltaManipulation has the changes 
                // Scale is a delta multiplier; 1.0 is last size,  (so 1.1 == scale 10%, 0.8 = shrink 20%) 
                // Rotate = Rotation, in degrees
                // Pan = Translation, == Translate offset, in Device Independent Pixels 


                var deltaManipulation = e.DeltaManipulation;
                var matrix = ((MatrixTransform)element.RenderTransform).Matrix;
                // find the old center; arguaby this could be cached 
                Point center = new Point(element.ActualWidth / 2, element.ActualHeight / 2);
                // transform it to take into account transforms from previous manipulations 
                center = matrix.Transform(center);
                //this will be a Zoom. 
                //matrix.ScaleAt(deltaManipulation.Scale.X, deltaManipulation.Scale.Y, center.X, center.Y); 
                // Rotation 
                matrix.RotateAt(e.DeltaManipulation.Rotation, center.X, center.Y);
                //Translation (pan) 
                matrix.Translate(e.DeltaManipulation.Translation.X, e.DeltaManipulation.Translation.Y);

                ((MatrixTransform)element.RenderTransform).Matrix = matrix;

                e.Handled = true;

                System.Diagnostics.Debug.WriteLine("Matrix: " + matrix.ToString());
            }
        }

        private void Window_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            // TODO: Add event handler implementation here.
            canvas.ManipulationStarting += new EventHandler<ManipulationStartingEventArgs>(circle_ManipulationStarting);
            canvas.ManipulationDelta += new EventHandler<ManipulationDeltaEventArgs>(circle_ManipulationDelta);
            this.ManipulationBoundaryFeedback += new EventHandler<ManipulationBoundaryFeedbackEventArgs>(rect2_ManipulationBoundaryFeedback);

            //Optional 
            circle.ManipulationCompleted += new EventHandler<ManipulationCompletedEventArgs>(rect2_ManipulationCompleted);
        }

        void rect2_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Completed");
        }

        void rect2_ManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Mani boundary " + e.BoundaryFeedback.Translation.ToString());
        }

        private void circle_TouchDown(object sender, System.Windows.Input.TouchEventArgs e)
        {
            if (movingEllipses.ContainsKey((Ellipse)sender) == false)
            {

                GlobalVariables.circleCopy = new Ellipse();
                GlobalVariables.circleCopy = DeepCopy((Ellipse)sender);

                GlobalVariables.trueCircle = GlobalVariables.circleCopy;

                GlobalVariables.circleCopy.ManipulationDelta += circle_ManipulationDelta;
                GlobalVariables.circleCopy.ManipulationStarting += circle_ManipulationStarting;
                GlobalVariables.circleCopy.TouchDown += circle_TouchDown;
                GlobalVariables.circleCopy.TouchUp += circle_TouchUp;
                GlobalVariables.circleCopy.Loaded += Window_Loaded;

                movingEllipses[(Ellipse)sender] = e.TouchDevice.Id;
                canvas.Children.Add(GlobalVariables.circleCopy);
            }
        }

        private void circle_TouchUp(object sender, System.Windows.Input.TouchEventArgs e)
        {
            // TODO: Add event handler implementation here.

            if (movingEllipses.ContainsKey((Ellipse)sender))
            {
                canvas.Children.Remove((Ellipse)sender);
                movingEllipses.Remove((Ellipse)sender);
            }
        }
    }
}