﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Shapes;
using System.Diagnostics;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 

    public static class GlobalVariables
    {
        public static GifImage gifCopy;
        public static System.Diagnostics.Stopwatch stopwatch = new Stopwatch();
        public static TimeSpan TotalTime;
        public static ContentBuilder contentBuilder = new ContentBuilder();
        public static ServiceContainer serviceContainer = new ServiceContainer();
    }

    static class Constants
    {
        public const int closeInterval = 30000; //in milliseconds
        public const int maxWin = 5;
    }

    public partial class App : Application
    {
    }
}