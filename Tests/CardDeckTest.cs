using System.Reflection;

namespace Tests;

[TestClass]
public class CardDeckTest
{

    [TestMethod]
    public void Constructor_GivesDeckOf52Cards()
    {
        int expected = 52;
        CardDeck deck = new CardDeck();

        Assert.AreEqual(expected, deck.Count);
    }
    
    [TestMethod]
    public void DrawCard_ReturnsCard()
    {
        Card expected = new Card(1);
        CardDeck deck = new CardDeck(new Stack<Card>([new Card(1), new Card(1), new Card(1)]));
        
        Card result = deck.DrawCard();

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void DrawCard_RemovesCardFromDeck()
    {
        int expected = 2;
        CardDeck deck = new CardDeck(new Stack<Card>([new Card(1), new Card(1), new Card(1)]));

        deck.DrawCard();

        Assert.AreEqual(expected, deck.Count);
    }

    [TestMethod]
    public void Shuffle_RandomisesCardOrder()
    {
        Stack<Card> original = new Stack<Card>([new Card(1), new Card(2), new Card(3), new Card(4), new Card(5), new Card(6), new Card(7), new Card(8), new Card(9), new Card(10), new Card(11), new Card(12), new Card(13)]);
        CardDeck deck = new CardDeck(original);

        deck.Shuffle();
        
        CollectionAssert.AreNotEqual(original, deck.cards);
    }

    [TestMethod]
    public void Shuffle_StillContainsTheSameCards()
    {
        CardDeck deck = new CardDeck();
        Stack<Card> original = new Stack<Card>(deck.cards);

        deck.Shuffle();

        CollectionAssert.AreEquivalent(original, deck.cards);
    }
}
