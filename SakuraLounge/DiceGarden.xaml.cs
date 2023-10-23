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
using SakuraLounge.Classes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SakuraLounge
{
    public sealed partial class DiceGarden : Page
    {
        private DiceGame diceGame;
        private Dragon dragon;
        private Player player;
        private int playerAttacks = 0; // Track the number of player attacks
        public DiceGarden()
        {
            this.InitializeComponent();
            dragon = new Dragon(100, 20, 15, 5);
            player = new Player(3, 5, 6);

            diceGame = new DiceGame();
            UpdateDiceImages();
        }



        private void DiceRoll_Click(object sender, RoutedEventArgs e)
        {
            int pointsToDeduct = 10; 
            int currentScore = ScoreManager.GetScore();

            if (currentScore >= pointsToDeduct)
            {

                if (playerAttacks < 3)
                {
                    diceGame.RollDice();
                    UpdateDiceImages();
                    playerAttacks++;

                    // Player's turn
                    int damageTotal = 0;
                    int strIncrease = 0;
                    int powerIncrease = 0;
                    int luckIncrease = 0;

                    for (int i = 0; i < 3; i++)
                    {
                        int playerAttack = player.Strength + diceGame.Roll1;

                        int dragonAttack = dragon.Strength; // Dragon's attack power

                        // Calculate combat outcomes for each die
                        int playerDamage = Math.Max(playerAttack - dragon.Luck, 0);
                        damageTotal += playerDamage;

                        // Modify player attributes based on the dice rolls
                        if (i == 0)
                        {
                            strIncrease = diceGame.Roll1;
                        }
                        else if (i == 1)
                        {
                            powerIncrease = diceGame.Roll2;
                        }
                        else if (i == 2)
                        {
                            luckIncrease = diceGame.Roll3;
                        }
                    }

                    // Update player's attributes
                    player.Strength += strIncrease;
                    player.Power += powerIncrease;
                    player.Luck += luckIncrease;

                    // Update dragon's health
                    dragon.Health -= damageTotal;
                    damageDisplay.Text += $"Hit {playerAttacks}: Damage: {damageTotal}\n";
                    CheckDragonStatus();
                }
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

        private void CheckDragonStatus()
        {
            if (dragon.Health <= 0)
            {
                // Dragon defeated
                ShowVictoryDialog();
            }
            else if (playerAttacks == 3)
            {
                // Dragon walks away
                ShowEscapeDialog();
            }
        }
        private async void ShowVictoryDialog()
        {
            OverlayGrid.Visibility = Visibility.Visible; // Show the overlay
            ContentDialogResult result = await VictoryDialog.ShowAsync();
            OverlayGrid.Visibility = Visibility.Collapsed; // Hide the overlay
        }

        private async void ShowEscapeDialog()
        {
            OverlayGrid.Visibility = Visibility.Visible; // Show the overlay
            ContentDialogResult result = await EscapeDialog.ShowAsync();
            OverlayGrid.Visibility = Visibility.Collapsed; // Hide the overlay
        }

        private void UpdateDiceImages()
        {
            UpdateImage(imageDice1, diceGame.Roll1);
            UpdateImage(imageDice2, diceGame.Roll2);
            UpdateImage(imageDice3, diceGame.Roll3);
        }

        private void UpdateImage(Image image, int roll)
        {
            string imagePath = $"ms-appx:///Assets/dice_{roll}.png";
            image.Source = new BitmapImage(new Uri(imagePath, UriKind.Absolute));
        }



        private void CloseVictoryDialog(object sender, RoutedEventArgs e)
        {
            VictoryDialog.Hide();
        }

        private void CloseEscapeDialog(object sender, RoutedEventArgs e)
        {
            EscapeDialog.Hide();
        }
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            // Reset the game
            dragon.Health = 100; // Set the dragon's initial health
            player.Strength = 10; // Set the player's initial strength
            player.Power = 20;   // Set the player's initial power
            player.Luck = 12;    // Set the player's initial luck
            playerAttacks = 0;   // Reset the number of player attacks

            // Clear any damage display text
            damageDisplay.Text = string.Empty;

            // Update the UI to reflect the reset values
            dragonHealthBar.Value = dragon.Health;
            UpdateDiceImages();
        }

    }
}

