using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace game.maze
{
    public interface IGame
    {
        IGameAttributes Attributes { get; set; }
        Cell GetStartingPosition();
        string GetValueByCoordinate(int row, int column);
        event EventHandler GameStarted;
        event EventHandler GameCompleted;
        void Navigate(ConsoleKeyInfo key);
        StatusEnum GameStatus { get; set; }
    }
}
