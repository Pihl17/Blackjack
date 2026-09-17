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

}
