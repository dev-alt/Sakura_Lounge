using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace SakuraLounge.Classes
{
     internal class GenerateFortune
    {
        public GenerateFortune() 
        {

        }
        public void Generate(TextBlock FortuneBox)
        {
            FortuneBox.Text += "Generated fortune";
        }
    }
}