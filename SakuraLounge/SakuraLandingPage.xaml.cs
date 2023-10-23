using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SakuraLounge
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SakuraLandingPage : Page
    {
        public SakuraLandingPage()
        {
            this.InitializeComponent();
            int currentScore = ScoreManager.GetScore();
            ScoreTextBlock.Text = "Score: " + currentScore.ToString();

            #region Setup the device sizing for the application  
            ApplicationView.GetForCurrentView().TryResizeView(new Size(App.DeviceScreenWidth, App.DeviceScreenHeight));
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(App.DeviceMinimumScreenWidth, App.DeviceMinimumScreenHeight));
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            #endregion
        }
        private void OurStory_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(OurStory));
        }

        private void BlossomAddress_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(BlossomAddress));
        }

        private void DiceGarden_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(DiceGarden));
        }

        private void LuckyPetalsLotto_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LuckyPetalsLotto));
        }

        private void SakuraSips_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SakuraSips));
        }

        private void FortuneBreezes_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(FortuneBreezes));
        }

        private void GamesOfTheOrient_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(GamesOfTheOrient));
        }

        private void CulturalTreasures_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CulturalTreasures));
        }

        private void FestiveSeasons_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(FestiveSeasons));
        }
    }
}
