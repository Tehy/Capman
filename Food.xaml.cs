﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Capman
{
    public sealed partial class Food : UserControl
    {


        public double LocationX { get; set; }
        public double LocationY { get; set; }

        public Food()
        {
            this.InitializeComponent();

            Width = 20;
            Height = 20;
        }
        public void SetLocation()
        {
            SetValue(Canvas.LeftProperty, LocationX + 10);
            SetValue(Canvas.TopProperty, LocationY + 10);
        }

    }
}
