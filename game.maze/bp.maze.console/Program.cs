using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game.maze.console
{
    class Program
    {
        static Maze maze = null;
        static int Height = 6;
        static int Width = 6;
        const int MIN_HEIGHT = 6;
        const int MIN_WIDTH = 6;
        const int MAX_HEIGHT = 20;
        const int MAX_WIDTH = 20;

        static void Main(string[] args)
        {
            //Console.WriteLine();
            StartMazeGame();
        }

        private static void StartMazeGame()
        {
            int selectedMenu;
            string selected = "";
            do
            {
                Console.WriteLine("Welcome to Maze Play:");
                Console.WriteLine("*************************************");
                Console.WriteLine("1. Create & Print Maze");
                Console.WriteLine("2. Print Maze attributes");
                Console.WriteLine("3. Find cell value by coordinate");
                Console.WriteLine("4. Navigate in Maze");
                Console.WriteLine("X. Exit");
                Console.WriteLine("*************************************");
                Console.Write("Please select an option to proceed with:");

                selected = Console.ReadLine();
                int.TryParse(selected, out selectedMenu);
                if (selectedMenu < 1 && selectedMenu > 4)
                {
                    Console.WriteLine("Invalid menu option selected. Please select a valid menu option.");
                }
                else
                {
                    Console.Clear();
                    switch (selectedMenu)
                    {
                        case (1):
                            CreateAndPrintMaze();
                            break;

                        case (2):
                            PrintMazeAttributes();
                            break;

                        case (3):
                            FindCellValueByCoordinates();
                            break;

                        case (4):
                            NavigateMaze();
                            break;

                        default:
                            break;
                    }
                }
            } while (selected != "X");
        }

        private static void NavigateMaze()
        {
            Console.WriteLine("******************* Navigate in Maze **************");
            if (maze == null)
            {
                Console.WriteLine("Unable to navigate Maze Play not started. Pess <Enter> to return to Main Menu.");
                Console.ReadLine();
                return;
            }

            //maze.Print();
            //Console.WriteLine("Choose Up/Down/Left/Right arrow keys to navigate.");
            ConsoleKeyInfo key = new ConsoleKeyInfo();
            //Console.Write(key.KeyChar);
            maze.GameCompleted += maze_GameCompleted;
            do
            {
                if (maze.GameStatus == StatusEnum.Completed)
                {
                    break;
                }
                else
                {
                    maze.Navigate(key);
                    maze.Print();
                    Console.WriteLine("Navigation path so far: {0}", maze.GetNavigationPath());
                    Console.WriteLine("Choose Up/Down/Left/Right arrow keys to navigate ('C'=>current position). ");
                    key = Console.ReadKey();
                    Console.Clear();
                }
            } while (//key.Key != ConsoleKey.X || 
                maze.GameStatus != StatusEnum.Completed);
        }

        static void maze_GameCompleted(object sender, EventArgs e)
        {
            Console.WriteLine("Maze game completed. PRESS ANY KEY TO RETURN TO MAIN MENU");
        }

        private static void FindCellValueByCoordinates()
        {
            Console.WriteLine("******************* Find cell by coordinate **************");
            string sColumn = string.Empty;
            string sRow = string.Empty;
            do
            {
                if (maze == null)
                {
                    Console.WriteLine("Unable to get value for coordinates as Maze Play not started. Pess <Enter> to return to Main Menu.");
                    Console.ReadLine();
                    return;
                }
                Console.WriteLine("Enter 'X' to return to main menu:");
                Console.Write("Enter value for row:");
                sRow = Console.ReadLine();
                int row;
                Console.Write("Enter value for column:");
                sColumn = Console.ReadLine();
                int column;
                int.TryParse(sRow, out row);
                int.TryParse(sColumn, out column);
                if(row == 0 || column == 0)
                {
                    Console.WriteLine("Incorrect row/column values entered. Please enter correct values.");
                    return;
                }
                if (row < 0 || row > Height)
                {
                    Console.WriteLine(
                        string.Format("Incorrect row entered. Please enter a valid range ({0}-{1})", 0, Height));
                }

                if (column < 0 || column > Width)
                {
                    Console.WriteLine(
                        string.Format("Incorrect row entered. Please enter a valid range ({0}-{1})", 0, Width));
                }
                if(!maze.MazeCreated)
                {
                    Console.WriteLine("Maze not created. Unable to get value for passed in coordinates");
                    
                }
                else
                {
                    Console.WriteLine(string.Format("Value at coordinate ({0}, {1}) is: {2}", 
                            row, 
                            column, 
                            maze.GetValueByCoordinate(row, column)));
                }
            } while (sRow == "Y" || sColumn == "X");

            Console.WriteLine("******************* Find cell by coordinate **************");

        }

        private static void PrintMazeAttributes()
        {
            Console.WriteLine("******************* Maze Attributes **************");
            if (maze == null)
            {
                Console.WriteLine("Unable to navigate Maze Play not started. Pess <Enter> to return to Main Menu.");
                Console.ReadLine();
                return;
            }
            else
            {
                Console.WriteLine("Empty spaces: " + maze.GetEmptySpaces()); ;
                Console.WriteLine("Wall characters (X): " + maze.GetCharactersSpaces());
            }
            Console.WriteLine("******************* Maze Attributes **************");
        }

        private static void CreateAndPrintMaze()
        {
            Console.WriteLine("******************* Create & Print Maze **************");

            string sColumn = string.Empty;
            string sRow = string.Empty;
            do
            {
                Console.WriteLine("Enter 'X' to return to main menu:");
                Console.Write("Enter maze height:");
                sRow = Console.ReadLine();
                if (sRow == "X")
                    return;
                Console.Write("Enter maze width:");
                sColumn = Console.ReadLine();
                if (sColumn == "X")
                    return;
                int.TryParse(sRow, out Height);
                int.TryParse(sColumn, out Width);
                if (Height == 0 || Width == 0)
                {
                    Console.WriteLine("Incorrect row/column values entered. Please enter correct values.");
                    return;
                }
                if (Height < MIN_HEIGHT || Height > MAX_HEIGHT)
                {
                    Console.WriteLine(
                        string.Format("Incorrect row entered. Please enter a valid range ({0}-{1})", MIN_HEIGHT, MAX_HEIGHT));
                }

                if (Width < MIN_WIDTH || Width > MAX_WIDTH)
                {
                    Console.WriteLine(
                        string.Format("Incorrect row entered. Please enter a valid range ({0}-{1})", MIN_WIDTH, MAX_WIDTH));
                }

                //  Create Maze
                maze = new Maze(Height, Width);
                maze.Print();
            } while (sRow == "X" || sColumn == "X");

            Console.WriteLine("******************* Create & Print Maze **************");
        }
    }
}
