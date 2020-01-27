using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace game.maze
{
    /// <summary>
    ///     IGameAttributes handles attributes assosiated with a Game
    ///     Note: Work in progress
    /// </summary>
    public interface IGameAttributes
    {
        string WallCharacter { get; set; }
        string EmptyCharacter { get; set; }
        string StartCharacter { get; set; }
        string FinishCharacter { get; set; }
        string CurrentCharecter { get; set; }
        int Height { get; set; }
        int Width { get; set; }
    }
}
