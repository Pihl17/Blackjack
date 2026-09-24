using System;
using System.Collections.Generic;
using System.Numerics;

public class Player
{

    public string name { get; protected set; }
	public List<Card> hand = new List<Card>();
    
    public delegate void OnTurnEndingEvent();
    public OnTurnEndingEvent? OnTurnEnding;
    
    public const int HitSleepTimeMilliSeconds = 100;
    public const int StandSleepTimeMilliSeconds = 500;

	public Player() {
        name = "";
    }

	public void Hit(Card card)
	{
		hand.Add(card);
		Console.WriteLine(name + " have been dealt a " + card.RankName);
        Thread.Sleep(HitSleepTimeMilliSeconds);
	}

    public void Stand()
    {
        Console.WriteLine(name + " stand with a hand value of " + CurrentScore());
        Thread.Sleep(StandSleepTimeMilliSeconds);
        OnTurnEnding?.Invoke();
    }

    public void PrintHand()
    {
        Console.Write(name + " currently have: ");
        for (int i = 0; i < hand.Count; i++)
        {
            Console.Write(hand[i].RankName + " ");
        }
        Console.WriteLine();
    }

    public void PrintCurrentHandScore()
    {
        Console.WriteLine(name + " currently have a score of " + CurrentScore());
    }

    string CurrentScore()
    {
        int score = Scorer.GetHandScore(hand.ToArray());
        if (score == Scorer.BustScore)
            return "Bust";
        else
            return score.ToString();
    }

}
