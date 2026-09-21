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

    public int highestPlayerHandScore;

    private Table()
	{
        deck = new CardDeck();
        dealer = new Dealer();
        player = new Gambler();
        player.OnTurnEnding += StartDealersTurn;
        dealer.OnTurnEnding += EndRound;
    }

    public void StartRound()
    {
        Console.WriteLine("-----------");
        Console.WriteLine("Round Start");
        Console.WriteLine("-----------\n");

        deck.Shuffle();
        DealStartHands();
        player.StartTurn();
    }

    void DealStartHands()
    {
        Console.WriteLine("Dealing out start hands:");
        player.Hit(deck.DrawCard());
        dealer.Hit(deck.DrawCard());
        player.Hit(deck.DrawCard());
        Console.WriteLine();
        player.PrintHand();
        dealer.PrintHand();
    }

    public void StartDealersTurn()
    {
        highestPlayerHandScore = Scorer.GetHandScore(player.hand.ToArray());
        Console.WriteLine("\n--------------");
        Console.WriteLine("Dealer's turn:");
        dealer.StartTurn();
    }

    public void EndRound()
    {
        int dealerScore = Scorer.GetHandScore(dealer.hand.ToArray());
        int outcome = CompareHands(dealerScore, player.hand.ToArray());

        Console.WriteLine();
        switch (outcome)
        {
            case 1:
                Console.WriteLine("You won!");
                break;
            case -1:
                Console.WriteLine("You lost...");
                break;
            case 0:
                Console.WriteLine("It\'s a standoff");
                break;
        }
        
        Console.WriteLine();
        Console.WriteLine("---------------");
        Console.WriteLine("Round has Ended");
        Console.WriteLine("---------------");

    }

    public int CompareHands(int dealerScore, Card[] playerCards)
    {
        int playerScore = Scorer.GetHandScore(playerCards);
        if (playerScore > dealerScore)
            return 1;
        if (playerScore < dealerScore)
            return -1;
        return 0;
    }

}
