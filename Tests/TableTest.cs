using Moq;

namespace Tests;

[TestClass]
public class TableTest
{

    [TestMethod]
    public void GetCurrent_InsantiatesAndReturnsSingleton()
    {
        var result = Table.Current;
        Assert.IsInstanceOfType<Table>(result);
    }
    
    [TestMethod]
    public void StartRound_DealsOutStartHands()
    {
        int expectedPlayerHand = 2;
        int expectedDealerHand = 1;

        Mock<Gambler> mockPlayer = new Mock<Gambler>();
        mockPlayer.Setup(player => player.StartTurn());

        Table.Current.player = mockPlayer.Object;


        Table.Current.StartRound();

        Assert.HasCount(expectedPlayerHand, Table.Current.player.hand, "Player hand count assertion failed");
        Assert.HasCount(expectedDealerHand, Table.Current.dealer.hand, "Dealer hand count assertion failed");
    }

    [TestMethod]
    [DoNotParallelize]
    public void StartDealersTurn_SetsHighestPlayerHandScore()
    {
        int expected = 21;
        Gambler player = new Gambler();
        player.hand = [new Card(1), new Card(13)];
        Table.Current.player = player;

        Table.Current.StartDealersTurn();

        Assert.AreEqual(expected, Table.Current.highestPlayerHandScore);
    }

    [TestMethod]
    public void EndRound_DetermineWinningHands()
    {
        Assert.Fail();
    }

}
