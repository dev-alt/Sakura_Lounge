using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace SakuraLounge.Classes
{
    class Lotto
    {
        private int[] _numArray;
        private Random _randomNumber;

        public Lotto()
        {
            _numArray = new int[6];
            _randomNumber = new Random(DateTime.Now.Millisecond);
        }

        public void GenerateNumbers()
        {
            for (int i = 0; i < 6; i++)
            {
                _numArray[i] = _randomNumber.Next(1, 50);
            }

        }

        public void PrintNumbers(TextBlock outputTextBlock)
        {
            for (int loop = 0; loop < 6; loop++)
            {
                string numberString = _numArray[loop].ToString();
                if (_numArray[loop] < 10)
                {
                    numberString = " " + numberString;
                }
                outputTextBlock.Text += numberString + " ";
            }
        }

        public void SetNumbersToZero()
        {
            for (int i = 0; i < 6; i++)
            {
                _numArray[i] = 0;
            }
        }

    }
}