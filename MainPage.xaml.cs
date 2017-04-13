using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
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

        // Food

        private List<Food> foods;

        // which keys are pressed

        private bool UpPressed;
        private bool LeftPressed;
        private bool RightPressed;
        private bool DownPressed;

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

            // initialize list of food
            foods = new List<Food>();

            // key Listener

            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
     

            // Timer

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1000 / 60);
            timer.Tick += Timer_Tick;
            timer.Start(); //startgame


            // Mouse Listener
            Window.Current.CoreWindow.PointerPressed += CoreWindow_PointerPressed;

        }
        

        private void CoreWindow_PointerPressed(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.PointerEventArgs args)
        {
            // create a new flower
            Food food = new Food();
            // set location with mouse position
            food.LocationX = args.CurrentPoint.Position.X - food.Width / 2;
            food.LocationY = args.CurrentPoint.Position.Y - food.Height / 2;
            // add to game canvas
            MyCanvas.Children.Add(food);
            // set flower location in canvas
            food.SetLocation();
            // add to flowers list (for collision checking)
            foods.Add(food);
        }

        private void Timer_Tick(object sender, object e)
        {
            if (UpPressed) player.MoveUp();
            if (DownPressed) player.MoveDown();
            if (RightPressed) player.MoveLeft();
            if (LeftPressed) player.MoveRight();

            player.SetLocation();

            // check collisions alle
            CheckCollision();

        }

        private void CheckCollision()
        {
            foreach(Food food in foods)
            {
                // get rects
                Rect Brect = new Rect(
                    player.LocationX,
                    player.LocationY,
                    player.ActualWidth,
                    player.ActualHeight
                    );

                Rect FRect = new Windows.Foundation.Rect(
                    food.LocationX,
                    food.LocationY,
                    food.ActualWidth,
                    food.ActualHeight
                    );
                // does these intersects?
                Brect.Intersect(FRect);
                // is Brect empty?
                if (!Brect.IsEmpty)
                {
                    // remove flower
                    MyCanvas.Children.Remove(food);
                    foods.Remove(food);
                    
                  
                    break;
                }

            }
        }

        private void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            switch (args.VirtualKey)
            {
                case VirtualKey.Up:
                    UpPressed = true;
                    LeftPressed = false;
                    RightPressed = false;
                    DownPressed = false;
                    break;
                case VirtualKey.Left:
                    UpPressed = false;
                    LeftPressed = true;
                    RightPressed = false;
                    DownPressed = false;
                    break;
                case VirtualKey.Right:
                    UpPressed = false;
                    LeftPressed = false;
                    RightPressed = true;
                    DownPressed = false;
                    break;
                case VirtualKey.Down:
                    UpPressed = false;
                    LeftPressed = false;
                    RightPressed = false;
                    DownPressed = true;
                    break;
            }

        }
    }
}
