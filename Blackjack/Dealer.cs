using System;

public class Dealer : Player
{

	public bool WillStand { get; private set; } = false;
	public int highestPlayerHand = 0;
	
	public Dealer() : base() { }

	public void StartTurn()
	{
		throw new NotImplementedException();
	}

	public void MakeDecision()
	{
        throw new NotImplementedException();
    }


}
