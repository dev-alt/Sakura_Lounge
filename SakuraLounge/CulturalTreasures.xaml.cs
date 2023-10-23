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
    public sealed partial class CulturalTreasures : Page
    {
        private SlotMachine slotMachine;
 

        public CulturalTreasures()
        {
            this.InitializeComponent();
            slotMachine = new SlotMachine(PlayGameSlots, new Image[] { Slot_One, Slot_Two, Slot_Three }, textBlockDollars);
            slotMachine.ResultUpdated += SlotMachine_ResultUpdated; // Subscribe to the event
            slotMachine.Initialize(); 
        }
        private void SlotMachine_ResultUpdated(object sender, SlotMachine.ResultEventArgs e)
        {
            textBlockResult.Text = e.Message;
        }


        private void PlayGameSlots_Click(object sender, RoutedEventArgs e)
        {
            slotMachine.PlayGameSlots_Click(sender, e);
        }

        private void buttonAddCash_Click(object sender, RoutedEventArgs e)
        {
            slotMachine.buttonAddCash_Click(sender, e);
        }

        private void Slot_One_Tapped(object sender, TappedRoutedEventArgs e)
        {
            slotMachine.Slot_Tapped(sender, e);
        }

        private void Slot_Two_Tapped(object sender, TappedRoutedEventArgs e)
        {
            slotMachine.Slot_Tapped(sender, e);
        }

        private void Slot_Three_Tapped(object sender, TappedRoutedEventArgs e)
        {
            slotMachine.Slot_Tapped(sender, e);
        }

        private void CheatButton_Click(object sender, RoutedEventArgs e)
        {
            slotMachine.CheatButton_Click();
        }
        
    }
}