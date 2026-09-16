using System;

public class Table
{

    CardDeck deck;
    Player player;

    public Table()
	{
        deck = new CardDeck();
        player = new Player(deck);
    }

    public void StartRound()
    {
        deck.Shuffle();
        player.Hit(deck.DrawCard());
        player.StartTurn();
    }
}
