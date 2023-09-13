using Microsoft.VisualStudio.TestTools.UnitTesting;
using SakuraLounge.Classes;
using System.Linq;

namespace SakuraLounge.Tests
{
    [TestClass]
    public class LottoTests
    {
        [TestMethod]
        public void GenerateTicket_GeneratesTicketWithCorrectSize()
        {
            // Arrange
            var lotto = new Lotto();
            var ticketSize = 6;

            // Act
            lotto.GenerateTicket(ticketSize);

            // Assert
            Assert.AreEqual(ticketSize, lotto.GetTickets().First().Length);
        }

        [TestMethod]
        public void PrintTickets_PrintsTicketsWithCorrectWinningNumbersHighlighted()
        {
            // Arrange
            var lotto = new Lotto();
            var ticketSize = 6;
            var winningNumbers = new int[] { 1, 2, 3, 4, 5, 6 };
            lotto.GenerateTicket(ticketSize);
            var outputTextBlock = new Windows.UI.Xaml.Controls.TextBlock();

            // Act
            lotto.PrintTickets(outputTextBlock, winningNumbers);

            // Assert
            var expectedOutput = " 1  2  3  4  5  6";
            Assert.AreEqual(expectedOutput, outputTextBlock.Inlines.First().ToString());
            Assert.AreEqual(Colors.Yellow, ((Windows.UI.Xaml.Media.SolidColorBrush)((Windows.UI.Xaml.Documents.Run)outputTextBlock.Inlines.First()).Foreground).Color);
        }

        [TestMethod]
        public void CalculateWinnings_ReturnsCorrectWinningsFor3CorrectNumbers()
        {
            // Arrange
            var lotto = new Lotto();
            var correctNumbers = 3;

            // Act
            var winnings = lotto.CalculateWinnings(correctNumbers);

            // Assert
            Assert.AreEqual(1, winnings);
        }

        [TestMethod]
        public void CalculateWinnings_ReturnsCorrectWinningsFor4CorrectNumbers()
        {
            // Arrange
            var lotto = new Lotto();
            var correctNumbers = 4;

            // Act
            var winnings = lotto.CalculateWinnings(correctNumbers);

            // Assert
            Assert.AreEqual(5, winnings);
        }

        [TestMethod]
        public void CalculateWinnings_ReturnsCorrectWinningsFor5CorrectNumbers()
        {
            // Arrange
            var lotto = new Lotto();
            var correctNumbers = 5;

            // Act
            var winnings = lotto.CalculateWinnings(correctNumbers);

            // Assert
            Assert.AreEqual(150, winnings);
        }

        [TestMethod]
        public void CalculateWinnings_ReturnsCorrectWinningsFor6CorrectNumbers()
        {
            // Arrange
            var lotto = new Lotto();
            var correctNumbers = 6;

            // Act
            var winnings = lotto.CalculateWinnings(correctNumbers);

            // Assert
            Assert.AreEqual(300, winnings);
        }

        [TestMethod]
        public void CalculateWinnings_ReturnsZeroWinningsForLessThan3CorrectNumbers()
        {
            // Arrange
            var lotto = new Lotto();
            var correctNumbers = 2;

            // Act
            var winnings = lotto.CalculateWinnings(correctNumbers);

            // Assert
            Assert.AreEqual(0, winnings);
        }
    }
}