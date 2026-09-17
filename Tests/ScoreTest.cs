
namespace Tests;

[TestClass]
public class ScoreTest
{
    [TestMethod]
    [DataRow(1, 11, DisplayName = "Ace")]
    [DataRow(2, 2, DisplayName = "Two")]
    [DataRow(3, 3, DisplayName = "Three")]
    [DataRow(4, 4, DisplayName = "Four")]
    [DataRow(5, 5, DisplayName = "Five")]
    [DataRow(6, 6, DisplayName = "Six")]
    [DataRow(7, 7, DisplayName = "Seven")]
    [DataRow(8, 8, DisplayName = "Eight")]
    [DataRow(9, 9, DisplayName = "Nine")]
    [DataRow(10, 10, DisplayName = "Ten")]
    [DataRow(11, 10, DisplayName = "Jack")]
    [DataRow(12, 10, DisplayName = "Queen")]
    [DataRow(13, 10, DisplayName = "King")]
    public void ScoresTheCardsCorrectly(int cardRank, int expected)
    {
        Card card = new Card(cardRank);
        int score = 0;

        score.AddCardValue(card);
        
        Assert.AreEqual(expected, score);
    }

    [TestMethod]
    public void AceScoring_OneIfElevenWouldBust()
    {
        int expected = 12;
        int score = 11;
        Card ace = new Card(1);

        score.AddCardValue(ace);

        Assert.AreEqual(expected, score);
    }

    [TestMethod]
    [DataRow(21, 13, 1)]
    [DataRow(7, 2, 5)]
    [DataRow(18, 3, 5, 12)]
    [DataRow(59, 7, 9, 13, 11, 7, 3, 1, 2, 10)]
    public void SumsCardScoresTogether(int expected, params int[] cardRanks)
    {
        Card[] cardHand = new Card[cardRanks.Length];
        for (int i = 0; i < cardHand.Length; i++)
        {
            cardHand[i] = new Card(cardRanks[i]);
        }

        int result = Scorer.GetHandScore(cardHand);

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    [DataRow(new int[] {1, 2, 3}, new int[] {3, 2, 1})]
    [DataRow(new int[] {10, 5, 8, 9, 2, 5}, new int[] {10, 9, 8, 5, 5, 2})]
    public void SortCardsInDesendingOrder(int[] UnorderedcardRanks, int[] orderedCardRanks)
    {
        Card[] expected = new Card[orderedCardRanks.Length];
        for (int i = 0; i < expected.Length; i++)
        {
            expected[i] = new Card(orderedCardRanks[i]);
        }
        Card[] cards = new Card[UnorderedcardRanks.Length];
        for (int i = 0; i < cards.Length; i++)
        {
            cards[i] = new Card(UnorderedcardRanks[i]);
        }

        Scorer.SortCardHand(cards);

        CollectionAssert.AreEqual(expected, cards);
    }
}
