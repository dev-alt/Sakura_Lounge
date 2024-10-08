﻿using System;
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
using Windows.UI.Xaml.Navigation;
using SakuraLounge.Classes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238
namespace SakuraLounge
{
    public sealed partial class LuckyPetalsLotto : Page
    {
        private ContentDialog _winningsDialog;
        private int costPerLine = 1;

        public LuckyPetalsLotto()
        {
            this.InitializeComponent();
        }

        // Click event handler for the "Play Game" button
        private void PlayGame_Click(object sender, RoutedEventArgs e)
        {
            TextBlockTicket.Text = "";

            if (RowsComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                if (selectedItem.Content != null)
                {
                    int selectedRows = int.Parse(selectedItem.Content.ToString());


                    int totalCost = selectedRows * costPerLine;
                    int currentScore = ScoreManager.GetScore();

                    if (currentScore >= totalCost)
                    {
                        ScoreManager.AddScore(-totalCost);
                        Lotto lotto = new Lotto(); 

                        // Register the event handler for total winnings update
                        lotto.TotalWinningsUpdated += DisplayWinningsPopup;
                        // Clear any existing tickets
                        lotto.ClearTickets();
                        int[] winningNumbers = { 5, 10, 15, 20, 25, 30 };

                        for (int i = 0; i < selectedRows; i++)
                        {
                            lotto.GenerateTicket(6);
                        }
                        // Print the lottery tickets
                        lotto.PrintTickets(TextBlockTicket, winningNumbers);

                    }
                    else
                    {
                        ShowNotEnoughPointsMessage();
                    }
                }
            }
            else
            {
                TextBlockTicket.Text = "Invalid number of rows. Please select a number.";
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


        // Display the winnings popup with the winning amount
        private async void DisplayWinningsPopup(int winnings)
        {
            // Close the existing dialog if it's open
            if (_winningsDialog != null)
            {
                _winningsDialog.Hide();
                _winningsDialog = null;
            }

            _winningsDialog = new ContentDialog
            {
                PrimaryButtonText = "OK"
            };

            if (winnings > 0)
            {
                _winningsDialog.Title = "Congratulations!";
                _winningsDialog.Content = $"You won ${winnings}.";
            }
            else
            {
                _winningsDialog.Title = "Sorry!";
                _winningsDialog.Content = "You have lost.";
            }

            await _winningsDialog.ShowAsync();
        }
    }
}
