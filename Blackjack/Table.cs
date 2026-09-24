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

    public const int StartHandCheckSleepTimeMilliSeconds = 500;
    public const int RoundOutcomeAnnouncementSleepTimeMilliSeconds = 500;

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

        Console.WriteLine("Welcome!");

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
                Thread.Sleep(1000);
                break;
            }
        }

        Console.WriteLine("\nYou ended with " + player.Chips + " chips");
        Console.WriteLine("Thank you for playing");
        Console.WriteLine("Press any button to exit");
        input.ReadKey();
    }

    public void StartRound()
    {
        Console.WriteLine("-----------");
        Console.WriteLine("Round Start");
        Console.WriteLine("-----------\n");

        player.MakeBet();
        Console.WriteLine();

        deck = new CardDeck();
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
        Thread.Sleep(StartHandCheckSleepTimeMilliSeconds);
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
                Thread.Sleep(RoundOutcomeAnnouncementSleepTimeMilliSeconds);
                break;
            case -1:
                Console.WriteLine("You lost...");
                player.LoseBet();
                Thread.Sleep(RoundOutcomeAnnouncementSleepTimeMilliSeconds);
                break;
            case 0:
                Console.WriteLine("It\'s a standoff");
                player.ReturnChips();
                Thread.Sleep(RoundOutcomeAnnouncementSleepTimeMilliSeconds);
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
