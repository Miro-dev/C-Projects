using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Place
    {
        public int ID { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        //public PictureBox {get;set;}
        //public Location Location { get; set; }
        //public int Event { get; set; }

        public Place(int id,int x, int y)
        {
            ID = id;
            X = x;
            Y = y;
        }
    }
}
