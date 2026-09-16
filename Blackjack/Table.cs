using System;

public class Table
{

    public CardDeck deck;
    public Gambler player;

    public Table()
	{
        deck = new CardDeck();
        player = new Gambler(deck);
    }

    public void StartRound()
    {
        deck.Shuffle();
        player.Hit(deck.DrawCard());
        player.StartTurn();
    }
}
