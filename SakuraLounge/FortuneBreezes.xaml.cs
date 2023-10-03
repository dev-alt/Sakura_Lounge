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
            // Generate the fortune and start playback
            fortuneGenerator.Generate(FortuneBox);
            isAudioPaused = false; // Reset the audio pause state
            FortuneDetailsPopup.IsOpen = true;
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
            //mediaElement.Stop();
            // Stop audio playback
            fortuneGenerator.StopPlayback();
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