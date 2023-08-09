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
        private Random number;
        private Boolean wheel1Clicked = false;
        private Boolean wheel2Clicked = false;
        private Boolean wheel3Clicked = false;
        private MediaPlayer mediaPlayer;
        private int wheel1;
        private int wheel2;
        private int wheel3;
        private int dollars;
        private Boolean loadUp = true;
        private Button PlayGameSlots;
        private Image Slot_One;
        private Image Slot_Two;
        private Image Slot_Three;
        private Image imageWinLose;
        private TextBlock textBlockDollars;
        private TextBlock resultAnnounce;
        private Uri[] wheelImageUris = new Uri[6];


        public SlotMachine(Button playGameSlots, Image slotOne, Image slotTwo, Image slotThree,
            Image winLoseImage, TextBlock dollarsTextBlock, TextBlock textBlockResult)
        {
            PlayGameSlots = playGameSlots;
            Slot_One = slotOne;
            Slot_Two = slotTwo;
            Slot_Three = slotThree;
            imageWinLose = winLoseImage;
            textBlockDollars = dollarsTextBlock;
            resultAnnounce = textBlockResult;
            mediaPlayer = new MediaPlayer();

        }

        public void Initialize()
        {
            number = new Random(DateTime.Now.Millisecond);

            PlayGameSlots.Visibility = Visibility.Collapsed;

            Slot_One.Source =
                new BitmapImage(new Uri("ms-appx:///Assets/dragon_slot_icon.png", UriKind.RelativeOrAbsolute));
            Slot_Two.Source =
                new BitmapImage(new Uri("ms-appx:///Assets/dragon_slot_icon.png", UriKind.RelativeOrAbsolute));
            Slot_Three.Source =
                new BitmapImage(new Uri("ms-appx:///Assets/dragon_slot_icon.png", UriKind.RelativeOrAbsolute));
            imageWinLose.Source = new BitmapImage(new Uri("ms-appx:///Assets/coins.png", UriKind.RelativeOrAbsolute));
            wheelImageUris[0] = new Uri("ms-appx:///Assets/coins.png", UriKind.RelativeOrAbsolute);
            wheelImageUris[1] = new Uri("ms-appx:///Assets/Cherry_blossom_Charm.png", UriKind.RelativeOrAbsolute);
            wheelImageUris[2] = new Uri("ms-appx:///Assets/Bonsai_Tree.png", UriKind.RelativeOrAbsolute);
            wheelImageUris[3] = new Uri("ms-appx:///Assets/Koi_Fish.png", UriKind.RelativeOrAbsolute);
            wheelImageUris[4] = new Uri("ms-appx:///Assets/Lucky_Lantern.png", UriKind.RelativeOrAbsolute);
            wheelImageUris[5] = new Uri("ms-appx:///Assets/dragon_slot_icon.png", UriKind.RelativeOrAbsolute);
        }

        private void PlayJackpotSound()
        {
            mediaPlayer.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/slot_payoff.wav"));
            mediaPlayer.Volume = 0.1;
            mediaPlayer.Play();
        }

        public void CheckWinningsAndLoosing()
        {
            int payout = 0;

            switch (wheel1)
            {
                case 6 when wheel2 == 6 && wheel3 == 6:
                    payout = 60;
                    PlayJackpotSound();
                    resultAnnounce.Text = "You have gained $" + payout;
                    break;
                case 5 when wheel2 == 5 && wheel3 == 5:
                    payout = 50;
                    resultAnnounce.Text = "You have gained $" + payout;
                    break;
                case 4 when wheel2 == 4 && wheel3 == 4:
                    payout = 40;
                    resultAnnounce.Text = "You have gained $" + payout;
                    break;
                case 3 when wheel2 == 3 && wheel3 == 3:
                    payout = 30;
                    resultAnnounce.Text = "You have gained $" + payout;
                    break;
                case 2 when wheel2 == 2 && wheel3 == 2:
                    payout = 20;
                    resultAnnounce.Text = "You have gained $" + payout;
                    break;
                case 5 when wheel2 == 5 && wheel3 == 6:
                    payout = 10;
                    resultAnnounce.Text = "You have gained $" + payout;
                    break;
                case 4 when wheel2 == 4 && wheel3 == 6:
                    payout = 8;
                    resultAnnounce.Text = "You have gained $" + payout;
                    break;
                case 3 when wheel2 == 3 && wheel3 == 6:
                    payout = 6;
                    resultAnnounce.Text = "You have gained $" + payout;
                    break;
                case 2 when wheel2 == 2 && wheel3 == 6:
                    payout = 4;
                    resultAnnounce.Text = "You have gained $" + payout;
                    break;
            }

            if (payout > 0)
            {
                dollars += payout;
                imageWinLose.Source =
                    new BitmapImage(new Uri("ms-appx:///Assets/WinGame.png", UriKind.RelativeOrAbsolute));
            }

            // Handle losing scenarios
            else if (wheel1 == 1 || wheel2 == 1 || wheel3 == 1)
            {
                dollars -= 2;
                imageWinLose.Source =
                    new BitmapImage(new Uri("ms-appx:///Assets/LoseGame.png", UriKind.RelativeOrAbsolute));
                resultAnnounce.Text = "You have lost";
            }
            // Update displayed dollars
            textBlockDollars.Text = "You have $" + dollars;

        }

        internal void buttonAddCash_Click(object sender, RoutedEventArgs e)
        {
            // Add a certain amount of dollars when the button is clicked
            dollars += 10; // For example, adding $10
            textBlockDollars.Text = "You have $" + dollars; // Update the displayed dollars
            mediaPlayer.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/coins.wav"));
            mediaPlayer.Play();

            if (dollars > 0) PlayGameSlots.Visibility = Visibility.Visible;
        }

        internal void Slot_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (sender == Slot_One)
            {
                ToggleWheel1();
                Slot_One.Opacity = wheel1Clicked ? 0.5 : 1.0;
            }
            else if (sender == Slot_Two)
            {
                ToggleWheel2();
                Slot_Two.Opacity = wheel2Clicked ? 0.5 : 1.0;
            }
            else if (sender == Slot_Three)
            {
                ToggleWheel3();
                Slot_Three.Opacity = wheel3Clicked ? 0.5 : 1.0;
            }
        }

        internal void ToggleWheel1()
        {
            wheel1Clicked = !wheel1Clicked;
        }

        internal void ToggleWheel2()
        {
            wheel2Clicked = !wheel2Clicked;
        }

        internal void ToggleWheel3()
        {
            wheel3Clicked = !wheel3Clicked;
        }

        public void PlayGameSlots_Click(object sender, RoutedEventArgs routedEventArgs)
        {

            if (loadUp == true)
            {
                wheel1Clicked = false;
                wheel2Clicked = false;
                wheel3Clicked = false;
                loadUp = false;
            }


            if (dollars <= 0)
            {
                PlayGameSlots.Visibility = Visibility.Collapsed;
                dollars = 0;
            }

            //PlaySpinSound();
            SpinWheels(); // Example method to simulate spinning the wheels
            CheckWinningsAndLoosing(); // Example method to check the winnings and losses

            Slot_One.Opacity = wheel1Clicked ? 0.5 : 1.0;
            Slot_Two.Opacity = wheel2Clicked ? 0.5 : 1.0;
            Slot_Three.Opacity = wheel3Clicked ? 0.5 : 1.0;
        }

        private void PlaySpinSound()
        {
            mediaPlayer.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/slot_machine_insert_3_coins_and_spin.wav"));
            mediaPlayer.Play();
        }

        private void SpinWheels()
        {
            wheel1 = number.Next(1, 7);
            wheel2 = number.Next(1, 7);
            wheel3 = number.Next(1, 7);

            UpdateSlotImages();
        }

        private void UpdateSlotImages()
        {
            UpdateSlotImage(Slot_One, wheel1);
            UpdateSlotImage(Slot_Two, wheel2);
            UpdateSlotImage(Slot_Three, wheel3);
        }

        private void UpdateSlotImage(Image image, int wheelValue)
        {
            int imageIndex = wheelValue - 1;
            if (imageIndex >= 0 && imageIndex < wheelImageUris.Length)
            {
                image.Source = new BitmapImage(wheelImageUris[imageIndex]);
            }
        }
        public void CheatButton_Click()
        {
            wheel1 = 6;
            wheel2 = 6;
            wheel3 = 6;

            UpdateSlotImages();
            CheckWinningsAndLoosing();
        }
    }
}
