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

    public void PrintChipAmount()
    {
        Console.WriteLine("You have " + Chips + " chips to bet with.");
    }

    public virtual void MakeBet()
    {
        while (true)
        {
            if (SetBet())
                break;
        }
    }

    bool SetBet()
    {
        PrintChipAmount();
        Console.WriteLine("Please type in the number of chips you want to bet and press ENTER");
        string? inputLine = input.ReadLine();
        if (int.TryParse(inputLine, out int inputNumber))
        {
            if (inputNumber < 0)
            {
                Console.WriteLine("The bet cannot be negative");
                return false;
            }
            if (inputNumber == 0)
            {
                Console.WriteLine("The bet must be a non-zero value");
                return false;
            }
            
            if (inputNumber > Chips)
            {
                inputNumber = Chips;
                Console.WriteLine("Cannot exceed your amount of chips - Setting the bet to " + inputNumber);
            }
            Chips -= inputNumber;
            Bet = inputNumber;
            return true;
        }
        else
        {
            Console.WriteLine("Invalid input - Please input a valid number");
        }
        return false;
    }

    public void WinBet(float multiplier)
    {
        int prizeChips = (int)MathF.Floor(Bet * multiplier);
        Chips += prizeChips;
        Bet = 0;
        Console.WriteLine("You received " + prizeChips + " from your bet (" + multiplier.ToString("F2") + "x your bet)\nMaking your chips total " + Chips);
    }

    public void LoseBet()
    {
        Console.WriteLine("The table took the " + Bet + " chips that you had bet");
        Bet = 0;
    }

    public void ReturnChips()
    {
        Chips += Bet;
        Bet = 0;
        Console.WriteLine("Your chips have been returned to you\nBringing you back to a chips total of " + Chips);
    }

}
