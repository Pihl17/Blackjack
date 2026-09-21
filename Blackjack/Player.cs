using System;
using System.Collections.Generic;
using System.Numerics;

public class Player
{

    public string name { get; protected set; }
	public List<Card> hand = new List<Card>();

	public Player() { }

    public delegate void OnTurnEndingEvent();
    public OnTurnEndingEvent OnTurnEnding;

	public void Hit(Card card)
	{
		hand.Add(card);
		Console.WriteLine(name + " have been dealt a " + card.RankName);
	}

    public void Stand()
    {
        Console.WriteLine(name + " stand with a hand value of " + Scorer.GetHandScore(hand.ToArray()));
        OnTurnEnding?.Invoke();
    }

    public void PrintHand()
    {
        Console.Write(name + " current hand is: ");
        for (int i = 0; i < hand.Count; i++)
        {
            Console.Write(hand[i].RankName + " ");
        }
        Console.WriteLine();
    }

    public void PrintCurrentHandScore()
    {
        Console.Write(name + " currently have a score of ");
        int score = Scorer.GetHandScore(hand.ToArray());
        if (score == Scorer.BustScore)
            Console.Write("Bust");
        else
            Console.Write(score.ToString());
        Console.WriteLine();
    }

}
