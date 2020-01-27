using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game.maze
{
    public class Maze : IGame
    {

        private const int _rowDimension = 0;
        private const int _columnDimension = 1;

        List<Cell> Cells { get; set; }
        Random rand = new Random();
        //  Starting cell within the maze - Random cell
        public Cell StartCell = new Cell();
        //  Current cell within the Maze 
        public Cell CurrentCell = null;
        //  Class to keep track of maze navigations
        List<Cell> NavigatedCells = null;

        //  Event handler to trigger Game Completion
        public event EventHandler GameCompleted;
        protected virtual void OnGameCompleted(EventArgs e)
        {
            EventHandler handler = GameCompleted;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        
        public bool MazeCreated
        {
            get;
            set;
        }

        public IGameAttributes Attributes
        { get; set; }
        private Random random = new Random();

        public Maze()
            : this(3, 3)
        {

        }

        public Maze(int rows, int columns)
        {
            Attributes = new MazeAttributes();
            Attributes.WallCharacter = "X";
            Attributes.EmptyCharacter = " ";
            Attributes.StartCharacter = "S";
            Attributes.FinishCharacter = "F";
            Attributes.Height = rows;
            Attributes.Width = columns;
            Cells = new List<Cell>();
            Cells = Initialise(rows, columns);
        }

        public List<Cell> Initialise(int rows, int columns)
        {

            List<Cell> cells = new List<Cell>();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Cell cell = new Cell();
                    cell.X = i;
                    cell.Y = j;
                    cell.Value = "X";
                    cell.Visited = false;
                    cells.Add(cell);
                }
            }
            Cells = cells;
            int randomPosition = rand.Next(1, 9);
            Cell randomCell = cells.Find(cell => cell.X == randomPosition && cell.Y == randomPosition);
            //Position randomCell = cells.Find(cell => cell.X == 8 && cell.Y == 8);
            randomCell.Value = "S";
            randomCell.Visited = true;
            StartCell = randomCell;

            CurrentCell = new Cell();
            CurrentCell.X = StartCell.X;
            CurrentCell.Y = StartCell.Y;
            CurrentCell.Value = "C";

            RecordNavigation(StartCell.X, StartCell.Y, StartCell.Value);

            CarvePassage(randomPosition, randomPosition, randomCell);

            MazeCreated = true;
            GameStatus = StatusEnum.InProgress;
            //CarvePassage(8, 8, randomCell);
            return cells;

        }

        private void RecordNavigation(int x, int y, string value)
        {
            if (NavigatedCells == null || NavigatedCells.Count == 0)
                NavigatedCells = new List<Cell>();
            NavigatedCells.Add(new Cell(x, y, value));
        }

        public string GetNavigationPath()
        {
            StringBuilder path = new StringBuilder();
            
            foreach (var cell in NavigatedCells)
            {
                if (path.Length == 0)
                {
                    path.Append(string.Format("Cell({0}, {1})", cell.X + 1, cell.Y + 1));
                }
                else
                {
                    path.Append(string.Format("=>Cell({0}, {1})", cell.X + 1, cell.Y + 1));
                }
            }
            return path.ToString();
        }

        public void Print()
        {
            for (int i = 0; i < Attributes.Height; i++)
            {
                for (int j = 0; j < Attributes.Width; j++)
                {
                    if (CurrentCell.X == i && 
                        CurrentCell.Y == j && 
                        (StartCell.Y != CurrentCell.Y ||  
                        StartCell.X != CurrentCell.X))
                    {
                        Console.Write(CurrentCell.Value);
                    }
                    else
                    {
                        Console.Write(Cells.Find(cell => cell.X == i && cell.Y == j).Value);
                    }
                }
                Console.WriteLine();
            }
            
        }

        public void CarvePassage(int currentX, int currentY, Cell cell)
        {
            Cell newCell = GetNextUnvisitedCell(cell);
            if(newCell == null)
            {
                cell.Value = "F";
                return;
            }

            newCell.Value = " ";
            newCell.Visited = true;
            Debug.Print("Current X:" + currentX.ToString() + ": currentY:" + currentY.ToString() + ";cell Value:" + cell.Value);
            CarvePassage(newCell.X, newCell.Y, newCell);
        }

        private Cell GetNextUnvisitedCell(Cell cell)
        {
            Cell newCell = new Cell();

            int direction = rand.Next(1, 4);
            //int direction = 1;
            Debug.Print("direction:" + direction.ToString());
            if (direction == 1)
            {
                if (cell.Y + 1 < Attributes.Width-1)
                {
                    newCell = Cells.Find(unvisitedCell => unvisitedCell.X == cell.X &&
                                      unvisitedCell.Y == cell.Y + 1);
                    if (newCell != null && !newCell.Visited)
                    {
                        newCell.PreviousCell = cell;
                        return newCell;
                    }
                }
                direction = 2;
            }

            if(direction == 2)
            {
                if (cell.Y - 1 > 0)
                {
                    newCell = Cells.Find(unvisitedCell => unvisitedCell.X == cell.X &&
                                      unvisitedCell.Y == cell.Y - 1);
                    if (newCell != null && !newCell.Visited)
                    {
                        newCell.PreviousCell = cell;
                        return newCell;
                    }
                }
                direction = 3;
            }

            if (direction == 3)
            {
                if (cell.X + 1 < Attributes.Height-1)
                {
                    newCell = Cells.Find(unvisitedCell => unvisitedCell.X == cell.X  + 1 &&
                                      unvisitedCell.Y == cell.Y);
                    if (newCell != null && !newCell.Visited)
                    {
                        newCell.PreviousCell = cell;
                        return newCell;
                    }
                }
                direction = 4;
            }

            if (direction == 4)
            {
                if (cell.X - 1 > 0)
                {
                    newCell = Cells.Find(unvisitedCell => unvisitedCell.X == cell.X - 1 &&
                                      unvisitedCell.Y == cell.Y);
                    if (newCell != null && !newCell.Visited)
                    {
                        newCell.PreviousCell = cell;
                        return newCell;
                    }
                }
            }
            return null;
        }

        public int GetEmptySpaces()
        {
            return Cells.FindAll(cell => cell.Value == " ").Count;
        }

        public int GetCharactersSpaces()
        {
            return Cells.FindAll(cell => cell.Value == "X").Count;
        }
        

        public string GetValueByCoordinate(int row, int column)
        {
            if((row <= 0 || row > Attributes.Height) || (column <= 0 || column > Attributes.Width))
            {
                throw new IndexOutOfRangeException("Cell coordinates out of range. Please pass valid coordinates.");
            }

            return Cells.Find(item => item.X == row && item.Y == column).Value;
        }

        public Cell GetStartingPosition()
        {
            if (Cells == null || Cells.Count == 0)
                throw new InvalidOperationException("Maze not constructed. Please construct the maze before performing any operations");

            return Cells.Find(cell => cell.Value == "S");
        }


        public Cell GetFinishingPosition()
        {
            if (Cells == null || Cells.Count == 0)
                throw new InvalidOperationException("Maze not constructed. Please construct the maze before performing any operations");

            return Cells.Find(cell => cell.Value == "F");
        }


        //  Navigate handler and related functions
        public void Navigate(ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    MoveUp();
                    break;
                case ConsoleKey.DownArrow:
                    MoveDown();
                    break;
                case ConsoleKey.LeftArrow:
                    MoveLeft();
                    break;
                case ConsoleKey.RightArrow:
                    MoveRight();
                    break;
                default:
                    break;
            }
        }

        private void MoveRight()
        {
            if (IsRightValid(CurrentCell))
            {
                CurrentCell.Y = CurrentCell.Y + 1;
                RecordNavigation(CurrentCell.X, CurrentCell.Y, CurrentCell.Value);
                if (IsGameFinished())
                {
                    GameStatus = StatusEnum.Completed;
                    GameCompleted(this, new EventArgs());
                }
            }
        }

        private bool IsRightValid(Cell CurrentCell)
        {
            Cell nextCell = Cells.Find(cell => cell.Y == CurrentCell.Y + 1 &&
                                       cell.Y + 1 < Attributes.Width &&
                                       cell.X == CurrentCell.X &&
                                       (cell.Value == " " || cell.Value == "F"));
            if (nextCell != null)
            {
                return true;
            }
            return false;
        }

        private void MoveLeft()
        {
            if (IsLeftValid(CurrentCell))
            {
                CurrentCell.Y = CurrentCell.Y - 1;
                RecordNavigation(CurrentCell.X, CurrentCell.Y, CurrentCell.Value);
                if (IsGameFinished())
                {
                    GameStatus = StatusEnum.Completed;
                    GameCompleted(this, new EventArgs());
                }
            }
        }

        private bool IsLeftValid(Cell CurrentCell)
        {
            Cell nextCell = Cells.Find(cell => cell.Y == CurrentCell.Y - 1 &&
                                       cell.Y + 1 > 0 &&
                                       cell.X == CurrentCell.X &&
                                       (cell.Value == " " || cell.Value == "F"));
            if (nextCell != null)
            {
                return true;
            }
            return false;
        }

        private void MoveDown()
        {
            if (IsDownValid())
            {
                CurrentCell.X = CurrentCell.X + 1;
                RecordNavigation(CurrentCell.X, CurrentCell.Y, CurrentCell.Value);
                if (IsGameFinished())
                {
                    GameStatus = StatusEnum.Completed;
                    GameCompleted(this, new EventArgs());
                }
            }
        }

        private bool IsGameFinished()
        {
            string nextCellValue = Cells.Find(cell => cell.X == CurrentCell.X && cell.Y == CurrentCell.Y).Value;
            if(nextCellValue == "F")
            {
                return true;
            }
            return false;
        }

        private bool IsDownValid()
        {
            Cell nextCell = Cells.Find(cell => cell.X == CurrentCell.X + 1 && 
                                       cell.X + 1 < Attributes.Height && 
                                       cell.Y == CurrentCell.Y && 
                                       (cell.Value == " " || cell.Value == "F"));
            if (nextCell != null)
            {
                return true;
            }
            return false;
        }

        private void MoveUp()
        {
            if(IsUpValid(CurrentCell))
            {
                CurrentCell.X = CurrentCell.X - 1;
                RecordNavigation(CurrentCell.X, CurrentCell.Y, CurrentCell.Value);
                if (IsGameFinished())
                {
                    GameStatus = StatusEnum.Completed;
                    GameCompleted(this, new EventArgs());
                }
            }
        }

        private bool GameFinished()
        {
            if (CurrentCell.Value == "F")
                return true;
            else
                return false;
        }

        private bool IsUpValid(Cell CurrentCell)
        {
            Cell nextCell = Cells.Find(cell => cell.X == CurrentCell.X - 1 &&
                                       cell.X + 1 > 0 &&
                                       cell.Y == CurrentCell.Y &&
                                       (cell.Value == " " || cell.Value == "F"));
            if (nextCell != null)
            {
                return true;
            }
            return false;
        }


        public event EventHandler GameStarted;


        public StatusEnum GameStatus
        {
            get;
            set;
        }
    }
}
