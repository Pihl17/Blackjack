using System;
using System.Collections.Generic;
using System.Text;

public class Gambler : Player
{

    Input input = new Input();
    
    public Gambler() : base() {
        name = "You";
    }

    public Gambler(Input input)
    {
        this.input = input;
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



}
