using System;

public class Dealer : Player
{
	public Dealer() : base() { }

	public void StartTurn()
	{
		Turn(Scorer.GetHandScore(Table.Current.player.hand.ToArray()));
	}

	public void Turn(int playerScore)
	{
        while (ShouldHit(Scorer.GetHandScore(hand.ToArray()), playerScore))
        {
            Hit(Table.Current.deck.DrawCard());
        }
        PrintHand();
        Stand();
    }


	public bool ShouldHit(int ownScore, int opponentScore)
	{
		return ownScore < 17 && ownScore <= opponentScore;
	}




}
