using System;
using System.Collections.Generic;
using System.Numerics;

public class Player
{

	public List<Card> hand = new List<Card>();

    public CardDeck deck;

	public Player(CardDeck deck)
	{
        this.deck = deck;
	}

	public void Hit(Card card)
	{
		hand.Add(card);
		Console.WriteLine("You have been dealt a " + card.rank);
	}

	public void StartTurn()
	{
        Console.WriteLine("It is now your turn:");
        Console.WriteLine("Press C to show hand\nPress H to hit \nPress Escape to Stand");
        ConsoleKeyInfo input;
        do {
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

    public void PrintHand()
    {
        Console.Write("Your current hand is: ");
        for (int i = 0; i < hand.Count; i++)
        {
            Console.Write(hand[i].rank + " ");
        }
        Console.WriteLine();
    }

}
