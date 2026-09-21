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
    [DataRow(-1, 21, 10, 8, DisplayName = "Player hand loses")]
    [DataRow(1, 17, 10, 10, DisplayName = "Player hand wins")]
    [DataRow(0, 20, 10, 10, DisplayName = "Standoff")]
    [DataRow(-1, 21, 10, 10, 10, DisplayName = "Player hand loses due to bust")]
    public void CompareHands_DetermineWinningHandsAndReturnsIfPlayerWins(int expected, int dealerScore, params int[] playerCardRanks)
    {
        Card[] playerHand = new Card[playerCardRanks.Length];
        for (int i = 0; i < playerHand.Length; i++)
            playerHand[i] = new Card(playerCardRanks[i]);

        int result = Table.Current.CompareHands(dealerScore, playerHand);

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    [DoNotParallelize]
    [DataRow("You won!", new int[] { 10, 7 }, new int[] { 1, 10 }, DisplayName = "Player winning")]
    [DataRow("You lost", new int[] { 1, 10 }, new int[] { 10, 6, 3 }, DisplayName = "Player losing")]
    [DataRow("It\'s a standoff", new int[] { 10, 8 }, new int[] { 10, 8 }, DisplayName = "Standoff")]
    public void EndRound_AnnouncesResults(string expected, int[] dealerCardRanks, int[] playerCardRanks)
    {
        Card[] dealerHand = new Card[dealerCardRanks.Length];
        for (int i = 0; i < dealerHand.Length; i++)
            dealerHand[i] = new Card(dealerCardRanks[i]);
        Dealer dealer = new Dealer();
        dealer.hand = dealerHand.ToList();
        Table.Current.dealer = dealer;
        Card[] playerHand = new Card[playerCardRanks.Length];
        for (int i = 0; i < playerHand.Length; i++)
            playerHand[i] = new Card(playerCardRanks[i]);
        Gambler player = new Gambler();
        player.hand = playerHand.ToList();
        Table.Current.player = player;

        StringWriter stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        Table.Current.EndRound();

        string result = stringWriter.ToString().Trim();
        Assert.Contains(expected, result);
    }

}
