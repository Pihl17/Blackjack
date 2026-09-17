using System;
using System.Collections.Generic;
using System.Numerics;

public class Player
{

	public List<Card> hand = new List<Card>();

	public Player() { }

    public delegate void OnTurnEndingEvent();
    public OnTurnEndingEvent OnTurnEnding;

	public void Hit(Card card)
	{
		hand.Add(card);
		Console.WriteLine("You have been dealt a " + card.rank);
	}

    public void Stand()
    {
        Console.WriteLine("You stand with a hand value of " + Scorer.GetHandScore(hand.ToArray()));
        OnTurnEnding?.Invoke();
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
