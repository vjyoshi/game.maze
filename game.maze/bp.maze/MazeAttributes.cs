using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game.maze
{
    public class MazeAttributes : IGameAttributes
    {
        public MazeAttributes(string wallCharacter, 
                    string emptyCharacter, 
                    string startCharacter, 
                    string finishCharacter, 
                    int height, 
                    int width)
        {
            this.WallCharacter = wallCharacter;
            this.EmptyCharacter = emptyCharacter;
            this.StartCharacter = startCharacter;
            this.FinishCharacter = finishCharacter;
            this.Height = height;
            this.Width = width;
        }

         public MazeAttributes()
        {
            WallCharacter = "X";
            EmptyCharacter = " ";
            StartCharacter = "S";
            FinishCharacter = "F";
            Height = 4;
            Width = 4;
        }

         public string WallCharacter
         {
             get;
             set;
         }

         public string EmptyCharacter
         {
             get;
             set;
         }

         public string StartCharacter
         {
             get;
             set;
         }

         public string FinishCharacter
         {
             get;
             set;
         }

         public int Height
         {
             get;
             set;
         }

         public int Width
         {
             get;
             set;
         }


         public string CurrentCharecter
         {
             get;
             set;
         }
    }
}
