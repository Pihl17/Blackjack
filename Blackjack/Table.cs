using System;

public class Table
{
    
    private static Table? current;
    public static Table Current
    {
        get
        {
            if (current == null)
                current = new Table();
            return current;
        }
    }
    
    public CardDeck deck;
    public Gambler player;

    private Table()
	{
        deck = new CardDeck();
        player = new Gambler();
    }

    public void StartRound()
    {
        deck.Shuffle();
        player.Hit(deck.DrawCard());
        player.StartTurn();
    }
}
