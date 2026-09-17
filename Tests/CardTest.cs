namespace Tests;

[TestClass]
public class CardTest
{
    [TestMethod]
    [DataRow(1, CardType.Ace, DisplayName = "Ace")]
    [DataRow(2, CardType.Numbered, DisplayName = "Two")]
    [DataRow(3, CardType.Numbered, DisplayName = "Three")]
    [DataRow(4, CardType.Numbered, DisplayName = "Four")]
    [DataRow(5, CardType.Numbered, DisplayName = "Five")]
    [DataRow(6, CardType.Numbered, DisplayName = "Six")]
    [DataRow(7, CardType.Numbered, DisplayName = "Seven")]
    [DataRow(8, CardType.Numbered, DisplayName = "Eight")]
    [DataRow(9, CardType.Numbered, DisplayName = "Nine")]
    [DataRow(10,CardType.Numbered, DisplayName = "Ten")]
    [DataRow(11,CardType.Face, DisplayName = "Jack")]
    [DataRow(12,CardType.Face, DisplayName = "Queen")]
    [DataRow(13,CardType.Face, DisplayName = "King")]
    public void CardTypesCorrespondsToRank(int cardRank, CardType expected)
    {
        Card card = new Card(cardRank);
        Assert.AreEqual(expected, card.type);
    }
}
