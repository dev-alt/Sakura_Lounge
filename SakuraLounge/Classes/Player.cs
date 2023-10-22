using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakuraLounge.Classes
{
    internal class Player
    {

        public int Strength { get; set; }
        public int Power { get; set; }
        public int Luck { get; set; }

        public Player( int strength, int power, int luck)
        {
            Strength = strength;
            Power = power;
            Luck = luck;
        }
    }
}
