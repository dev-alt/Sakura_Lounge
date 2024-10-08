using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Media.SpeechSynthesis;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;

namespace SakuraLounge.Classes
{
     internal class GenerateFortune
    {
        private readonly Random random = new Random();
        private readonly SpeechSynthesizer synth = new SpeechSynthesizer();

        public MediaElement MediaElement { get; set; } // Property to store the MediaElement
        private double previousVolume = 1.0; // Initialize with the default maximum volume

        readonly string[] timeArray = {
            "thirty minutes",
            "an hour",
            "eight hours",
            "twelve hours",
            "a day",
            "a week",
            "a month",
            "a year",
            "a decade"
        };

        readonly string[] aspectArray = {
            "finances",
            "love life",
            "career prospects",
            "travel plans",
            "relationships"
        };

        readonly string[] effectArray = {
            "fall apart",
            "exceed your expectation",
            "become awkward in an unexpected way",
            "become manageable",
            "become spectacular",
            "come to a positive outcome"
        };

        readonly string[] personaArray = {
            "man",
            "boy",
            "woman",
            "girl",
            "dog",
            "bird",
            "hedgehog",
            "singer",
            "relative"
        };

        readonly string[] featureArray = {
            "pink hair",
            "a broken golden chain",
            "scary eyes",
            "long blond nose hair",
            "very red lips",
            "silver feet"
        };

        readonly string[] consequenceArray = {
            "avoid looking at directly",
            "sing a sad song with",
            "stop and talk to",
            "dance with",
            "tell a secret",
            "buy a coffee"
        };

        public void Generate(TextBlock fortuneBox)
        {
            string generatedFortune = GenerateFortuneMessage();
            fortuneBox.Text = generatedFortune;
            ReadFortunePrediction(generatedFortune);
        }

        public string GenerateFortuneMessage()
        {
            Debug.WriteLine("Test");
            // Generate random indices for each array
            int timeIndex = random.Next(0, timeArray.Length);
            int aspectIndex = random.Next(0, aspectArray.Length);
            int effectIndex = random.Next(0, effectArray.Length);
            int personaIndex = random.Next(0, personaArray.Length);
            int featureIndex = random.Next(0, featureArray.Length);
            int consequenceIndex = random.Next(0, consequenceArray.Length);

            // Build the fortune message
            string fortuneMessage = $"Over a period of {timeArray[timeIndex]}, your {aspectArray[aspectIndex]} will {effectArray[effectIndex]}. " +
                                    $"This will come to pass after you meet a {personaArray[personaIndex]} with {featureArray[featureIndex]} who, for some reason, you find yourself obliged to {consequenceArray[consequenceIndex]}.";

            return fortuneMessage;
        }

        public async void ReadFortunePrediction(string prediction)
        {
            if (MediaElement != null)
            {
                // Hook up an event handler to stop playback when it's completed
                MediaElement.MediaEnded += (sender, args) =>
                {
                    // End the MediaElement when playback ends
                    ((MediaElement)sender).Stop();
                    ((MediaElement)sender).MediaEnded -= null; // Remove the event handler
                };

                // Synthesize the speech and play it
                var stream = await synth.SynthesizeTextToStreamAsync(prediction);
                MediaElement.SetSource(stream, stream.ContentType);
                MediaElement.Play();
            }
        }



    }
}