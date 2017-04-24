using System;
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
    public sealed partial class Ghost : UserControl
    { // Speed 
        private double speed = 5;

        //location
        public double LocationX { get; set; }
        public double LocationY { get; set; }


        public Ghost()
        {
            this.InitializeComponent();
        }

        // Show player
        public void SetLocation()
        {
            SetValue(Canvas.LeftProperty, LocationX);
            SetValue(Canvas.TopProperty, LocationY);
        }

        // Move functions
        public void MoveUp()
        {
            // Update location values (with speed)
            LocationY -= speed;
            
        }

        public void MoveDown()
        {
            // Update location values (with speed)
            LocationY += speed;
          
        }

        public void MoveLeft()
        {
            // Update location values (with speed)
            LocationX -= speed;
            
        }

        public void MoveRight()
        {
            // Update location values (with speed)
            LocationX += speed;
           
        }

    }
}

