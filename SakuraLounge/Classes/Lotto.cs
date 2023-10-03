using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Media;

namespace SakuraLounge.Classes
{
    internal class Lotto
    {
        private readonly List<int[]> _tickets; // List to store multiple tickets
        private readonly Random _randomNumber;
        public event Action<int> WinningsOccurred;
        public List<int[]> GetTickets()
        {
            return _tickets;
        }

        public Lotto()
        {
            _tickets = new List<int[]>();
            _randomNumber = new Random(DateTime.Now.Millisecond);
        }

        /// <summary>
        /// Generates a lottery ticket with the specified number of random numbers between 1 and 50, and adds it to the list of tickets.
        /// </summary>
        /// <param name="ticketSize">The size of the ticket to generate.</param>
        public void GenerateTicket(int ticketSize)
        {
            try
            {
                if (ticketSize <= 0)
                {
                    throw new ArgumentException("Ticket size must be greater than zero.");
                }

                int[] ticket = new int[ticketSize];

                for (int i = 0; i < ticketSize; i++)
                {
                    ticket[i] = _randomNumber.Next(1, 50);
                }

                // Bubble sort for the ticket numbers
                for (int i = 0; i < ticket.Length - 1; i++)
                {
                    for (int j = 0; j < ticket.Length - 1 - i; j++)
                    {
                        if (ticket[j] > ticket[j + 1])
                        {
                            // Swap ticket[j] and ticket[j + 1]
                            (ticket[j], ticket[j + 1]) = (ticket[j + 1], ticket[j]);
                        }
                    }
                }

                _tickets.Add(ticket);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Prints the tickets and highlights the winning numbers.
        /// </summary>
        /// <param name="outputTextBlock">The TextBlock where the tickets will be printed.</param>
        /// <param name="winningNumbers">The array of winning numbers.</param>
        public void PrintTickets(TextBlock outputTextBlock, int[] winningNumbers)
        {
            foreach (var ticket in _tickets)
            {
                int correctNumbers = ticket.Count(num => winningNumbers.Contains(num));

                int winnings = CalculateWinnings(correctNumbers);

                foreach (var number in ticket)
                {
                    var run = new Run();
                    run.Text = number.ToString();

                    if (number < 10)
                    {
                        run.Text = " " + run.Text;
                    }

                    // Check if the number is a winning number and set the foreground color accordingly
                    if (winningNumbers.Contains(number))
                    {
                        run.Foreground = new SolidColorBrush(Colors.Yellow); // Highlight as a winning number
                    }

                    outputTextBlock.Inlines.Add(run);

                    if (number != ticket.Last())
                    {
                        var spaceRun = new Run();
                        spaceRun.Text = "  ";
                        outputTextBlock.Inlines.Add(spaceRun);
                    }
                }

                if (winnings > 0)
                {
                    WinningsOccurred?.Invoke(winnings);
                }

                outputTextBlock.Inlines.Add(new LineBreak()); // Add a line break between tickets
            }
        }


        /// <summary>
        /// Calculates the winnings based on the number of correct numbers guessed.
        /// </summary>
        /// <param name="correctNumbers">The number of correct numbers guessed.</param>
        /// <returns>The amount of winnings.</returns>
        internal int CalculateWinnings(int correctNumbers)
        {
            int winnings = 0;

            switch (correctNumbers)
            {
                case 3:
                    winnings = 1; // $1 for 3 correct numbers
                    break;
                case 4:
                    winnings = 5; // $5 for 4 correct numbers
                    break;
                case 5:
                    winnings = 150; // $150 for 5 correct numbers
                    break;
                case 6:
                    winnings = 300; // $300 for all 6 correct numbers
                    break;
                default:
                    winnings = 0; // No winnings for less than 3 correct numbers
                    break;
            }

            return winnings;
        }

        public void ClearTickets()
        {
            _tickets.Clear();
        }
    }
}