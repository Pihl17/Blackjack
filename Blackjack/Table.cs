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

    public const float WinMultiplierDefault = 2f;

    public Input input = new Input();
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
    
    public void Welcome()
    {

        while (true)
        {
            player.PrintChipAmount();
            Console.WriteLine("Wanna play another game? (Y/N)");
            ConsoleKeyInfo inputInfo = input.ReadKey();
            if (inputInfo.Key == ConsoleKey.Y)
            {
                StartRound();
            }
            if (inputInfo.Key == ConsoleKey.N)
            {
                break;
            }
            if (player.Chips <= 0)
            {
                Console.WriteLine("You have run out of chips and cannot play anymore.");
                break;
            }
        }

        Console.WriteLine("\nYou ended with " + player.Chips + " chips");
        Console.WriteLine("Thank you for playing");
    }

    public void StartRound()
    {
        Console.WriteLine("-----------");
        Console.WriteLine("Round Start");
        Console.WriteLine("-----------\n");

        player.MakeBet();
        Console.WriteLine();

        deck.Shuffle();
        DealStartHands();
        player.StartTurn();
    }

    public void DealStartHands()
    {
        player.hand.Clear();
        dealer.hand.Clear();
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
        DetermineRoundOutcome();
        
        Console.WriteLine();
        Console.WriteLine("---------------");
        Console.WriteLine("Round has Ended");
        Console.WriteLine("---------------");

    }

    public void DetermineRoundOutcome()
    {
        int dealerScore = Scorer.GetHandScore(dealer.hand.ToArray());
        int outcome = CompareHands(dealerScore, player.hand.ToArray());

        Console.WriteLine();
        switch (outcome)
        {
            case 1:
                Console.WriteLine("You won!");
                player.WinBet(WinMultiplierDefault);
                break;
            case -1:
                Console.WriteLine("You lost...");
                player.LoseBet();
                break;
            case 0:
                Console.WriteLine("It\'s a standoff");
                player.ReturnChips();
                break;
        }
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
