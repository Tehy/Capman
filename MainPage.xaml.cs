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

        //ghost
        private Ghost ghost;
        //private List<Ghost> ghosts;

        // Food

        private List<Food> foods;
        private List<Superfood> superfoods;


        // Wall

        private List<Wall> walls;


        // which keys are pressed

        private bool UpPressed;
        private bool LeftPressed;
        private bool RightPressed;
        private bool DownPressed;

        // Ghost movement
        private bool Up;
        private bool Left;
        private bool Right;
        private bool Down;

        // Was there a collision with wall
        private bool WallCollision;
        private bool GhostWallCollision;

        // super food eaten?
        private bool SuperfoodEaten;

        // counter for superfood
        private int SuperFoodCounter = 0;

        // player lives
        private int lives = 3;

        // Points
        private int points = 0; 

        // game loop timer
        private DispatcherTimer timer;

        private int WantToMoveDir = 0;
        private int LastOkMoveDir = 0;


        public MainPage()
        {

            this.InitializeComponent();

            // initialize list of food
            foods = new List<Food>();

            // initialize list of superfood
            superfoods = new List<Superfood>();

            // initialize wall list
            walls = new List<Wall>();

            


            // MapMatrix
            int[,] array2D = new int[25, 25]

{
{3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
{3, 2, 1, 1, 1, 1, 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 1, 1, 1, 1, 1, 2, 3},
{3, 1, 3, 3, 3, 3, 1, 1, 1, 3, 1, 3, 3, 3, 1, 3, 1, 3, 1, 3, 3, 3, 3, 1, 3},
{3, 1, 3, 1, 1, 1, 1, 3, 3, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 1, 3},
{3, 1, 3, 1, 3, 3, 1, 1, 1, 1, 1, 3, 3, 3, 1, 3, 3, 1, 3, 3, 3, 1, 3, 1, 3},
{3, 1, 3, 1, 3, 1, 1, 3, 3, 1, 3, 3, 1, 1, 1, 1, 3, 1, 3, 3, 3, 1, 3, 1, 3},
{3, 1, 1, 1, 3, 1, 3, 3, 1, 1, 1, 3, 1, 3, 3, 1, 3, 1, 1, 1, 1, 1, 1, 1, 3},
{3, 1, 3, 1, 3, 1, 1, 2, 1, 3, 1, 1, 1, 1, 1, 1, 3, 3, 3, 1, 3, 3, 3, 3, 3},
{3, 1, 3, 1, 1, 1, 3, 1, 3, 3, 1, 3, 1, 3, 3, 1, 3, 1, 1, 1, 1, 1, 1, 1, 3},
{3, 1, 3, 3, 3, 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 2, 3, 3, 3, 1, 3, 1, 3},
{3, 1, 1, 1, 1, 1, 3, 1, 3, 3, 3, 4, 3, 3, 3, 1, 3, 1, 3, 1, 1, 1, 1, 1, 3},
{3, 3, 3, 1, 3, 1, 3, 1, 3, 4, 4, 4, 4, 4, 3, 1, 3, 1, 3, 1, 3, 1, 3, 1, 3},
{3, 2, 1, 1, 3, 1, 1, 1, 3, 4, 4, 4, 4, 4, 3, 1, 1, 1, 3, 1, 3, 1, 3, 2, 3},
{3, 1, 3, 1, 3, 1, 3, 1, 3, 4, 4, 4, 4, 4, 3, 1, 3, 1, 1, 1, 3, 1, 3, 1, 3},
{3, 1, 1, 1, 1, 1, 3, 1, 3, 3, 3, 4, 3, 3, 3, 1, 3, 3, 3, 1, 3, 1, 3, 1, 3},
{3, 1, 3, 1, 3, 3, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3},
{3, 1, 3, 1, 1, 1, 3, 1, 3, 3, 3, 1, 3, 3, 3, 3, 3, 3, 1, 3, 3, 3, 1, 3, 3},
{3, 1, 3, 3, 3, 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3},
{3, 1, 1, 1, 3, 1, 3, 1, 3, 3, 3, 3, 3, 3, 1, 3, 3, 3, 1, 3, 1, 3, 3, 1, 3},
{3, 3, 3, 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 1, 2, 1, 3, 1, 1, 3, 1, 3},
{3, 1, 1, 1, 3, 3, 1, 3, 3, 3, 1, 3, 1, 3, 1, 3, 1, 3, 3, 3, 3, 1, 3, 1, 3},
{3, 1, 3, 1, 1, 1, 2, 3, 1, 1, 1, 3, 1, 3, 1, 1, 1, 3, 1, 1, 1, 1, 3, 1, 3},
{3, 1, 3, 3, 3, 3, 3, 3, 1, 3, 1, 3, 1, 3, 3, 3, 1, 1, 1, 3, 3, 3, 3, 1, 3},
{3, 2, 1, 1, 1, 1, 1, 1, 1, 3, 1, 1, 1, 1, 1, 1, 1, 3, 1, 1, 1, 1, 1, 2, 3},
{3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3}
};

            // Create the map by using matrix

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

                    else if (array2D[j, i] == 2)
                    {
                        // create a new superfood
                        Superfood superfood = new Superfood();
                        // set location 
                        superfood.LocationX = i * 40;
                        superfood.LocationY = j * 40;
                        // add to game canvas
                        MyCanvas.Children.Add(superfood);
                        // set food location in canvas
                        superfood.SetLocation();
                        // add to foods list (for collision checking)
                        superfoods.Add(superfood);
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

           
            // Add Ghost
            ghost = new Ghost
            {
                LocationX = MyCanvas.Width / 2 - 55,
                LocationY = MyCanvas.Height / 2
            };
            MyCanvas.Children.Add(ghost);

            Up = true;
           
           // RandomDirectionGhost();

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
             Superfood superfood = new Superfood();
             // set location with mouse position
             superfood.LocationX = args.CurrentPoint.Position.X - superfood.Width / 2;
            superfood.LocationY = args.CurrentPoint.Position.Y - superfood.Height / 2;
             // add to game canvas
             MyCanvas.Children.Add(superfood);
            // set food location in canvas
            superfood.SetLocation();
            // add to foods list (for collision checking)
            superfoods.Add(superfood);
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

            ghost.SetLocation();

            if (UpPressed) player.MoveUp();
            else if (DownPressed) player.MoveDown();
            else if (RightPressed) player.MoveRight();
            else if (LeftPressed) player.MoveLeft();
            CheckCollisionWall();
            if (WallCollision == true && WantToMoveDir != LastOkMoveDir)
            {

                if (LastOkMoveDir == 1)
                {
                    player.MoveUp();
                    UpPressed = true;
                }
                else if (LastOkMoveDir == 3)
                {
                    player.MoveDown();
                    DownPressed = true;
                }
                else if (LastOkMoveDir == 2)
                {
                    player.MoveRight();
                    RightPressed = true;
                }
                else if (LastOkMoveDir == 4)
                {
                    player.MoveLeft();
                    LeftPressed = true;
                }

                CheckCollisionWall();
                player.SetLocation();

            }

            else if (WallCollision == false)
            {
                
                player.SetLocation();
                LastOkMoveDirCheck();
            }
            /*else if (WallCollision == true && WantToMoveDir != LastOkMoveDir)
            {

                if (LastOkMoveDir == 1) player.MoveUp();
                else if (LastOkMoveDir == 3) player.MoveDown();
                else if (LastOkMoveDir == 2) player.MoveRight();
                else if (LastOkMoveDir == 4) player.MoveLeft();
                CheckCollisionWall();
                player.SetLocation();

            }*/


                if (Up) ghost.MoveUp();
            if (Down) ghost.MoveDown();
            if (Right) ghost.MoveRight();
            if (Left) ghost.MoveLeft();
            CheckCollisionGhost();
            CheckCollisionGhostWall();
            if (GhostWallCollision == false)
            {
                ghost.SetLocation();
            }

            Superfoodtimer();

            // check collisions alle
            CheckCollisionFood();
            CheckCollisionSuperFood();
            CheckCollisionGhost();
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
                    points += 1;
                    
                  
                    break;
                }
            }
        }

        //check collisions with Superfood dots
        private void CheckCollisionSuperFood()
        {
            foreach (Superfood superfood in superfoods)
            {
                // get rects
                Rect Brect = new Rect(
                    player.LocationX,
                    player.LocationY,
                    player.ActualWidth,
                    player.ActualHeight
                    );

                Rect FRect = new Windows.Foundation.Rect(
                    superfood.LocationX,
                    superfood.LocationY,
                    superfood.ActualWidth,
                    superfood.ActualHeight
                    );
                // does these intersects?
                Brect.Intersect(FRect);
                // is Brect empty?
                if (!Brect.IsEmpty)
                {
                    // remove food
                    MyCanvas.Children.Remove(superfood);
                    superfoods.Remove(superfood);
                    points += 5;
                    SuperfoodEaten = true;

                    break;
                }
            }
        }

        // check player collision on ghost
        private void CheckCollisionGhost()
        {
            // get rects
            Rect Brect = new Rect(
                player.LocationX,
                player.LocationY,
                player.ActualWidth,
                player.ActualHeight
                );

            Rect FRect = new Windows.Foundation.Rect(
                ghost.LocationX,
                ghost.LocationY,
                ghost.ActualWidth,
                ghost.ActualHeight
                );
            // does these intersects?
            Brect.Intersect(FRect);
            // is Brect empty?
            if (!Brect.IsEmpty)
            {
                // remove player pr ghost
                if (SuperfoodEaten == true)
                {
                    // remove ghost and create a new one going up from the middle
                    MyCanvas.Children.Remove(ghost);
                    points += 20;
                    // Add Ghost
                    Up = true;
                    Down = false;
                    Right = false;
                    Left = false;

                    ghost = new Ghost
                    {
                        LocationX = MyCanvas.Width / 2 - 55,
                        LocationY = MyCanvas.Height / 2
                    };
                    MyCanvas.Children.Add(ghost);

                    Up = true;
                }
               
                // kill the player
                else
                {
                    MyCanvas.Children.Remove(player);
                    lives -= 1;
                    if (lives > 0)
                    {
                        // add player
                        player = new Player
                        {
                            LocationX = MyCanvas.Width / 2,
                            LocationY = MyCanvas.Height / 2 + 105

                        };
                        MyCanvas.Children.Add(player);

                    }

                    else if (lives == 0 )
                    {
                        this.Frame.Navigate(typeof(MainMenu));
                    }
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


        // check ghost collisions with walls
        private void CheckCollisionGhostWall()
        {
            foreach (Wall wall in walls)
            {
                // get rects
                Rect Grect = new Rect(
                    ghost.LocationX,
                    ghost.LocationY,
                    ghost.ActualWidth,
                    ghost.ActualHeight
                    );

                Rect WRect = new Windows.Foundation.Rect(
                    wall.LocationX,
                    wall.LocationY,
                    wall.ActualWidth,
                    wall.ActualHeight
                    );
                // do these interact?
                Grect.Intersect(WRect);
                // is Brect empty?
                if (!Grect.IsEmpty)
                {
                    if (Up == true)
                    {
                        ghost.MoveDown();
                        Up = false;
                        Left = false;
                        Right = false;
                        Down = false;
                        GhostWallCollision = true;
                        RandomDirectionGhost();
                        break;
                    }
                    else if (Down == true)
                    {
                        ghost.MoveUp();
                        Up = false;
                        Left= false;
                        Right = false;
                        Down = false;
                        GhostWallCollision = true;
                        RandomDirectionGhost();
                        break;
                    }
                    else if (Left == true)
                    {
                        ghost.MoveRight();                        
                        Up = false;
                        Left = false;
                        Right = false;
                        Down = false;
                        GhostWallCollision = true;
                        RandomDirectionGhost();
                        break;
                    }
                    else if (Right == true)
                    {
                        ghost.MoveLeft();                      
                        Up = false;
                        Left = false;
                        Right = false;
                        Down = false;
                        GhostWallCollision = true;
                        RandomDirectionGhost();
                        break;
                    }

                }
                else
                {
                    GhostWallCollision = false;
                }
            }
        }

        private void RandomDirectionGhost()
        {
            Random rng = new Random();
            int suunta = rng.Next(0, 4);
            if (suunta == 0)
            {
                Up = true;               
            }
            else if (suunta == 1)
            {
                Down = true;
            }
            else if (suunta == 2)
            {
                Left = true;
            }
            else if (suunta == 3)
            {
                Right = true;
            }

        }

        // is superfood still usable (6 seconds from eating)
        private void Superfoodtimer()
        {
            if (SuperfoodEaten == true)
            {
                if (SuperFoodCounter < 360)
                {
                    SuperFoodCounter += 1;
                }
                else
                {
                    SuperfoodEaten = false;
                }
            }
        }

        private void LastOkMoveDirCheck()
      
        {
            if (UpPressed == true)
            {

                LastOkMoveDir = 1;
            }
            else if (RightPressed == true)
            {
                LastOkMoveDir = 2;
            }

            else if (DownPressed == true)
            {
                LastOkMoveDir = 3;
            }

            else if (LeftPressed == true)
            {
                LastOkMoveDir = 4;

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
                    WantToMoveDir = 1;
                    break;
                case VirtualKey.Left:
                    UpPressed = false;
                    LeftPressed = true;
                    RightPressed = false;
                    DownPressed = false;
                    WantToMoveDir = 4;
                    break;
                case VirtualKey.Right:
                    UpPressed = false;
                    LeftPressed = false;
                    RightPressed = true;
                    DownPressed = false;
                    WantToMoveDir = 2;
                    break;
                case VirtualKey.Down:
                    UpPressed = false;
                    LeftPressed = false;
                    RightPressed = false;
                    DownPressed = true;
                    WantToMoveDir = 3;
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
