using System;
using System.Collections.Generic;
using System.Text;

public class Gambler : Player
{

    Input input = new Input();

    public const int StartChipAmount = 100;
    public int Chips { get; private set; } = StartChipAmount;
    public int Bet { get; private set; }
    
    public Gambler() : base() {
        name = "You";
    }

    public Gambler(Input input)
    {
        this.input = input;
        name = "You";
    }

    public Gambler(int chips, int bet)
    {
        Chips = chips;
        Bet = bet;
        name = "You";
    }

    public virtual void StartTurn()
    {
        Console.WriteLine("--------------------");
        Console.WriteLine("It is now your turn:");
        ConsoleKeyInfo inputInfo;
        do
        {
            Console.WriteLine();
            PrintHand();
            PrintCurrentHandScore();
            Console.WriteLine("\nOptions:\nH - hit\nS - Stand\n");
            
            inputInfo = input.ReadKey();
            if (inputInfo.Key == ConsoleKey.H)
            {
                Hit(Table.Current.deck.DrawCard());
                if (Scorer.GetHandScore(hand.ToArray()) == Scorer.BustScore)
                    break;
            }
        } while (inputInfo.Key != ConsoleKey.S);

        Stand();
    }

    public void MakeBet()
    {
        throw new NotImplementedException();
    }

    public void WinBet(float multiplier)
    {
        throw new NotImplementedException();
    }

    public void LoseBet()
    {
        throw new NotImplementedException();
    }

    public void ReturnChips()
    {
        throw new NotImplementedException();
    }



}
