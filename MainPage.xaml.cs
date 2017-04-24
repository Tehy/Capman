﻿using System;
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

        // Wall

        private List<Wall> walls;


        // which keys are pressed

        private bool UpPressed;
        private bool LeftPressed;
        private bool RightPressed;
        private bool DownPressed;

        // Was there a collision with wall
        private bool WallCollision;

        // game loop timer
        private DispatcherTimer timer;

        


        public MainPage()
        {

            this.InitializeComponent();

          

       
          

            // initialize list of food
            foods = new List<Food>();

            // initialize wall list
            walls = new List<Wall>();


            // MapMatrix
            int[,] array2D = new int[25, 25]

{
{3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
{3, 1, 1, 1, 1, 1, 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3},
{3, 1, 3, 3, 3, 3, 1, 1, 1, 3, 1, 3, 3, 3, 1, 3, 3, 3, 1, 3, 3, 3, 3, 1, 3},
{3, 1, 3, 1, 1, 1, 1, 3, 3, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 1, 3},
{3, 1, 3, 1, 3, 3, 1, 1, 1, 1, 1, 3, 3, 3, 1, 3, 3, 1, 3, 3, 3, 1, 3, 1, 3},
{3, 1, 3, 1, 3, 1, 1, 3, 3, 1, 3, 3, 1, 1, 1, 1, 3, 1, 3, 3, 3, 1, 3, 1, 3},
{3, 1, 1, 1, 3, 1, 3, 3, 1, 1, 1, 3, 1, 3, 3, 1, 3, 1, 1, 1, 1, 1, 1, 1, 3},
{3, 1, 3, 1, 3, 1, 1, 1, 1, 3, 1, 1, 1, 1, 1, 1, 3, 3, 3, 1, 3, 3, 3, 1, 3},
{3, 1, 3, 1, 1, 1, 3, 1, 3, 3, 1, 3, 1, 3, 3, 1, 3, 1, 1, 1, 1, 1, 3, 1, 3},
{3, 1, 3, 3, 3, 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 1, 3, 3, 3, 1, 3, 1, 3},
{3, 1, 1, 1, 1, 1, 3, 1, 3, 3, 3, 4, 3, 3, 3, 1, 3, 1, 3, 1, 1, 1, 1, 1, 3},
{3, 1, 3, 1, 3, 1, 3, 1, 3, 4, 4, 4, 4, 4, 3, 1, 3, 1, 3, 1, 3, 1, 3, 1, 3},
{3, 1, 3, 1, 3, 1, 1, 1, 3, 4, 4, 4, 4, 4, 3, 1, 1, 1, 3, 1, 3, 1, 3, 1, 3},
{3, 1, 3, 1, 3, 1, 3, 1, 3, 4, 4, 4, 4, 4, 3, 1, 3, 1, 1, 1, 3, 1, 3, 1, 3},
{3, 1, 1, 1, 1, 1, 3, 1, 3, 3, 3, 3, 3, 3, 3, 1, 3, 3, 3, 1, 3, 1, 3, 1, 3},
{3, 1, 3, 1, 3, 3, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3},
{3, 1, 3, 1, 1, 1, 3, 1, 3, 3, 3, 1, 3, 3, 3, 3, 3, 3, 1, 3, 3, 3, 3, 1, 3},
{3, 1, 3, 3, 3, 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3},
{3, 1, 1, 1, 3, 1, 3, 1, 3, 3, 3, 3, 3, 3, 1, 3, 3, 3, 1, 3, 1, 3, 3, 1, 3},
{3, 1, 3, 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 1, 1, 1, 3, 1, 1, 3, 1, 3},
{3, 1, 3, 1, 3, 3, 1, 3, 3, 3, 1, 3, 1, 3, 1, 3, 1, 3, 3, 3, 3, 1, 3, 1, 3},
{3, 1, 3, 1, 1, 1, 1, 3, 1, 1, 1, 3, 1, 3, 1, 1, 1, 3, 1, 1, 1, 1, 3, 1, 3},
{3, 1, 3, 3, 3, 3, 3, 3, 1, 3, 3, 3, 1, 3, 3, 3, 1, 3, 1, 3, 3, 3, 3, 1, 3},
{3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3},
{3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3}
};


            for (int i = 0; i < 25; i++)


            {
                for (int j = 0; j < 25; j++)
                {
                    if (array2D[j, i] == 0)
                    {
                        Player player = new Player();
                        
                            player.LocationX = i * 40;
                            player.LocationY = j * 40;

                        
                        MyCanvas.Children.Add(player);
                        player.SetLocation();
                    }

                    else if (array2D[j, i] == 1)
                    {
                        // create a new food
                        Food food = new Food();
                        // set location with mouse position
                        food.LocationX = i * 40;
                        food.LocationY = j * 40;
                        // add to game canvas
                        MyCanvas.Children.Add(food);
                        // set food location in canvas
                        food.SetLocation();
                        // add to foods list (for collision checking)
                        foods.Add(food);
                    }

                    else if (array2D[j, i] == 3)
                    {
                        // create a new wall
                        Wall wall = new Wall();
                        // set location 
                        {
                            wall.LocationX = i * 40;
                            wall.LocationY = j * 40;
                        };
                        MyCanvas.Children.Add(wall);
                        // set wall location in canvas
                        wall.SetLocation();
                        // add to walls list (for collision checking)
                        walls.Add(wall);
                    }

                }
            }

            //CreateWalls();

            // add player
            player = new Player
            {
                LocationX = MyCanvas.Width / 2,
                LocationY = MyCanvas.Height / 2 + 105

            };
            MyCanvas.Children.Add(player);

           

            // key Listener

            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
     

            // Timer

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1000 / 60);
            timer.Tick += Timer_Tick;
            timer.Start(); //startgame


            // Mouse listener (to test object creation
            Window.Current.CoreWindow.PointerPressed += CoreWindow_PointerPressed;

        }

        private void CoreWindow_PointerPressed(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.PointerEventArgs args)
         {
             // create a new food
             Food food = new Food();
             // set location with mouse position
             food.LocationX = args.CurrentPoint.Position.X - food.Width / 2;
             food.LocationY = args.CurrentPoint.Position.Y - food.Height / 2;
             // add to game canvas
             MyCanvas.Children.Add(food);
             // set food location in canvas
             food.SetLocation();
             // add to foods list (for collision checking)
             foods.Add(food);
         }
         
        /*
        private void CoreWindow_PointerPressed(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.PointerEventArgs args)
        {
            // create a new wall
            Wall wall = new Wall();
            // set location with mouse position
            wall.LocationX = args.CurrentPoint.Position.X - wall.Width / 2;
            wall.LocationY = args.CurrentPoint.Position.Y - wall.Height / 2;
            // add to game canvas
            MyCanvas.Children.Add(wall);
            // set wall location in canvas
            wall.SetLocation();
            // add to walls list (for collision checking)
            walls.Add(wall);
        }*/


        // Game timer (THE MOST IMPORTANT THING HERE)

        private void Timer_Tick(object sender, object e)
        {
            
            if (UpPressed) player.MoveUp();
            if (DownPressed) player.MoveDown();
            if (RightPressed) player.MoveRight();
            if (LeftPressed) player.MoveLeft();
            CheckCollisionWall();
            if (WallCollision == false)
            {
                player.SetLocation();
            }
          
            // check collisions alle
            CheckCollisionFood();

        }

        //check collisions with food dots
        private void CheckCollisionFood()
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
                    // remove food
                    MyCanvas.Children.Remove(food);
                    foods.Remove(food);
                    
                  
                    break;
                }
            }
        }

        // check collisions with walls
        private void CheckCollisionWall()
        {
            foreach (Wall wall in walls)
            {
                // get rects
                Rect Prect = new Rect(
                    player.LocationX,
                    player.LocationY,
                    player.ActualWidth,
                    player.ActualHeight
                    );

                Rect WRect = new Windows.Foundation.Rect(
                    wall.LocationX,
                    wall.LocationY,
                    wall.ActualWidth,
                    wall.ActualHeight
                    );
                // do these interact?
                Prect.Intersect(WRect);
                // is Brect empty?
                if (!Prect.IsEmpty)
                {
                    if (UpPressed == true)
                    {
                        player.MoveDown();
                        UpPressed = false;
                        LeftPressed = false;
                        RightPressed = false;
                        DownPressed = false;
                        WallCollision = true;
                        break;
                    }
                    else if (DownPressed == true)
                    {
                        player.MoveUp();
                        UpPressed = false;
                        LeftPressed = false;
                        RightPressed = false;
                        DownPressed = false;
                        WallCollision = true;
                        break;
                    }
                    else if (LeftPressed == true)
                    {
                        player.MoveRight();
                        UpPressed = false;
                        LeftPressed = false;
                        RightPressed = false;
                        DownPressed = false;
                        WallCollision = true;
                        break;
                    }
                    else if (RightPressed == true)
                    {
                        player.MoveLeft();
                        UpPressed = false;
                        LeftPressed = false;
                        RightPressed = false;
                        DownPressed = false;
                        WallCollision = true;
                        break;
                    }
                 
                }
                else
                {
                    WallCollision = false;
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

        // Luodaan reunamuurit (ei kaytossa)
        private void CreateWalls()
        {
            for (int i = 0; i < 50; i++)
            {// create a new wall

                Wall wall = new Wall();
                // set location 
                wall.LocationX = i * 20;
                wall.LocationY = 0;
                // add to game canvas
                MyCanvas.Children.Add(wall);
                // set wall location in canvas
                wall.SetLocation();
                // add to walls list (for collision checking)
                walls.Add(wall);
            }

            for (int i = 0; i < 50; i++)
            {// create a new wall

                Wall wall = new Wall();
                // set location 
                wall.LocationX = i * 20;
                wall.LocationY = 980;
                // add to game canvas
                MyCanvas.Children.Add(wall);
                // set wall location in canvas
                wall.SetLocation();
                // add to walls list (for collision checking)
                walls.Add(wall);
            }

            for (int i = 0; i < 50; i++)
            {// create a new wall

                Wall wall = new Wall();
                // set location 
                wall.LocationX = 0;
                wall.LocationY = i * 20;
                // add to game canvas
                MyCanvas.Children.Add(wall);
                // set wall location in canvas
                wall.SetLocation();
                // add to walls list (for collision checking)
                walls.Add(wall);
            }

            for (int i = 0; i < 50; i++)
            {// create a new wall

                Wall wall = new Wall();
                // set location 
                wall.LocationX = 980;
                wall.LocationY = i * 20;
                // add to game canvas
                MyCanvas.Children.Add(wall);
                // set wall location in canvas
                wall.SetLocation();
                // add to walls list (for collision checking)
                walls.Add(wall);
            }

           
        }
    }
}
