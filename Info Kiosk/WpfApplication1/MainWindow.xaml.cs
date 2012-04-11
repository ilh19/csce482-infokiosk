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
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Windows.Interop;
using System.Windows.Threading;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static ecologylab.interactive.Utils.DisableTouchConversionToMouse disableTouchConversionToMouse = new ecologylab.interactive.Utils.DisableTouchConversionToMouse();
        private Dictionary<UIElement, int> movingGifImage = new Dictionary<UIElement, int>();
        //private Dictionary<object, DispatcherTimer> timerList = new Dictionary<object, DispatcherTimer>();
        //private int count;

        Dictionary<int, Vector2> lastPosition = new Dictionary<int, Vector2>();
        CircularArray<Vector2> lastPositionArray = new CircularArray<Vector2>(2000);


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
            GlobalVariables.contentBuilder.Add("C:\\infoKiosk\\KioskRepository\\Info Kiosk\\WpfApplication1\\Content\\fire2.png", "fire2", null, "TextureProcessor");
            GlobalVariables.contentBuilder.Add("C:\\infoKiosk\\KioskRepository\\Info Kiosk\\WpfApplication1\\Content\\blue fire.png", "blue fire", null, "TextureProcessor");
            GlobalVariables.contentBuilder.Add("C:\\infoKiosk\\KioskRepository\\Info Kiosk\\WpfApplication1\\Content\\ParticleSystem.fx", "ParticleSystem", null, "EffectProcessor");

            // Build this new model data.
            string buildError = GlobalVariables.contentBuilder.Build();

            particleEffect = contentManager.Load<Effect>("ParticleSystem");
            virus = contentManager.Load<Texture2D>("virus2");
            fire2 = contentManager.Load<Texture2D>("fire2");
            fire = contentManager.Load<Texture2D>("fire");
            blueFire = contentManager.Load<Texture2D>("blue fire");

            emitter1 = new ParticleEmitter(100000, particleEffect, fire2);
            emitter1.effectTechnique = "FadeAtXPercent";
            emitter1.fadeStartPercent = .1f;
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
            this.InitializeComponent();
            weather.GifSource = "/Images/weather.gif";
            map.GifSource = "/Images/map.gif";
            transit.GifSource = "/Images/transit.gif";
            dining.GifSource = "/Images/dining.gif";

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
            GlobalVariables.lastTouchTime = GlobalVariables.TotalTime;

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
            //lastTouchTime = GlobalVariables.TotalTime;

            // TODO: Add event handler implementation here.

            if (movingGifImage.ContainsKey((GifImage)sender))
            {
                canvas.Children.Remove((GifImage)sender);
                movingGifImage.Remove((GifImage)sender);
            }

            if (GlobalVariables.count <= Constants.maxWin)
            {
                if ((sender as GifImage).Name == "map")
                {
                    Widget wdgt = new TransitWidget(canvas,LayoutRoot, e);
                }
                else if ((sender as GifImage).Name == "transit")
                {
                    Widget wdgt = new TransitWidget(canvas, LayoutRoot, e);
                }
                else if ((sender as GifImage).Name == "weather")
                {
                    Widget wdgt = new WeatherWidget(canvas, LayoutRoot, e);
                }
                else if ((sender as GifImage).Name == "dining")
                {
                    Widget wdgt = new DiningWidget(canvas,LayoutRoot, e);
                }
                else
                {
                    Widget wdgt = new TransitWidget(canvas, LayoutRoot, e);
                }

            }
            else
            {
                //limit reached
            }
        }

        #region Particle System

        private static GraphicsDeviceService graphicsService;
        private XnaImageSource imageSource;

        internal ParticleEmitter emitter1;
        Texture2D virus;
        Texture2D fire;
        Texture2D fire2;
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

            GlobalVariables.PrevTime = GlobalVariables.TotalTime;
            GlobalVariables.TotalTime = GlobalVariables.stopwatch.Elapsed;
            g.Clear(Microsoft.Xna.Framework.Color.Transparent);

            if (emitter1 != null)
            {
                AttractMode();
                emitter1.update();
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

        #endregion Particle System

        private void rootImage_TouchMove(object sender, TouchEventArgs e)
        {
            GlobalVariables.lastTouchTime = GlobalVariables.TotalTime;

            if (emitter1 != null)
            {
                Vector2 currPosition = new Vector2((float)(-e.GetTouchPoint(this).Position.X + ActualWidth / 2), (float)(-e.GetTouchPoint(this).Position.Y + ActualHeight / 2));
                
                if (!lastPosition.ContainsKey(e.TouchDevice.Id))
                    lastPosition.Add(e.TouchDevice.Id, currPosition);

                Vector2 lastPos = lastPosition[e.TouchDevice.Id];
                float distance = Vector2.Distance(lastPos, currPosition);

                //if (lastPositionArray.contains(currPosition))
                //    return;
                //else
                //    lastPositionArray.add(currPosition);

                for (float i = 0; i <= distance; i += 1f)
                {
                    float ratio;
                    if (distance <= 0.001)
                        ratio = 0;
                    else
                        ratio = i / distance;

                    double velocityAngle = rand.NextDouble() * Math.PI * 2;
                    float velocitySpeed = rand.Next(5, 10);
                    double accelAngle = rand.NextDouble() * Math.PI * 2;
                    float accelSpeed = rand.Next(5, 10);
                    emitter1.createParticles(new Vector2((float)Math.Cos(velocityAngle) * velocitySpeed, (float)Math.Sin(velocityAngle) * velocitySpeed),
                                             new Vector2((float)Math.Cos(accelAngle) * accelSpeed, (float)Math.Sin(accelAngle) * accelSpeed),
                                             new Vector2(lastPos.X * ratio + currPosition.X * (1 - ratio), lastPos.Y * ratio + currPosition.Y * (1 - ratio)),
                                             45,
                                             2000);
                }

                lastPosition[e.TouchDevice.Id] = new Vector2((float)(-e.GetTouchPoint(this).Position.X + ActualWidth / 2), (float)(-e.GetTouchPoint(this).Position.Y + ActualHeight / 2));
            }
        }

        private void rootImage_TouchUp(object sender, TouchEventArgs e)
        {
            GlobalVariables.lastTouchTime = GlobalVariables.TotalTime;

            if(lastPosition.ContainsKey(e.TouchDevice.Id))
                lastPosition.Remove(e.TouchDevice.Id);
        }

        void AttractMode()
        {
            TimeSpan AttractModeStartTime = new TimeSpan(0, 0, 90);
            #region Activate/Deactivate Attract Mode
            if (GlobalVariables.TotalTime.Subtract(GlobalVariables.lastTouchTime) < new TimeSpan(0, 0, 5))
            {
                if (canvas.Children.IndexOf(rootImage) != 0)
                {
                    canvas.Children.Remove(rootImage);
                    canvas.Children.Insert(0, rootImage);
                    grayImage.Visibility = System.Windows.Visibility.Hidden;
                }

                return;
            }

            if (canvas.Children.IndexOf(rootImage) != canvas.Children.Count)
            {
                grayImage.Visibility = System.Windows.Visibility.Visible;
                canvas.Children.Remove(rootImage);
                canvas.Children.Insert(canvas.Children.Count, rootImage);
            }
            #endregion Activate/Deactivate Attract Mode

            #region 0 - 1000
            if (GlobalVariables.TotalTime.Subtract(GlobalVariables.lastTouchTime) < (AttractModeStartTime + new TimeSpan(0, 0, 1)))
            {
                grayImage.Opacity = ((GlobalVariables.TotalTime.Subtract(GlobalVariables.lastTouchTime) - AttractModeStartTime).TotalMilliseconds / 1000) * .7;
                return;
            }
            #endregion 0 - 1000

            #region 1000 - 5000;
            if (GlobalVariables.TotalTime.Subtract(GlobalVariables.lastTouchTime) < (AttractModeStartTime + new TimeSpan(0, 0, 5)))
            {
                for (double i = GlobalVariables.PrevTime.TotalMilliseconds; i < GlobalVariables.TotalTime.TotalMilliseconds; i += .25)
                {
                    double singleLoopTime = 50.0; //in ms
                    double fullSpiralTime = 5000.0; //in ms
                    float fullSpiralRadius = 900f; //size of spiral

                    float radius = fullSpiralRadius - (float)((i % fullSpiralTime) / fullSpiralTime) * fullSpiralRadius;
                    double positionAngle = ((i % singleLoopTime) / singleLoopTime) * Math.PI * 2;
                    Vector2 position = new Vector2((float)Math.Cos(positionAngle) * radius, (float)Math.Sin(positionAngle) * radius);

                    Vector2 direction = position - Vector2.Zero;
                    direction.Normalize();
                    float velocitySpeed = rand.Next(10, 15);
                    float accelSpeed = rand.Next(10, 15);
                    emitter1.createParticles(direction * velocitySpeed,
                                    direction * accelSpeed,
                                    position,
                                    rand.Next(15, 20),
                                    rand.Next(2000, 5000));
                }

                for (double i = GlobalVariables.PrevTime.TotalMilliseconds; i < GlobalVariables.TotalTime.TotalMilliseconds; i += .25)
                {
                    double singleLoopTime = 50.0; //in ms
                    double fullSpiralTime = 2500.0; //in ms
                    float fullSpiralRadius = 900f; //size of spiral

                    float radius = fullSpiralRadius - (float)((i % fullSpiralTime) / fullSpiralTime) * fullSpiralRadius;
                    double positionAngle = ((i % singleLoopTime) / singleLoopTime) * Math.PI * 2;
                    Vector2 position = new Vector2((float)Math.Cos(positionAngle) * radius, (float)Math.Sin(positionAngle) * radius);

                    Vector2 direction = position - Vector2.Zero;
                    direction.Normalize();
                    float velocitySpeed = rand.Next(10, 15);
                    float accelSpeed = rand.Next(10, 15);
                    emitter1.createParticles(direction * velocitySpeed,
                                    direction * accelSpeed,
                                    position,
                                    rand.Next(15, 20),
                                    rand.Next(2000, 5000));
                }

                for (double i = GlobalVariables.PrevTime.TotalMilliseconds; i < GlobalVariables.TotalTime.TotalMilliseconds; i += .25)
                {
                    double singleLoopTime = 50.0; //in ms
                    double fullSpiralTime = 1000.0; //in ms
                    float fullSpiralRadius = 900f; //size of spiral

                    float radius = fullSpiralRadius - (float)((i % fullSpiralTime) / fullSpiralTime) * fullSpiralRadius;
                    double positionAngle = ((i % singleLoopTime) / singleLoopTime) * Math.PI * 2;
                    Vector2 position = new Vector2((float)Math.Cos(positionAngle) * radius, (float)Math.Sin(positionAngle) * radius);

                    Vector2 direction = position - Vector2.Zero;
                    direction.Normalize();
                    float velocitySpeed = rand.Next(10, 15);
                    float accelSpeed = rand.Next(10, 15);
                    emitter1.createParticles(direction * velocitySpeed,
                                    direction * accelSpeed,
                                    position,
                                    rand.Next(15, 20),
                                    rand.Next(2000, 5000));
                }
                return;
            }
            #endregion 0 - 5000;

            #region 5000 - 6000;
            if (GlobalVariables.TotalTime.Subtract(GlobalVariables.lastTouchTime) < (AttractModeStartTime + new TimeSpan(0, 0, 6)))
            {
                for (int i = 0; i < 500; i++)
                {
                    double positionAngle = rand.NextDouble() * Math.PI * 2;
                    Vector2 position = new Vector2((float)Math.Cos(positionAngle), (float)Math.Sin(positionAngle));

                    Vector2 direction = position - Vector2.Zero;
                    direction.Normalize();
                    float velocitySpeed = rand.Next(10, 15);
                    float accelSpeed = rand.Next(500, 750);
                    emitter1.createParticles(direction * velocitySpeed,
                                    direction * accelSpeed,
                                    Vector2.Zero,
                                    rand.Next(75, 125),
                                    rand.Next(2000, 5000));
                }
                return;
            }
            #endregion 5000 - 6000;

            #region 6000 - 9000
            if (GlobalVariables.TotalTime.Subtract(GlobalVariables.lastTouchTime) < (AttractModeStartTime + new TimeSpan(0, 0, 9)))
            {
                video.LoadedBehavior = System.Windows.Controls.MediaState.Manual;
                video.Visibility = Visibility.Visible;
                video.Volume = 1;
                video.Play();
            }
            #endregion 6000 - 9000

        }

        private void video_MediaEnded(object sender, RoutedEventArgs e)
        {
            video.Visibility = Visibility.Hidden;
            video.Position = TimeSpan.Zero;
        }
    }
}
