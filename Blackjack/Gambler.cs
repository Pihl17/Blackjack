using System;
using System.Collections.Generic;
using System.Text;

public class Gambler : Player
{
    public Gambler() : base() { }

    public void StartTurn()
    {
        Console.WriteLine("It is now your turn:");
        Console.WriteLine("Press C to show hand\nPress H to hit \nPress Escape to Stand");
        ConsoleKeyInfo input;
        do
        {
            input = Console.ReadKey(true);
            if (input.Key == ConsoleKey.H)
            {
                Hit(Table.Current.deck.DrawCard());
            }
            if (input.Key == ConsoleKey.C)
            {
                PrintHand();
            }
        } while (input.Key != ConsoleKey.Escape);
    }


}
