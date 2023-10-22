using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace SakuraLounge.Classes
{
    internal class Dragon : INotifyPropertyChanged
    {
        private int health;

        public int Health
        {
            get { return health; }
            set
            {
                if (health != value)
                {
                    health = value;
                    OnPropertyChanged("Health");
                }
            }
        }

        public int Strength { get; set; }
        public int Power { get; set; }
        public int Luck { get; set; }

        public Dragon(int health, int strength, int power, int luck)
        {
            Health = health;
            Strength = strength;
            Power = power;
            Luck = luck;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}