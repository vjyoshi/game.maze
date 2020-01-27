using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace game.maze
{
    public class Cell
    {
        public int X { get; set; }
        public int Y { get;set;}
        public string Value { get; set; }
        public bool Visited { get; set; }
        public Cell PreviousCell { get; set; }

        public Cell() { }
        public Cell(int x, int y, string value)
        {
            X = x;
            Y = y;
            Value = value;
        }
    }
}
