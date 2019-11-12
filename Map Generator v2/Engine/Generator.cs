using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Generator
    {
        public int FieldWidth { get; set; }
        public int FieldHeight { get; set; }
        public int TileSide { get; set; }

        public Generator(int fieldWidth, int fieldHeight, int tileSide)
        {
            FieldWidth = fieldWidth;
            FieldHeight = fieldHeight;
            TileSide = tileSide;
            double TileBorder = TileSide * 33 / 100;

            int Rows = FieldHeight / TileSide;
            int Columns = 3;

            int[,] FieldGrid= new int [Rows, Columns];
        }
    }
}
