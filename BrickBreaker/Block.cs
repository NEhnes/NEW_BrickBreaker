using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml;
using System.Security.Cryptography;

namespace BrickBreaker
{
    public class Block
    {
        public int width = 32;
        public int height = 32;

        public int x;
        public int y;
        public Color colour;


        public Block(int _x, int _y, Color _colour)
        {
            x = _x;
            y = _y;
            colour = _colour;


        }

        public void AddScore()
        {
            // has to be a switch because Color.Blank is not a constant
            if (colour == Color.Red)
            {
                GameScreen.score += 100;
            }
            else if (colour == Color.Blue)
            {
                GameScreen.score += 200;
            }
            else if (colour == Color.Pink)
            {
                GameScreen.score += 300;
            }
            else if (colour == Color.Yellow)
            {
                GameScreen.score += 400;
            }
            else if (colour == Color.Gray)
            {
                GameScreen.score += 500;
            }
        }
    }
}
