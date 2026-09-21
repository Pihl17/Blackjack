using Moq;

namespace Tests;

[TestClass]
public class GamblerTest
{
    [TestMethod]
    [DoNotParallelize]
    public void Turn_Hit_AutoEndsTurnOnBust()
    {   
        Mock<Input> mockInput = new Mock<Input>();
        mockInput.SetupSequence(i => i.ReadKey())
            .Returns(new ConsoleKeyInfo('h', ConsoleKey.H, false, false, false))
            .Throws(new InvalidOperationException());

        Gambler gambler = new Gambler(mockInput.Object);
        gambler.hand = new List<Card>([new Card(10), new Card(9)]);
        Mock<CardDeck> mockDeck = new Mock<CardDeck>();
        mockDeck.Setup(deck => deck.DrawCard()).Returns(new Card(10));
        Table.Current.deck = mockDeck.Object;

        gambler.StartTurn();

        mockInput.Verify(i => i.ReadKey(), Times.Once());
    }

    [TestMethod]
    [DoNotParallelize]
    public void Turn_Hit_DrawsCardWithoutAutoEndingTurn()
    {
        int expected = 3;
        Mock<Input> mockInput = new Mock<Input>();
        mockInput.SetupSequence(i => i.ReadKey())
            .Returns(new ConsoleKeyInfo('h', ConsoleKey.H, false, false, false))
            .Returns(new ConsoleKeyInfo('h', ConsoleKey.H, false, false, false))
            .Returns(new ConsoleKeyInfo('s', ConsoleKey.S, false, false, false))
            .Throws(new InvalidOperationException());

        Gambler gambler = new Gambler(mockInput.Object);
        gambler.hand = new List<Card>([new Card(2)]);
        Mock<CardDeck> mockDeck = new Mock<CardDeck>();
        mockDeck.Setup(deck => deck.DrawCard()).Returns(new Card(2));
        Table.Current.deck = mockDeck.Object;

        gambler.StartTurn();

        mockInput.Verify(i => i.ReadKey(), Times.Exactly(expected));
    }

    [TestMethod]
    [DataRow('s', ConsoleKey.S, DisplayName = "Stand")]
    [DataRow('h', ConsoleKey.H, DisplayName = "Hit")]
    [DataRow('c', ConsoleKey.C, DisplayName = "Look at hand")]
    public void Turn_CanRunAllInputsWithoutError(char character, ConsoleKey key)
    {
        int expected = 3;
        Mock<Input> mockInput = new Mock<Input>();
        mockInput.SetupSequence(i => i.ReadKey())
            .Returns(new ConsoleKeyInfo(character, key, false, false, false))
            .Returns(new ConsoleKeyInfo('s', ConsoleKey.S, false, false, false))
            .Throws(new InvalidOperationException());

        Gambler gambler = new Gambler(mockInput.Object);
        gambler.hand = new List<Card>([new Card(2)]);

        gambler.StartTurn();
    }

    [TestMethod]
    public void Turn_Stand_DoesntRepeatTurn()
    {
        Mock<Input> mockInput = new Mock<Input>();
        mockInput.SetupSequence(i => i.ReadKey())
            .Returns(new ConsoleKeyInfo('s', ConsoleKey.S, false, false, false))
            .Throws(new InvalidOperationException());
        Gambler gambler = new Gambler(mockInput.Object);

        gambler.StartTurn();

        mockInput.Verify(i => i.ReadKey(), Times.Once());
    }

    [TestMethod]
    [DataRow("89", 89)]
    public void PrintChipAmount_PrintsCorrectAmountOfChips(string expected, int amount)
    {
        Gambler gambler = new Gambler(amount, 0);

        StringWriter stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        gambler.PrintChipAmount();

        string result = stringWriter.ToString().Trim();
        Assert.Contains(expected, result);
    }

    [TestMethod]
    public void MakeBet_RemovesBetChipsFromChipPool()
    {
        int expected = 80;
        Mock<Input> mock = new Mock<Input>();
        mock.Setup(i => i.ReadLine()).Returns("20");
        Gambler gambler = new Gambler(mock.Object);

        gambler.MakeBet();

        Assert.AreEqual(expected, gambler.Chips);
    }

    [TestMethod]
    public void MakeBet_SetsBetAmount()
    {
        int expected = 15;
        Mock<Input> mock = new Mock<Input>();
        mock.Setup(i => i.ReadLine()).Returns("15");
        Gambler gambler = new Gambler(mock.Object);

        gambler.MakeBet();

        Assert.AreEqual(expected, gambler.Bet);
    }

    [TestMethod]
    public void MakeBet_MoreThanPlayerHasInPool_TakeNoMoreThanPlayerHas()
    {
        int expected = 0;
        Mock<Input> mock = new Mock<Input>();
        mock.Setup(i => i.ReadLine()).Returns("140");
        Gambler gambler = new Gambler(mock.Object);

        gambler.MakeBet();

        Assert.AreEqual(expected, gambler.Chips);
    }

    [TestMethod]
    public void MakeBet_MoreThanPlayerHasInPool_AddNoMoreToBetThanPlayerHad()
    {
        int expected = 100;
        Mock<Input> mock = new Mock<Input>();
        mock.Setup(i => i.ReadLine()).Returns("250");
        Gambler gambler = new Gambler(mock.Object);

        gambler.MakeBet();

        Assert.AreEqual(expected, gambler.Bet);
    }

    [TestMethod]
    [DoNotParallelize]
    [DataRow("ad", "Invalid input - Please input a valid number", DisplayName = "text input")]
    [DataRow("two", "Invalid input - Please input a valid number", DisplayName = "text number input")]
    [DataRow("t 20", "Invalid input - Please input a valid number", DisplayName = "text and number input")]
    [DataRow("-31", "cannot be negative", DisplayName = "Negative number")]
    [DataRow("0", "must be a non-zero value", DisplayName = "Zero")]
    [DataRow(null, "Invalid input - Please input a valid number", DisplayName = "Empty line")]
    public void MakeBet_InvalidInput_DeniedAndCausesRetry(string? invalidInput, string expectedUserMessage)
    {
        int expectedCalls = 2;
        string validInput = "5";
        Mock<Input> mock = new Mock<Input>();
        mock.SetupSequence(i => i.ReadLine())
            .Returns(invalidInput)
            .Returns(validInput)
            .Throws(new ArgumentException("The supposed valid input failed to be accepted by the method."));
        Gambler gambler = new Gambler(mock.Object);

        StringWriter stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        gambler.MakeBet();

        string consoleOutput = stringWriter.ToString().Trim();
        Assert.Contains(expectedUserMessage, consoleOutput);
        mock.Verify(i => i.ReadLine(), Times.Exactly(expectedCalls));
    }

    [TestMethod]
    [DataRow(10, 2f, 20)]
    public void WinBet_GivesMoreBetToChipPool(int betAmount, float multiplier, int expected)
    {
        Gambler gambler = new Gambler(0, betAmount);

        gambler.WinBet(multiplier);

        Assert.AreEqual(expected, gambler.Chips);
    }

    [TestMethod]
    public void WinBet_ResetsBetValue()
    {
        int expected = 0;
        Gambler gambler = new Gambler(100, 20);

        gambler.WinBet(1f);
        
        Assert.AreEqual(expected, gambler.Bet);
    }

    [TestMethod]
    public void LoseBet_RemovesChipsFromBet()
    {
        int expected = 0;
        Gambler gambler = new Gambler(100, 20);

        gambler.LoseBet();
        
        Assert.AreEqual(expected, gambler.Bet);
    }

    [TestMethod]
    public void ReturnChips_ReturnsBetToChipPool()
    {
        int expected = 100;
        Gambler gambler = new Gambler(80, 20);

        gambler.ReturnChips();

        Assert.AreEqual(expected, gambler.Chips);
    }

    [TestMethod]
    public void ReturnChips_ResetsBetValue()
    {
        int expected = 0;
        Gambler gambler = new Gambler(70, 30);

        gambler.ReturnChips();

        Assert.AreEqual(expected, gambler.Bet);
    }

}
