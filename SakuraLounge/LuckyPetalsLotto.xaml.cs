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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LuckyPetalsLotto : Page
    {
        public LuckyPetalsLotto()
        {
            this.InitializeComponent();
        }

        private void PlayGame_Click(object sender, RoutedEventArgs e)
        {
            Lotto row;
            row = new Lotto();

            TextBlockTicket.Text = "";

            TextBlockTicket.Text = TextBlockTicket.Text + "----------------\n";
            TextBlockTicket.Text = TextBlockTicket.Text + "--Lotto Ticket--\n";
            TextBlockTicket.Text = TextBlockTicket.Text + "----------------\n";

            TextBlockTicket.Text = TextBlockTicket.Text + "--";

            row.SetNumbersToZero();
            row.GenerateNumbers();
            row.PrintNumbers(TextBlockTicket);

            

        }
    }
}
