using System;
using System.Collections.Generic;
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

            _wheelImageUris[0] = new Uri("ms-appx:///Assets/coins.png");
            _wheelImageUris[1] = new Uri("ms-appx:///Assets/Cherry_blossom_Charm.png");
            _wheelImageUris[2] = new Uri("ms-appx:///Assets/Bonsai_Tree.png");
            _wheelImageUris[3] = new Uri("ms-appx:///Assets/Koi_Fish.png");
            _wheelImageUris[4] = new Uri("ms-appx:///Assets/Lucky_Lantern.png");
            _wheelImageUris[5] = new Uri("ms-appx:///Assets/dragon_slot_icon.png");
        }

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
            UpdateSlotImages();
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

            if (_wheels[0] == _wheels[1] && _wheels[1] == _wheels[2])
            {
                payout = 10;
                PlayJackpotSound();
            }

            if (payout > 0)
            {
                _dollars += payout;
                resultMessage = "You have gained $" + payout;
            }
            else
            {
                _dollars -= 2; 
                resultMessage = "You have lost";
            }

            _dollarsTextBlock.Text = "You have $" + _dollars;
            ResultUpdated?.Invoke(this, new ResultEventArgs { Message = resultMessage });
        }



        public void PlayGameSlots_Click(object sender, RoutedEventArgs e)
        {
            InitializeGameIfLoadedUp();

            if (_dollars <= 0)
            {
                _playGameSlots.Visibility = Visibility.Collapsed;
                _dollars = 0;
            }

            SpinWheels();
            CheckWinningsAndLosses();
            UpdateSlotOpacities();
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

        internal void buttonAddCash_Click(object sender, RoutedEventArgs e)
        {
            _dollars += 10;
            _dollarsTextBlock.Text = "You have $" + _dollars;
            _mediaPlayer.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/coins.wav"));
            _mediaPlayer.Play();

            if (_dollars > 0)
            {
                _playGameSlots.Visibility = Visibility.Visible;
            }
        }

    }
}
