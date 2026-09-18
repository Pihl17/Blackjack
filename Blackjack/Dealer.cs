using System;

public class Dealer : Player
{

	public bool WillStand { get; private set; } = false;
	
	public Dealer() : base() { }

	public void StartTurn()
	{
		while (!WillStand)
		{
			MakeDecision();
		}
		PrintHand();
		Stand();
	}

	public void MakeDecision()
	{
		int score = Scorer.GetHandScore(hand.ToArray());

		if (score >= 21 || score > Table.Current.highestPlayerHandScore)
		{
			WillStand = true;
			return;
		}
		if (score < 17 || Scorer.IsSoft(hand.ToArray()))
		{
			Hit(Table.Current.deck.DrawCard());
			return;
		}

		WillStand = true;
    }


}
