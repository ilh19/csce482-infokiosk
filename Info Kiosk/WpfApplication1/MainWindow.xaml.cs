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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Windows.Markup;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Windows.Interop;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static ecologylab.interactive.Utils.DisableTouchConversionToMouse disableTouchConversionToMouse = new ecologylab.interactive.Utils.DisableTouchConversionToMouse();
        private Dictionary<UIElement, int> movingGifImage = new Dictionary<UIElement, int>();

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // if we're not in design mode, initialize the graphics device
            if (DesignerProperties.GetIsInDesignMode(this) == false)
            {
                InitializeGraphicsDevice();
            }

            //graphicsService = GraphicsDeviceService.AddRef(new WindowInteropHelper(this).Handle);

            GlobalVariables.serviceContainer.AddService<IGraphicsDeviceService>(graphicsService);

            ContentManager contentManager = new ContentManager(GlobalVariables.serviceContainer, GlobalVariables.contentBuilder.OutputDirectory);

            contentManager.Unload();

            GlobalVariables.contentBuilder.Clear();
            GlobalVariables.contentBuilder.Add("C:\\infoKiosk\\KioskRepository\\Info Kiosk\\WpfApplication1\\Content\\virus2.png", "virus2", null, "TextureProcessor");
            GlobalVariables.contentBuilder.Add("C:\\infoKiosk\\KioskRepository\\Info Kiosk\\WpfApplication1\\Content\\fire.png", "fire", null, "TextureProcessor");
            GlobalVariables.contentBuilder.Add("C:\\infoKiosk\\KioskRepository\\Info Kiosk\\WpfApplication1\\Content\\blue fire.png", "blue fire", null, "TextureProcessor");
            GlobalVariables.contentBuilder.Add("C:\\infoKiosk\\KioskRepository\\Info Kiosk\\WpfApplication1\\Content\\ParticleSystem.fx", "ParticleSystem", null, "EffectProcessor");

            // Build this new model data.
            string buildError = GlobalVariables.contentBuilder.Build();

            particleEffect = contentManager.Load<Effect>("ParticleSystem");
            virus = contentManager.Load<Texture2D>("virus2");
            fire = contentManager.Load<Texture2D>("fire");
            blueFire = contentManager.Load<Texture2D>("blue fire");

            emitter1 = new ParticleEmitter(10000, particleEffect, blueFire, fire);
            emitter1.effectTechnique = "ChangePicAndFadeAtPercent";
            emitter1.fadeStartPercent = .8f;
            emitter1.changePicPercent = .6f;
        }

        public GifImage DeepCopy(GifImage element)
        {
            string shapestring = XamlWriter.Save(element);
            StringReader stringReader = new StringReader(shapestring);
            XmlTextReader xmlTextReader = new XmlTextReader(stringReader);
            GifImage DeepCopyobject = (GifImage)XamlReader.Load(xmlTextReader);
            return DeepCopyobject;
        }

        public MainWindow()
        {
           // this.InitializeComponent();
            this.InitializeComponent();
            weather.GifSource = "/Images/earth.gif";
            map.GifSource = "/Images/earth.gif";
            transit.GifSource = "/Images/earth.gif";
            dining.GifSource = "/Images/earth.gif";
            // Insert code required on object creation below this point.

            //InitializeComponent();
            GlobalVariables.stopwatch.Start();
            // hook up an event to fire when the control has finished loading
            Loaded += new RoutedEventHandler(MainWindow_Loaded);
        }

        ~MainWindow()
        {
            imageSource.Dispose();

            // release on finalizer to clean up the graphics device
            if (graphicsService != null)
                graphicsService.Release();
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
                System.Windows.Point center = new System.Windows.Point(element.ActualWidth / 2, element.ActualHeight / 2);
                // transform it to take into account transforms from previous manipulations 
                center = matrix.Transform(center);
                //this will be a Zoom. 
                //matrix.ScaleAt(deltaManipulation.Scale.X, deltaManipulation.Scale.Y, center.X, center.Y); 
                // Rotation 
                matrix.RotateAt(e.DeltaManipulation.Rotation, center.X, center.Y);
                //Translation (pan) 
                matrix.Translate(e.DeltaManipulation.Translation.X, e.DeltaManipulation.Translation.Y);

                //((MatrixTransform)element.RenderTransform).Matrix = matrix;
                element.RenderTransform = new MatrixTransform(matrix);

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
            //circle.ManipulationCompleted += new EventHandler<ManipulationCompletedEventArgs>(rect2_ManipulationCompleted);


            
        }

        void rect2_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Completed");
        }

        void rect2_ManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Mani boundary " + e.BoundaryFeedback.Translation.ToString());
        }

        private void gifImage_TouchDown(object sender, System.Windows.Input.TouchEventArgs e)
        {
            if (movingGifImage.ContainsKey((GifImage)sender) == false)
            {
                GlobalVariables.gifCopy = new GifImage();
                GlobalVariables.gifCopy = DeepCopy((GifImage)sender);

                GlobalVariables.gifCopy.ManipulationDelta += circle_ManipulationDelta;
                GlobalVariables.gifCopy.ManipulationStarting += circle_ManipulationStarting;
                GlobalVariables.gifCopy.TouchDown += gifImage_TouchDown;
                GlobalVariables.gifCopy.TouchUp += gifImage_TouchUp;
                GlobalVariables.gifCopy.Loaded += Window_Loaded;

                movingGifImage[(GifImage)sender] = e.TouchDevice.Id;
                canvas.Children.Add(GlobalVariables.gifCopy);
            }
        }

        private void gifImage_TouchUp(object sender, System.Windows.Input.TouchEventArgs e)
        {
            // TODO: Add event handler implementation here.

            if (movingGifImage.ContainsKey((GifImage)sender))
            {
                canvas.Children.Remove((GifImage)sender);
                movingGifImage.Remove((GifImage)sender);
            }

            WrapPanel window = new WrapPanel();
            window.Name = "widgetWindow";
            window.Height = 300;
            window.Width = 250;
            window.IsManipulationEnabled = true;
            window.Background = Brushes.LightSlateGray;
            window.RenderTransform = new MatrixTransform(1.5, 0.5, -0.5, 1.5, e.GetTouchPoint(canvas).Position.X, e.GetTouchPoint(canvas).Position.Y);
            window.AddHandler(WrapPanel.ManipulationDeltaEvent, new EventHandler<ManipulationDeltaEventArgs>(window_ManipulationDelta), true);//("circle_ManipulationDelta");
            window.AddHandler(WrapPanel.ManipulationStartingEvent, new EventHandler<ManipulationStartingEventArgs>(window_ManipulationStarting), true);
            window.AddHandler(WrapPanel.TouchDownEvent, new EventHandler<TouchEventArgs>(window_TouchDown), true);
            window.AddHandler(WrapPanel.ManipulationInertiaStartingEvent, new EventHandler<ManipulationInertiaStartingEventArgs>(window_ManipulationInertiaStarting), true);
            canvas.Children.Add(window);

            Grid grid = new Grid();
            grid.Name = "WidgetGrid";
            window.Children.Add(grid);

            System.Windows.Shapes.Rectangle leftTab = new System.Windows.Shapes.Rectangle();
            leftTab.Name = "LeftTab";
            ImageBrush leftIcon = new ImageBrush();    // image background for close tab
            leftIcon.ImageSource =
                new BitmapImage(
                    new Uri(@"Z:\CSCE 482\csce482-infokiosk\Info Kiosk\WpfApplication1\Images\leftHandle.png", UriKind.Relative)
                );
            leftTab.AddHandler(System.Windows.Shapes.Rectangle.TouchDownEvent, new EventHandler<TouchEventArgs>(LeftTabTouch), true);
            leftTab.RenderTransform = new MatrixTransform(1, 0, 0, 1, -140, 0);
            leftTab.Height = 80;
            leftTab.Width = 35;
            leftTab.Fill = leftIcon;
            grid.Children.Add(leftTab);

            System.Windows.Shapes.Rectangle rightTab = new System.Windows.Shapes.Rectangle();
            rightTab.Name = "RightTab";
            ImageBrush rightIcon = new ImageBrush();    // image background for close tab
            rightIcon.ImageSource =
                new BitmapImage(
                    new Uri(@"Z:\CSCE 482\csce482-infokiosk\Info Kiosk\WpfApplication1\Images\rightHandle.png", UriKind.Relative)
                );
            rightTab.AddHandler(System.Windows.Shapes.Rectangle.TouchDownEvent, new EventHandler<TouchEventArgs>(RightTabTouch), true);
            rightTab.RenderTransform = new MatrixTransform(1, 0, 0, 1, 140, 0);
            rightTab.Height = 80;
            rightTab.Width = 35;
            rightTab.Fill = rightIcon;
            grid.Children.Add(rightTab);

            System.Windows.Shapes.Rectangle closeTab = new System.Windows.Shapes.Rectangle();
            closeTab.Name = "CloseTab";
            ImageBrush closeIcon = new ImageBrush();    // image background for close tab
            closeIcon.ImageSource =
                new BitmapImage(
                    new Uri(@"Z:\CSCE 482\csce482-infokiosk\Info Kiosk\WpfApplication1\Images\closeIcon.png", UriKind.Relative)
                );
            closeTab.AddHandler(System.Windows.Shapes.Rectangle.TouchDownEvent, new EventHandler<TouchEventArgs>(CloseTabTouchDown), true);
            closeTab.RenderTransform = new MatrixTransform(1, 0, 0, 1, 110, -166);
            closeTab.Height = 30;
            closeTab.Width = 30;
            closeTab.Fill = closeIcon;
            grid.Children.Add(closeTab);

            System.Windows.Shapes.Rectangle restoreTab = new System.Windows.Shapes.Rectangle();
            restoreTab.Name = "RestoreTab";
            ImageBrush restoreIcon = new ImageBrush();    // image background for restore tab
            restoreIcon.ImageSource =
                new BitmapImage(
                    new Uri(@"Z:\CSCE 482\csce482-infokiosk\Info Kiosk\WpfApplication1\Images\resizeIcon.png", UriKind.Relative)
                );
            restoreTab.AddHandler(System.Windows.Shapes.Rectangle.TouchDownEvent, new EventHandler<TouchEventArgs>(RestoreTabTouchDown), true);
            restoreTab.RenderTransform = new MatrixTransform(1, 0, 0, 1, 81, -166);
            restoreTab.Height = 30;
            restoreTab.Width = 30;
            restoreTab.Fill = restoreIcon;
            grid.Children.Add(restoreTab);

            WebControl webControl = new WebControl();
            webControl.Name = "webControl";

            if ((sender as GifImage).Name == "map")
            {
                webControl.Source = new Uri("C:\\infoKiosk\\KioskRepository\\Info Kiosk\\WpfApplication1\\webpages\\maps.htm");
            }
            else if ((sender as GifImage).Name == "transit")
            {
                webControl.Source = new Uri("C:\\infoKiosk\\KioskRepository\\Info Kiosk\\WpfApplication1\\transit\\01.html");
            }
            else if ((sender as GifImage).Name == "weather")
            {
                webControl.Source = new Uri("http://theshinyspoonpay.appspot.com");
            }
            else if ((sender as GifImage).Name == "dining")
            {
                webControl.Source = new Uri("http://m.tamu.edu/dining");
            }
            else
            {
                //webControl.Source = new Uri("http://www.bing.com/search?q=" + sender);
                webControl.Source = new Uri("http://m.tamu.edu/");
            }


            //webControl.Source = new Uri("C:\\infoKiosk\\KioskRepository\\Info Kiosk\\WpfApplication1\\transit\\01.htm");




            webControl.Margin = new Thickness(0, 0, 0, 0);
            webControl.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            webControl.Width = 250;
            webControl.RenderTransform = new MatrixTransform(1, 0, 0, 1, 0, 0);
            grid.Children.Add(webControl);
        }

        private void LeftTabTouch(object sender, System.Windows.Input.TouchEventArgs e)
        {

        }

        private void RightTabTouch(object sender, System.Windows.Input.TouchEventArgs e)
        {

        }

        private void CloseTabTouchDown(object sender, System.Windows.Input.TouchEventArgs e)
        {
            ((((sender as System.Windows.Shapes.Rectangle).Parent as Grid).Parent as WrapPanel).Parent as Canvas).Children.Remove(
                    (((sender as System.Windows.Shapes.Rectangle).Parent as Grid).Parent as WrapPanel));
        }


        /// <summary>
        /// Should restore the original size of the window to the default size...having issues. -VG
        /// </summary>
        private void RestoreTabTouchDown(object sender, EventArgs e)
        {
            (((sender as System.Windows.Shapes.Rectangle).Parent as Grid).Parent as WrapPanel).Height = 300;
            (((sender as System.Windows.Shapes.Rectangle).Parent as Grid).Parent as WrapPanel).Width = 250;
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
                System.Windows.Point center = new System.Windows.Point(element.ActualWidth / 2, element.ActualHeight / 2);
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
                DesiredDeceleration = 7.0 * 96.0 / (1000.0 * 1000.0)
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

        #region Particle System

        private static GraphicsDeviceService graphicsService;
        private XnaImageSource imageSource;

        internal ParticleEmitter emitter1;
        Texture2D virus;
        Texture2D fire;
        Texture2D blueFire;
        Effect particleEffect;
        internal Random rand = new Random();
        private bool isDrawTurn = false;

        /// <summary>
        /// Gets the GraphicsDevice behind the control.
        /// </summary>
        public static GraphicsDevice GraphicsDevice
        {
            get { return graphicsService.GraphicsDevice; }
        }

        public void DrawFunction(GraphicsDevice g)
        {
            GlobalVariables.TotalTime = GlobalVariables.stopwatch.Elapsed;
            g.Clear(Microsoft.Xna.Framework.Color.Transparent);

            if (emitter1 != null)
            {
                emitter1.update();
                createParticlesInCircle(emitter1, 10, 125, new Vector2(0, 0));
                emitter1.draw();
            }
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            // if we're not in design mode, recreate the 
            // image source for the new size
            if (DesignerProperties.GetIsInDesignMode(this) == false &&
                graphicsService != null)
            {
                // recreate the image source
                imageSource.Dispose();
                imageSource = new XnaImageSource(
                    GraphicsDevice, (int)ActualWidth, (int)ActualHeight);
                rootImage.Source = imageSource.WriteableBitmap;
            }

            base.OnRenderSizeChanged(sizeInfo);

        }

        private void InitializeGraphicsDevice()
        {
            if (graphicsService == null)
            {
                // add a reference to the graphics device
                graphicsService = GraphicsDeviceService.AddRef(
                    (PresentationSource.FromVisual(this) as HwndSource).Handle);

                // create the image source
                imageSource = new XnaImageSource(
                    GraphicsDevice, (int)ActualWidth, (int)ActualHeight);
                rootImage.Source = imageSource.WriteableBitmap;

                // hook the rendering event
                CompositionTarget.Rendering += CompositionTarget_Rendering;
            }
        }

        /// <summary>
        /// Draws the control and allows subclasses to override 
        /// the default behavior of delegating the rendering.
        /// </summary>
        protected virtual void Render()
        {
            // invoke the draw delegate so someone will draw something pretty
            //if (DrawFunction != null)
            DrawFunction(GraphicsDevice);
        }

        void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            if (isDrawTurn)
            {
                // set the image source render target
                GraphicsDevice.SetRenderTarget(imageSource.RenderTarget);

                // allow the control to draw
                Render();

                // unset the render target
                GraphicsDevice.SetRenderTarget(null);

                // commit the changes to the image source
                imageSource.Commit();
            }
            isDrawTurn = !isDrawTurn;
        }

        private void createParticlesInCircle(ParticleEmitter emitter, int maxNumToCreate, float radius, Vector2 circleOrigin)
        {
            double positionAngle = ((GlobalVariables.TotalTime.TotalMilliseconds % 8000.0) / 8000.0) * Math.PI * 2;
            Vector2 position = new Vector2((float)Math.Cos(positionAngle) * radius, (float)Math.Sin(positionAngle) * radius) + circleOrigin;

            for (int i = 0; i < maxNumToCreate; i++)
            {
                double velocityAngle = rand.NextDouble() * Math.PI * 2;
                float velocitySpeed = rand.Next(2, 15);
                double accelAngle = rand.NextDouble() * Math.PI * 2;
                float accelSpeed = rand.Next(2, 15);
                emitter.createParticles(new Vector2((float)Math.Cos(velocityAngle) * velocitySpeed, (float)Math.Sin(velocityAngle) * velocitySpeed),
                                new Vector2((float)Math.Cos(accelAngle) * accelSpeed, (float)Math.Sin(accelAngle) * accelSpeed),
                                position,
                                rand.Next(5, 20),
                                rand.Next(1000, 4000));
            }
        }

        #endregion Particle System

        private void rootImage_TouchDown(object sender, TouchEventArgs e)
        {
            if(emitter1 != null)
                emitter1.createParticles(Vector2.One,
                                     Vector2.One,
                                     Vector2.Zero, 10, 3000);
        }
    }
}
