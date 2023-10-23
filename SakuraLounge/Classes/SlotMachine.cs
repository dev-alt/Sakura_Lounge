using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;

namespace SakuraLounge.Classes
{
    internal class SlotMachine
    {
        private Random _random;
        private readonly bool[] _wheelClicked = new bool[3];
        private readonly MediaPlayer _mediaPlayer;
        private readonly int[] _wheels = new int[3];
        private int _dollars;
        private bool _loadedUp = true;
        private readonly Button _playGameSlots;
        private readonly Image[] _slotImages;
        private readonly TextBlock _dollarsTextBlock;
        private readonly Uri[] _wheelImageUris = new Uri[6];
        public event EventHandler<ResultEventArgs> ResultUpdated;


        public class ResultEventArgs : EventArgs
        {
            public string Message { get; set; }
        }

        public SlotMachine(Button playGameSlots, Image[] slotImages, TextBlock dollarsTextBlock)
        {
            _playGameSlots = playGameSlots;
            _slotImages = slotImages;
            _dollarsTextBlock = dollarsTextBlock;
            _mediaPlayer = new MediaPlayer();

        }

        public void Initialize()
        {
            _random = new Random(DateTime.Now.Millisecond);

            // Modify the initialization of _wheelImageUris
            _wheelImageUris[0] = new Uri("ms-appx:///Assets/coins.png"); // Symbol with a win of 10
            _wheelImageUris[1] = new Uri("ms-appx:///Assets/Cherry_blossom_Charm.png"); // Symbol with a win of 50
            _wheelImageUris[2] = new Uri("ms-appx:///Assets/Bonsai_Tree.png"); // Symbol with a win of 100
            _wheelImageUris[3] = new Uri("ms-appx:///Assets/Koi_Fish.png"); // Symbol with a win of 150
            _wheelImageUris[4] = new Uri("ms-appx:///Assets/Lucky_Lantern.png"); // Symbol with a win of 300
            _wheelImageUris[5] = new Uri("ms-appx:///Assets/dragon_slot_icon.png"); // Jackpot symbol with a win of 1000

        }

        private Dictionary<int[], int> symbolCombinations = new Dictionary<int[], int>
        {
            // Use indices to refer to the image URIs in _wheelImageUris
            { new int[] { 1, 1, 1 }, 5 }, // Three coins win 10
            { new int[] { 2, 2, 2 }, 10 }, // Three Cherry Blossom Charm symbols win 20
            { new int[] { 3, 3, 3 }, 20 }, // Three Bonsai Tree symbols win 30
            { new int[] { 4, 4, 4 }, 30 }, // Three Koi Fish symbols win 40
            { new int[] { 5, 5, 5 }, 40 }, // Three Lucky Lantern symbols win 50
            { new int[] { 6, 6, 6 }, 300 }, // Three Dragon symbols win 300
            { new int[] { 1, 1, 6 }, 1 }, // Two Coins and a Dragon win 15
            { new int[] { 1, 6, 1 }, 1 }, // Two Coins and a Dragon win 15
            { new int[] { 6, 1, 1 }, 1 }, // Two Coins and a Dragon win 15
            { new int[] { 2, 2, 6 }, 2 }, // Two Cherry Blossom Charm symbols and a Dragon win 25
            { new int[] { 6, 2, 2 }, 2 }, // Two Cherry Blossom Charm symbols and a Dragon win 25
            { new int[] { 2, 6, 2 }, 2 }, // Two Cherry Blossom Charm symbols and a Dragon win 25
            { new int[] { 3, 3, 6 }, 3 }, // Two Bonsai Tree symbols and a Dragon win 35
            { new int[] { 3, 6, 3 }, 3 }, // Two Bonsai Tree symbols and a Dragon win 35
            { new int[] { 6, 3, 3 }, 3 }, // Two Bonsai Tree symbols and a Dragon win 35
            { new int[] { 4, 4, 6 }, 4 }, // Two Koi Fish symbols and a Dragon win 45
            { new int[] { 4, 6, 4 }, 4 }, // Two Koi Fish symbols and a Dragon win 45
            { new int[] { 6, 4, 4 }, 4 }, // Two Koi Fish symbols and a Dragon win 45
            { new int[] { 5, 5, 6 }, 5 }, // Two Lucky Lantern symbols and a Dragon win 55
            { new int[] { 5, 6, 5 }, 5 }, // Two Lucky Lantern symbols and a Dragon win 55
            { new int[] { 6, 5, 5 }, 5 }, // Two Lucky Lantern symbols and a Dragon win 55
            { new int[] { 1, 6, 6 }, 5 }, // One Coin and two Dragons win 50
            { new int[] { 6, 1, 6 }, 5 }, // One Coin and two Dragons win 50
            { new int[] { 6, 6, 1 }, 5 }, // One Coin and two Dragons win 50
            { new int[] { 2, 6, 6 }, 6 }, // One Cherry Blossom Charm symbol and two Dragons win 60
            { new int[] { 6, 2, 6 }, 6 }, // One Cherry Blossom Charm symbol and two Dragons win 60
            { new int[] { 6, 6, 2 }, 6 }, // One Cherry Blossom Charm symbol and two Dragons win 60
            { new int[] { 3, 6, 6 }, 7 }, // One Bonsai Tree symbol and two Dragons win 70
            { new int[] { 6, 3, 6 }, 7 }, // One Bonsai Tree symbol and two Dragons win 70
            { new int[] { 6, 6, 3 }, 7 }, // One Bonsai Tree symbol and two Dragons win 70
            { new int[] { 4, 6, 6 }, 8 }, // One Koi Fish symbol and two Dragons win 80
            { new int[] { 6, 4, 6 }, 8 }, // One Koi Fish symbol and two Dragons win 80
            { new int[] { 6, 6, 4 }, 8 }, // One Koi Fish symbol and two Dragons win 80
            { new int[] { 5, 6, 6 }, 9 }, // One Lucky Lantern symbol and two Dragons win 90
            { new int[] { 6, 5, 6 }, 9 }, // One Lucky Lantern symbol and two Dragons win 90
            { new int[] { 6, 6, 5 }, 9 } // One Lucky Lantern symbol and two Dragons win 90
        };



        private void PlayJackpotSound()
        {
            _mediaPlayer.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/slot_payoff.wav"));
            _mediaPlayer.Volume = 0.1;
            _mediaPlayer.Play();
        }

        private void UpdateSlotImage(Image image, int wheelValue)
        {
            int imageIndex = wheelValue - 1;
            if (imageIndex >= 0 && imageIndex < _wheelImageUris.Length)
            {
                image.Source = new BitmapImage(_wheelImageUris[imageIndex]);
            }
        }

        private void PlaySpinSound()
        {
            _mediaPlayer.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/coins.wav"));
            _mediaPlayer.Play();
        }

        public void CheatButton_Click()
        {
            SetAllWheelsToSameValue(6);
            UpdateSlotImages();
            CheckWinningsAndLosses();
            LogSpinResult();
        }

        private void SetAllWheelsToSameValue(int value)
        {
            for (int i = 0; i < _wheels.Length; i++)
            {
                _wheels[i] = value;
            }
        }

        private void InitializeGameIfLoadedUp()
        {
            if (!_loadedUp) return;
            ResetWheelClicks();
            _loadedUp = false;
        }

        private void ResetWheelClicks()
        {
            for (int i = 0; i < _wheelClicked.Length; i++)
            {
                _wheelClicked[i] = false;
            }
        }

        private void SpinWheels()
        {
            for (int i = 0; i < _wheels.Length; i++)
            {
                _wheels[i] = _random.Next(1, 7);
            }
            Debug.WriteLine("SpinWheels result: " + string.Join(", ", _wheels));
            UpdateSlotImages();
            CheckWinningsAndLosses();
        }

        private void UpdateSlotImages()
        {
            for (int i = 0; i < _slotImages.Length; i++)
            {
                UpdateSlotImage(_slotImages[i], _wheels[i]);
            }
        }

        private void UpdateSlotOpacities()
        {
            _slotImages[0].Opacity = _wheelClicked[0] ? 0.5 : 1.0;
            _slotImages[1].Opacity = _wheelClicked[1] ? 0.5 : 1.0;
            _slotImages[2].Opacity = _wheelClicked[2] ? 0.5 : 1.0;
        }

        private void CheckWinningsAndLosses()
        {
            int payout = 0;
            string resultMessage = "";

            Debug.WriteLine("Checking _wheels:");
            Debug.WriteLine($"_wheels[0]: {_wheels[0]}, _wheels[1]: {_wheels[1]}, _wheels[2]: {_wheels[2]}");
            Debug.WriteLine("Checking combination.Key: " + string.Join(", ", _wheels));

            foreach (var combination in symbolCombinations)
            {
                Debug.WriteLine($"Checking combination.Key: {string.Join(", ", combination.Key)}");

                if (Enumerable.SequenceEqual(combination.Key, _wheels))
                {
                    payout = combination.Value;
                    Debug.WriteLine("Matched combination: " + string.Join(", ", combination.Key));
                    break;
                }
            }

            if (payout > 0)
            {
                ScoreManager.AddScore(payout);
                resultMessage = "You have gained $" + payout;
                PlayJackpotSound();
            }
            else
            {
                _dollars -= 2;
                resultMessage = "You have lost";
            }

            _dollarsTextBlock.Text = "You have $" + ScoreManager.GetScore();
            ResultUpdated?.Invoke(this, new ResultEventArgs { Message = resultMessage });
        }





        public void PlayGameSlots_Click(object sender, RoutedEventArgs e)
        {
            InitializeGameIfLoadedUp();

            int gameCost = 2;

            if (ScoreManager.GetScore() >= gameCost)
            {
                ScoreManager.AddScore(-gameCost);
                CheckWinningsAndLosses();
                _dollarsTextBlock.Text = "You have $" + ScoreManager.GetScore(); // Update the score text
            }

            if (ScoreManager.GetScore() >= gameCost)
            {
                SpinWheels();
                UpdateSlotOpacities();
            }

            if (ScoreManager.GetScore() <= 0)
            {
                _playGameSlots.Visibility = Visibility.Collapsed;
            }
        }

        internal void ToggleWheel1()
        {
            _wheelClicked[0] = !_wheelClicked[0];
        }

        internal void ToggleWheel2()
        {
            _wheelClicked[1] = !_wheelClicked[1];
        }

        internal void ToggleWheel3()
        {
            _wheelClicked[2] = !_wheelClicked[2];
        }

        internal void Slot_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (sender == _slotImages[0])
            {
                ToggleWheel1();
                _slotImages[0].Opacity = _wheelClicked[0] ? 0.5 : 1.0;
            }
            else if (sender == _slotImages[1])
            {
                ToggleWheel2();
                _slotImages[1].Opacity = _wheelClicked[1] ? 0.5 : 1.0;
            }
            else if (sender == _slotImages[2])
            {
                ToggleWheel3();
                _slotImages[2].Opacity = _wheelClicked[2] ? 0.5 : 1.0;
            }
        }
        private void LogSpinResult()
        {
            string spinResult = string.Join(", ", _wheels); // Combine wheel values into a string
            Debug.WriteLine("Spin result: " + spinResult);
        }

        internal void buttonAddCash_Click(object sender, RoutedEventArgs e)
        {
            int cashToAdd = 10;
            ScoreManager.AddScore(cashToAdd);
            _dollarsTextBlock.Text = "You have $" + ScoreManager.GetScore();
            _mediaPlayer.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/coins.wav"));
            _mediaPlayer.Play();

            if (_dollars > 0)
            {
                _playGameSlots.Visibility = Visibility.Visible;
            }
        }

    }
}
