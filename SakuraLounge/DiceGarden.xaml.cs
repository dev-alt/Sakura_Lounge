using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SakuraLounge
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DiceGarden : Page
    {


        Random _number;

        #region Player 1

        int _roll1;
        int _roll2;
        int _roll3;


        Boolean _dice1Flag = false;
        Boolean _dice2Flag = false;
        Boolean _dice3Flag = false;


        #endregion



        public DiceGarden()
        {
            this.InitializeComponent();
            _number = new Random(DateTime.Now.Millisecond);

        }

        public void Call()
        {

        }
        private void player1Roll_Click(object sender, RoutedEventArgs e)
        {
            _roll1 = _number.Next(1, 7);
            _roll2 = _number.Next(1, 7);
            _roll3 = _number.Next(1, 7);


            if (_dice1Flag == false)
            {
                if (_roll1 == 1) imageDice1.Source = new BitmapImage(new Uri("ms-appx:///Assets/one.png", UriKind.RelativeOrAbsolute));
                else if (_roll1 == 2) imageDice1.Source = new BitmapImage(new Uri("ms-appx:///Assets/two.png", UriKind.RelativeOrAbsolute));
                else if (_roll1 == 3) imageDice1.Source = new BitmapImage(new Uri("ms-appx:///Assets/three.png", UriKind.RelativeOrAbsolute));
                else if (_roll1 == 4) imageDice1.Source = new BitmapImage(new Uri("ms-appx:///Assets/four.png", UriKind.RelativeOrAbsolute));
                else if (_roll1 == 5) imageDice1.Source = new BitmapImage(new Uri("ms-appx:///Assets/five.png", UriKind.RelativeOrAbsolute));
                else imageDice1.Source = new BitmapImage(new Uri("ms-appx:///Assets/six.png", UriKind.RelativeOrAbsolute));
            }

            if (_dice2Flag == false)
            {
                if (_roll2 == 1) imageDice2.Source = new BitmapImage(new Uri("ms-appx:///Assets/one.png", UriKind.RelativeOrAbsolute));
                else if (_roll2 == 2) imageDice2.Source = new BitmapImage(new Uri("ms-appx:///Assets/two.png", UriKind.RelativeOrAbsolute));
                else if (_roll2 == 3) imageDice2.Source = new BitmapImage(new Uri("ms-appx:///Assets/three.png", UriKind.RelativeOrAbsolute));
                else if (_roll2 == 4) imageDice2.Source = new BitmapImage(new Uri("ms-appx:///Assets/four.png", UriKind.RelativeOrAbsolute));
                else if (_roll2 == 5) imageDice2.Source = new BitmapImage(new Uri("ms-appx:///Assets/five.png", UriKind.RelativeOrAbsolute));
                else imageDice2.Source = new BitmapImage(new Uri("ms-appx:///Assets/six.png", UriKind.RelativeOrAbsolute));
            }

            if (_dice3Flag == false)
            {
                if (_roll3 == 1) imageDice3.Source = new BitmapImage(new Uri("ms-appx:///Assets/one.png", UriKind.RelativeOrAbsolute));
                else if (_roll3 == 2) imageDice3.Source = new BitmapImage(new Uri("ms-appx:///Assets/two.png", UriKind.RelativeOrAbsolute));
                else if (_roll3 == 3) imageDice3.Source = new BitmapImage(new Uri("ms-appx:///Assets/three.png", UriKind.RelativeOrAbsolute));
                else if (_roll3 == 4) imageDice3.Source = new BitmapImage(new Uri("ms-appx:///Assets/four.png", UriKind.RelativeOrAbsolute));
                else if (_roll3 == 5) imageDice3.Source = new BitmapImage(new Uri("ms-appx:///Assets/five.png", UriKind.RelativeOrAbsolute));
                else imageDice3.Source = new BitmapImage(new Uri("ms-appx:///Assets/six.png", UriKind.RelativeOrAbsolute));
            }
                      
        }
        private void imageDice1_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (_dice1Flag == false)
            {
                _dice1Flag = true;
                imageDice1.Opacity = 0.5f;
            }
            else
            {
                _dice1Flag = false;
                imageDice1.Opacity = 1f;
            }
        }

        private void imageDice2_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (_dice2Flag == false)
            {
                _dice2Flag = true;
                imageDice2.Opacity = 0.5f;
            }
            else
            {
                _dice2Flag = false;
                imageDice2.Opacity = 1f;
            }
        }

        private void imageDice3_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (_dice3Flag == false)
            {
                _dice3Flag = true;
                imageDice3.Opacity = 0.5f;
            }
            else
            {
                _dice3Flag = false;
                imageDice3.Opacity = 1f;
            }
        }



        private void DiceRoll_Click(object sender, RoutedEventArgs e)
        {

        }
    }





}
