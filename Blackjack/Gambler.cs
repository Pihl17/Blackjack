using System;
using System.Collections.Generic;
using System.Text;

public class Gambler : Player
{
    public Gambler(CardDeck deck) : base(deck) { }

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
                Hit(deck.DrawCard());
            }
            if (input.Key == ConsoleKey.C)
            {
                PrintHand();
            }
        } while (input.Key != ConsoleKey.Escape);
    }


}
