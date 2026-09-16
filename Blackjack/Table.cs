using System;

public class Table
{

    CardDeck deck;
    Gambler player;

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
