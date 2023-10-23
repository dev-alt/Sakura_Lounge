using SakuraLounge.Classes;
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
using static System.Net.Mime.MediaTypeNames;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SakuraLounge
{
    public sealed partial class SakuraSips : Page
    {
        public SakuraSips()
        {
            this.InitializeComponent();
        }

        private void ShowDrinkDetailsPopup(Drink drink)
        {
            PopupDrinkName.Text = drink.Name;
            PopupDrinkRecipe.Text = "Recipe: " + drink.Recipe;
            PopupDrinkMix.Text = "Mix: " + drink.Mix;
            PopupDrinkPrice.Text = "Price: $" + drink.Price;

            DrinkDetailsPopup.IsOpen = true;
        }

        private void Drink1_Click(object sender, RoutedEventArgs e)
        {
            var drink = Drink.DrinkDictionary["Matcha Zen Latte"];
            ShowDrinkDetailsPopup(drink);
        }

        private void Drink2_Click(object sender, RoutedEventArgs e)
        {
            var drink = Drink.DrinkDictionary["Dragon's Fire Sake"];
            ShowDrinkDetailsPopup(drink);
        }

        private void Drink3_Click(object sender, RoutedEventArgs e)
        {
            var drink = Drink.DrinkDictionary["Bamboo Forest Mojito"];
            ShowDrinkDetailsPopup(drink);
        }

        private void Drink4_Click(object sender, RoutedEventArgs e)
        {
            var drink = Drink.DrinkDictionary["Fortune Teller's Fizz"];
            ShowDrinkDetailsPopup(drink);
        }

        private void Drink5_Click(object sender, RoutedEventArgs e)
        {
            var drink = Drink.DrinkDictionary["Sakura Sparkler"];
            ShowDrinkDetailsPopup(drink);
        }

        private void Drink6_Click(object sender, RoutedEventArgs e)
        {
            var drink = Drink.DrinkDictionary["Lucky Lotus Martini"];
            ShowDrinkDetailsPopup(drink);
        }

        private void ClosePopup_Click(object sender, RoutedEventArgs e)
        {
            DrinkDetailsPopup.IsOpen = false;
        }
        private async void ShowNotEnoughPointsMessage()
        {
            int currentScore = ScoreManager.GetScore(); 
            string message = $"You don't have enough Money. You have ${currentScore}";

            ContentDialog notEnoughPointsDialog = new ContentDialog
            {
                Title = "Not Enough money",
                Content = message,
                CloseButtonText = "OK"
            };

            ContentDialogResult result = await notEnoughPointsDialog.ShowAsync();
        }

        private async void Buy_Click(object sender, RoutedEventArgs e)
        {
            int drinkPrice = 10; // Change this to the actual price of the drink.
            int currentScore = ScoreManager.GetScore();

            if (currentScore >= drinkPrice)
            {
                // Deduct the price of the drink from the user's score.
                ScoreManager.AddScore(-drinkPrice);
                ContentDialog thankYouDialog = new ContentDialog
                {
                    Title = "Purchase Successful",
                    Content = "Thank you for your purchase!",
                    CloseButtonText = "OK"
                };

                await thankYouDialog.ShowAsync();
                DrinkDetailsPopup.IsOpen = false;

            }
            else
            {
                // Handle the case where the user doesn't have enough money to buy the drink.
                ShowNotEnoughPointsMessage();
            }
        }

    }
}