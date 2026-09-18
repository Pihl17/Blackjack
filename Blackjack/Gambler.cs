using System;
using System.Collections.Generic;
using System.Text;

public class Gambler : Player
{

    Input input = new Input();
    
    public Gambler() : base() { }

    public Gambler(Input input)
    {
        this.input = input;
    }

    public virtual void StartTurn()
    {
        Console.WriteLine("It is now your turn:");
        Console.WriteLine("Press C to show hand\nPress H to hit \nPress S to Stand");
        ConsoleKeyInfo inputInfo;
        do
        {
            inputInfo = input.ReadKey();
            if (inputInfo.Key == ConsoleKey.H)
            {
                Hit(Table.Current.deck.DrawCard());
                if (Scorer.GetHandScore(hand.ToArray()) > 21)
                    break;
            }
            if (inputInfo.Key == ConsoleKey.C)
            {
                PrintHand();
            }
        } while (inputInfo.Key != ConsoleKey.S);

        Stand();
    }



}
