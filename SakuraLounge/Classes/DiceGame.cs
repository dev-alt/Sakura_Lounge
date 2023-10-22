using System;

namespace SakuraLounge.Classes
{
    internal class DiceGame
    {
        private Random random;
        public int Roll1 { get; private set; }
        public int Roll2 { get; private set; }
        public int Roll3 { get; private set; }

        public bool Dice1Flag { get; private set; }
        public bool Dice2Flag { get; private set; }
        public bool Dice3Flag { get; private set; }

        public DiceGame()
        {
            random = new Random(DateTime.Now.Millisecond);
        }

        public void RollDice()
        {
            if (!Dice1Flag)
            {
                Roll1 = random.Next(1, 7);
            }
            if (!Dice2Flag)
            {
                Roll2 = random.Next(1, 7);
            }
            if (!Dice3Flag)
            {
                Roll3 = random.Next(1, 7);
            }
        }


    }
}
