using Moq;

namespace Tests;

[TestClass]
public class DealerTest
{
    [TestMethod]
    [DoNotParallelize]
    public void StartTurn_ContinuesToDrawTillConditionsForStandingMet()
    {
        int expectedDraws = 5;
        Dealer dealer = new Dealer();
        dealer.hand = new List<Card>([new Card(8)]);
        Mock<CardDeck> mock = new Mock<CardDeck>();
        mock.Setup(deck => deck.DrawCard()).Returns(new Card(2));
        Table.Current.deck = mock.Object;
        Table.Current.highestPlayerHandScore = 21;

        dealer.StartTurn();

        mock.Verify(deck => deck.DrawCard(), Times.AtLeast(expectedDraws));
    }

    [TestMethod]
    [DoNotParallelize]
    public void StartTurn_DoesntDrawMoreCardsThanNeeded()
    {
        int expectedDraws = 2;
        Dealer dealer = new Dealer();
        dealer.hand = new List<Card>([new Card(10), new Card(4)]);
        Mock<CardDeck> mock = new Mock<CardDeck>();
        mock.Setup(deck => deck.DrawCard()).Returns(new Card(2));
        Table.Current.deck = mock.Object;
        Table.Current.highestPlayerHandScore = 21;

        dealer.StartTurn();

        mock.Verify(deck => deck.DrawCard(), Times.AtMost(expectedDraws));
    }

    [TestMethod]
    public void MakeDecision_StandsOnBust()
    {
        bool expected = true;
        Dealer dealer = new Dealer();
        dealer.hand = new List<Card>([new Card(10), new Card(6), new Card(6)]);

        dealer.MakeDecision();

        Assert.AreEqual(expected, dealer.WillStand);
    }

    [TestMethod]
    public void MakeDecision_Has21PointHand_Stands()
    {
        bool expected = true;
        Dealer dealer = new Dealer();
        dealer.hand = new List<Card>([new Card(10), new Card(1)]);

        dealer.MakeDecision();

        Assert.AreEqual(expected, dealer.WillStand);
    }

    [TestMethod]
    public void MakeDecision_HasMorePointsThanHighestPlayerHand_Stands()
    {
        bool expected = true;
        Dealer dealer = new Dealer();
        dealer.hand = new List<Card>([new Card(10), new Card(3)]);
        Table.Current.highestPlayerHandScore = 12;

        dealer.MakeDecision();

        Assert.AreEqual(expected, dealer.WillStand);
    }

    [TestMethod]
    public void MakeDecision_HasHard17PlusHand_Stands()
    {
        bool expected = true;
        Dealer dealer = new Dealer();
        dealer.hand = new List<Card>([new Card(10), new Card(7)]);
        Table.Current.highestPlayerHandScore = 20;

        dealer.MakeDecision();

        Assert.AreEqual(expected, dealer.WillStand);
    }
    
    [TestMethod]
    [DoNotParallelize]
    public void MakeDecision_HasLessThan17Points_Hits()
    {
        Dealer dealer = new Dealer();
        dealer.hand = new List<Card>([new Card(10), new Card(6)]);
        Table.Current.highestPlayerHandScore = 20;
        Mock<CardDeck> mock = new Mock<CardDeck>();
        mock.Setup(deck => deck.DrawCard()).Returns(new Card(1));
        Table.Current.deck = mock.Object;

        dealer.MakeDecision();

        mock.Verify(deck => deck.DrawCard(), Times.AtLeastOnce());
    }

    [TestMethod]
    [DoNotParallelize]
    [DataRow(6)]
    [DataRow(7)]
    [DataRow(9)]
    public void MakeDecision_HasSoft17Plus_Hits(int otherCardRank)
    {
        Dealer dealer = new Dealer();
        dealer.hand = new List<Card>([new Card(1), new Card(otherCardRank)]);
        Table.Current.highestPlayerHandScore = 21;
        Mock<CardDeck> mock = new Mock<CardDeck>();
        mock.Setup(deck => deck.DrawCard()).Returns(new Card(1));
        Table.Current.deck = mock.Object;

        dealer.MakeDecision();

        mock.Verify(deck => deck.DrawCard(), Times.AtLeastOnce());
    }

}
