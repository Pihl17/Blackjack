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
    public Dealer dealer;
    public Gambler player;

    private Table()
	{
        deck = new CardDeck();
        dealer = new Dealer();
        player = new Gambler();
        player.OnTurnEnding += StartDealersTurn;
    }

    public void StartRound()
    {
        deck.Shuffle();
        DealStartHands();
        player.StartTurn();
    }

    void DealStartHands()
    {
        player.Hit(deck.DrawCard());
        dealer.Hit(deck.DrawCard());
        player.Hit(deck.DrawCard());
    }

    public void StartDealersTurn()
    {
        Console.WriteLine("\nDealer's turn:");
        dealer.StartTurn();
    }

}
