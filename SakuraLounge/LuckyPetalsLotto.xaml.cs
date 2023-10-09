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
using Windows.UI.Xaml.Navigation;
using SakuraLounge.Classes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238
namespace SakuraLounge
{

    public sealed partial class LuckyPetalsLotto : Page
    {

        private ContentDialog _winningsDialog; 

        public LuckyPetalsLotto()
        {
            this.InitializeComponent();
        }

        private void PlayGame_Click(object sender, RoutedEventArgs e)
        {
            TextBlockTicket.Text = "";

            if (RowsComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                int selectedRows = int.Parse(selectedItem.Content.ToString()); // Get the selected number of rows

                Lotto lotto = new Lotto();
                lotto.WinningsOccurred += DisplayWinningsPopup;
                lotto.ClearTickets();
                int[] winningNumbers = { 5, 10, 15, 20, 25, 30 };

                for (int i = 0; i < selectedRows; i++)
                {
                    lotto.GenerateTicket(6);
                }

                lotto.PrintTickets(TextBlockTicket, winningNumbers);
            }
            else
            {
                TextBlockTicket.Text = "Invalid number of rows. Please select a number.";
            }

        }



        // Display the winnings popup with the specified winnings amount
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
                Title = "Congratulations!",
                Content = $"You won ${winnings}.",
                PrimaryButtonText = "OK"
            };

            await _winningsDialog.ShowAsync();
        }


        private void RowsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
        
    }
}
