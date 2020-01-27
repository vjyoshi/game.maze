using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace game.maze
{
    interface IPlayer
    {
        IGame Game { get; set; }

        void InitiateGame();
        void PlayGame();
        void QuitGame();

        /*
    Turn left and right
    Understand what is in front of them
    Understand all movement options from their given location
        */
        Point CurrentPosition { get; set; }

    }
}
