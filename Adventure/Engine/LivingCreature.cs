using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class LivingCreature
    {
        public int MaximumMeatLeft { get; set; }
        public int CurrentMeatLeft { get; set; }

        public LivingCreature(int currentMeatLeft, int maximumMeatLeft)
        {
            CurrentMeatLeft = currentMeatLeft;
            MaximumMeatLeft = maximumMeatLeft;
        }
    }
}
