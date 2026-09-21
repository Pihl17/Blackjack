using System;

public enum CardType { Numbered, Face, Ace }
public struct Card
{
    public int rank;
    public Card(int rank)
    {
        this.rank = rank;
    }
	public CardType type
	{
		get
		{
			if (rank == 1)
				return CardType.Ace;
			if (rank >= 11)
				return CardType.Face;
			return CardType.Numbered;
		}
	}
	public string RankName { get
		{
			switch (rank)
			{
				case 1:
					return "A";
				case 11:
					return "J";
				case 12:
					return "Q";
				case 13:
					return "K";
				default:
					return rank.ToString();
			}
		} 
	}

}

public class CardDeck
{

	public Stack<Card> cards = new Stack<Card>(52);

	public int Count { get { return cards.Count; } }
	
	public CardDeck()
	{
		InstantiateDeck();
	}

	public CardDeck(Stack<Card> cards)
	{
		this.cards = cards;
	}

	void InstantiateDeck()
	{
		for (int i = 1; i <= 13; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				cards.Push(new Card(i));
			}
		}
    }

	public void Shuffle()
	{
		cards = new Stack<Card>(cards.Shuffle());
	}

	public virtual Card DrawCard()
	{
		return cards.Pop();
	}


}
