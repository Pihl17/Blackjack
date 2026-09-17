using Moq;

namespace Tests;

[TestClass]
public class DealerTest
{

    [TestMethod]
    public void Turn_HitsUntillHitting17Plus()
    {
        int expectedDraws = 4;
        int playerScore = 21;
        Dealer dealer = new Dealer();
        dealer.hand = new List<Card>() { new Card(4) };
        Mock<CardDeck> mockDeck = new Mock<CardDeck>();
        mockDeck.SetupSequence(deck => deck.DrawCard())
            .Returns(new Card(2))
            .Returns(new Card(3))
            .Returns(new Card(6))
            .Returns(new Card(4))
            .Returns(new Card(1));
        Table.Current.deck = mockDeck.Object;

        dealer.Turn(playerScore);

        mockDeck.Verify(deck => deck.DrawCard(), Times.Exactly(expectedDraws));
    }
    
    [TestMethod]
    [DataRow(7, true)]
    [DataRow(16, true)]
    [DataRow(17, false)]
    [DataRow(18, false)]
    [DataRow(21, false)]
    public void ShouldHit_HitsWhenHavingLessThan17InScore(int dealerScore, bool expected)
    {
        int playerScore = 21;
        Dealer dealer = new Dealer();

        bool result = dealer.ShouldHit(dealerScore, playerScore);

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void ShouldHit_HitsWhenHavingASoft17Plus()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void ShouldHit_StandWith21()
    {
        Assert.Fail();
    }

    [TestMethod]
    [DataRow(12, 9, false)]
    [DataRow(15, 3, false)]
    [DataRow(10, 10, true)]
    [DataRow(3, 16, true)]
    [DataRow(15, 16, true)]
    public void ShouldHit_StandWithScoreHigherThanPlayer(int dealerScore, int playerScore, bool expected)
    {
        Dealer dealer = new Dealer();

        bool result = dealer.ShouldHit(dealerScore, playerScore);

        Assert.AreEqual(expected, result);
    }



}
