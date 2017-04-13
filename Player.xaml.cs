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
    public sealed partial class Player : UserControl
    {
        // Speed and acceleration
       
        
        private double speed = 5;

        //location
        public double LocationX { get; set; }
        public double LocationY { get; set; }



        public Player()
        {
            this.InitializeComponent();
        }

             // Show player
        public void SetLocation()
        {
            SetValue(Canvas.LeftProperty, LocationX);
            SetValue(Canvas.TopProperty, LocationY);
        }

        // Move
        public void MoveUp()
        {
            // Update location values (with speed)
            LocationY -= speed;
            if (LocationY >= 550) LocationY = -50;
            else if (LocationY <= -50 ) LocationY = 550;
        }

        public void MoveDown()
        {
            // Update location values (with speed)
            LocationY += speed;
            if (LocationY >= 550) LocationY = -50;
            else if (LocationY <= -50) LocationY = 550;
        }

        public void MoveLeft()
        {
            // Update location values (with speed)
            LocationX += speed;
            if (LocationX >= 750) LocationX = -75;
            else if (LocationX <= -75) LocationX = 750;
        }

        public void MoveRight()
        {
            // Update location values (with speed)
            LocationX -= speed;
            if (LocationX >= 750) LocationX = -75;
            else if (LocationX <= -75) LocationX = 750;
        }

    }
}

