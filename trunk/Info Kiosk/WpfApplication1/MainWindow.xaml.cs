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
using System.Windows.Media.Animation;
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

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

            


        }

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

            WrapPanel window = new WrapPanel();
            window.Height = 300;
            window.Width = 320;
            window.IsManipulationEnabled = true;
            window.Background = Brushes.LightSlateGray;
            Grid grid = new Grid();
            WebControl webControl = new WebControl();
            webControl.Name = "webControl";
            webControl.Source = new Uri("http://m.tamu.edu");
            webControl.Margin = new Thickness(0, 0, 0, 0);
            webControl.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            webControl.Width = 250;
            webControl.RenderTransform = new MatrixTransform(1, 0, 0, 1, 35, 0);
            grid.Children.Add(webControl);
            window.Children.Add(grid);
            window.RenderTransform = new MatrixTransform(1.5, 0.5, -0.5, 1.5, e.GetTouchPoint(canvas).Position.X, e.GetTouchPoint(canvas).Position.Y);
            window.AddHandler(WrapPanel.ManipulationDeltaEvent, new EventHandler<ManipulationDeltaEventArgs>(window_ManipulationDelta), true);//("circle_ManipulationDelta");
            window.AddHandler(WrapPanel.ManipulationStartingEvent, new EventHandler<ManipulationStartingEventArgs>(window_ManipulationStarting), true);
            window.AddHandler(WrapPanel.TouchDownEvent, new EventHandler<TouchEventArgs>(window_TouchDown), true);
            window.AddHandler(WrapPanel.ManipulationInertiaStartingEvent, new EventHandler<ManipulationInertiaStartingEventArgs>(window_ManipulationInertiaStarting), true);


            canvas.Children.Add(window);
        }

        private void window_TouchDown(object sender, System.Windows.Input.TouchEventArgs e)
        {

        }

        private void window_ManipulationDelta(object sender, System.Windows.Input.ManipulationDeltaEventArgs e)
        {
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
                matrix.ScaleAt(deltaManipulation.Scale.X, deltaManipulation.Scale.Y, center.X, center.Y);
                // Rotation 
                matrix.RotateAt(e.DeltaManipulation.Rotation, center.X, center.Y);
                //Translation (pan) 
                matrix.Translate(e.DeltaManipulation.Translation.X, e.DeltaManipulation.Translation.Y);

                ((MatrixTransform)element.RenderTransform).Matrix = matrix;

                e.Handled = true;

                // We are only checking boundaries during inertia 
                // in real world, we would check all the time 
                if (e.IsInertial)
                {
                    Rect containingRect = new Rect(((FrameworkElement)e.ManipulationContainer).RenderSize);

                    Rect shapeBounds = element.RenderTransform.TransformBounds(new Rect(element.RenderSize));

                    // Check if the element is completely in the window.
                    // If it is not and intertia is occuring, stop the manipulation.
                    if (e.IsInertial && !containingRect.Contains(shapeBounds))
                    {
                        //Report that we have gone over our boundary 
                        e.ReportBoundaryFeedback(e.DeltaManipulation);
                        // comment out this line to see the Window 'shake' or 'bounce' 
                        // similar to Win32 Windows when they reach a boundary; this comes for free in .NET 4                
                        e.Complete();
                    }
                }
            }
        }

        void window_ManipulationInertiaStarting(object sender, ManipulationInertiaStartingEventArgs e)
        {
            // Decrease the velocity of the Rectangle's movement by 
            // 10 inches per second every second.
            // (10 inches * 96 DIPS per inch / 1000ms^2)
            e.TranslationBehavior = new InertiaTranslationBehavior()
            {
                InitialVelocity = e.InitialVelocities.LinearVelocity,
                DesiredDeceleration = 10.0 * 96.0 / (1000.0 * 1000.0)
            };

            // Decrease the velocity of the Rectangle's resizing by 
            // 0.1 inches per second every second.
            // (0.1 inches * 96 DIPS per inch / (1000ms^2)
            e.ExpansionBehavior = new InertiaExpansionBehavior()
            {
                InitialVelocity = e.InitialVelocities.ExpansionVelocity,
                DesiredDeceleration = 0.1 * 96 / 1000.0 * 1000.0
            };

            // Decrease the velocity of the Rectangle's rotation rate by 
            // 2 rotations per second every second.
            // (2 * 360 degrees / (1000ms^2)
            e.RotationBehavior = new InertiaRotationBehavior()
            {
                InitialVelocity = e.InitialVelocities.AngularVelocity,
                DesiredDeceleration = 720 / (1000.0 * 1000.0)
            };
            e.Handled = true;
        }


        UIElement last; 

        void window_ManipulationStarting(object sender, ManipulationStartingEventArgs e)
        {
            var uie = e.OriginalSource as UIElement;
            if (uie != null)
            {
                if (last != null) Canvas.SetZIndex(last, 0);
                Canvas.SetZIndex(uie, 2);
                last = uie;
            }

            //canvas is the parent of the image starting the manipulation;
            //Container does not have to be parent, but that is the most common scenario
            e.ManipulationContainer = canvas;
            e.Handled = true;
            // you could set the mode here too 
            // e.Mode = ManipulationModes.All;             
        }
    }
}
