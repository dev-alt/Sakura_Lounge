using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using SakuraLounge.Classes;

namespace SakuraLounge
{
    public sealed partial class FortuneBreezes : Page
    {
        private GenerateFortune fortuneGenerator;
        private bool isAudioPaused = false;

        public FortuneBreezes()
        {
            this.InitializeComponent();
            fortuneGenerator = new GenerateFortune();
            fortuneGenerator.MediaElement = mediaElement; // Set the MediaElement property
        }

        private void GenerateFortune_Click(object sender, RoutedEventArgs e)
        {

            int pointsToDeduct = 5;
            int currentScore = ScoreManager.GetScore();
            if (currentScore >= pointsToDeduct)
            {

                ScoreManager.AddScore(-pointsToDeduct); // Subtract 50 points from the user's score

                // Generate the fortune and start playback
                fortuneGenerator.Generate(FortuneBox);
                isAudioPaused = false; // Reset the audio pause state
                FortuneDetailsPopup.IsOpen = true;
            }
            else
            {
                ShowNotEnoughPointsMessage();
            }
        }

        private async void ShowNotEnoughPointsMessage()
        {
            int currentScore = ScoreManager.GetScore(); // Retrieve the user's current score
            string message = $"You don't have enough points to play. You have {currentScore} points.";

            ContentDialog notEnoughPointsDialog = new ContentDialog
            {
                Title = "Not Enough Points",
                Content = message,
                CloseButtonText = "OK"
            };

            ContentDialogResult result = await notEnoughPointsDialog.ShowAsync();
        }

        private void ClosePopup_Click(object sender, RoutedEventArgs e)
        {
            FortuneDetailsPopup.IsOpen = false;
        }
        
        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Play();
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Pause();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Stop();
            isAudioPaused = false; // Reset the audio pause state
        }

        private void Slider_ValueChanged(object sender, Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            if (mediaElement != null)
            {
                // Update the volume of the MediaElement
                mediaElement.Volume = e.NewValue;
            }
        }

    }
}