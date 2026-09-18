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

}
