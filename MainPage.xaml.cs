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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Capman
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //player
        private Player player;

        // private buttons pressed

        // game loop timer
        private DispatcherTimer timer;

        
        

        public MainPage()
        {
            this.InitializeComponent();
            // add player
            player = new Player
            {
                LocationX = MyCanvas.Width / 2,
                LocationY = MyCanvas.Height / 2

            };
            MyCanvas.Children.Add(player);

            

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1000 / 60);
            timer.Tick += Timer_Tick;
            timer.Start(); //startgame


        }

        private void Timer_Tick(object sender, object e)
        {
               
        }
    }
}
