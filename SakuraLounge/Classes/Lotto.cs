using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Media;

namespace SakuraLounge.Classes
{
    internal class Lotto
    {
        private readonly List<int[]> _tickets; // List to store multiple tickets
        private readonly Random _randomNumber;
        private int totalWinnings; // Track total winnings
        public event Action<int> WinningsOccurred;
        public event Action<int> TotalWinningsUpdated; // Event for total winnings update


        public List<int[]> GetTickets()
        {
            return _tickets;
        }

        public Lotto()
        {
            _tickets = new List<int[]>();
            _randomNumber = new Random(DateTime.Now.Millisecond);
            totalWinnings = 0; 
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
                HashSet<int> usedNumbers = new HashSet<int>();

                for (int i = 0; i < ticketSize; i++)
                {
                    int randomNumber;
                    do
                    {
                        randomNumber = _randomNumber.Next(1, 51); // Generates a random number between 1 and 50
                    } while (usedNumbers.Contains(randomNumber));

                    ticket[i] = randomNumber;
                    usedNumbers.Add(randomNumber);
                }

                // Bubble sort for the ticket numbers
                for (int i = 0; i < ticket.Length - 1; i++)
                {
                    for (int j = 0; j < ticket.Length - 1 - i; j++)
                    {
                        if (ticket[j] > ticket[j + 1])
                        {
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
            int totalWinnings = 0;

            outputTextBlock.Inlines.Add(new Run { Text = "--------------------------" });
            outputTextBlock.Inlines.Add(new LineBreak());

            foreach (var ticket in _tickets)
            {
                int correctNumbers = ticket.Count(winningNumbers.Contains);
                int winnings = CalculateWinnings(correctNumbers);

                // Accumulate winnings to the total winnings for this draw
                totalWinnings += winnings;


                // Start the ticket with "--" to indicate the start
                outputTextBlock.Inlines.Add(new Run { Text = "-- " });

                for (int i = 0; i < ticket.Length; i++)
                {
                    string numberText = ticket[i] < 10 ? "0" + ticket[i].ToString() : ticket[i].ToString();

                    // Add leading space for single-digit numbers
                    if (ticket[i] < 10)
                    {
                        numberText = " " + numberText;
                    }

                    // Highlight winning numbers in yellow
                    if (winningNumbers.Contains(ticket[i]))
                    {
                        var run = new Run { Text = numberText };
                        run.Foreground = new SolidColorBrush(Colors.DarkBlue); 
                        var underlinedRun = new Underline();
                        underlinedRun.Inlines.Add(run);
                        outputTextBlock.Inlines.Add(underlinedRun);
                    }
                    else
                    {
                        // Add non-winning numbers in regular color
                        outputTextBlock.Inlines.Add(new Run { Text = numberText });
                    }

                    // Add comma and space except for the last number
                    if (i < ticket.Length - 1)
                    {
                        outputTextBlock.Inlines.Add(new Run { Text = " " });
                    }
                }

                // End the ticket with "--" to indicate the end
                outputTextBlock.Inlines.Add(new Run { Text = " --" });

                // Add line break between tickets
                outputTextBlock.Inlines.Add(new LineBreak());


                if (winnings > 0)
                {
                    WinningsOccurred?.Invoke(winnings);
                }
            }

            // Add a line of dashes after the entire ticket block
            outputTextBlock.Inlines.Add(new Run { Text = "---------------------------" });
            outputTextBlock.Inlines.Add(new LineBreak());

            // Notify of the total winnings for this draw
            TotalWinningsUpdated?.Invoke(totalWinnings);
        }

        public int GetTotalWinnings()
        {
            return totalWinnings;
        }

        public void ClearTotalWinnings()
        {
            totalWinnings = 0;
        }


        /// <summary>
        /// Calculates the winnings based on the number of correct numbers guessed.
        /// </summary>
        /// <param name="correctNumbers">The number of correct numbers guessed.</param>
        /// <returns>The amount of winnings.</returns>
        internal int CalculateWinnings(int correctNumbers)
        {
            int winnings;

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