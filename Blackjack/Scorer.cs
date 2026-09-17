using System;

public static class Scorer
{
    
    public static int GetHandScore(Card[] cards)
    {
        SortCardHand(cards);
        int totalScore = 0;
        foreach (Card card in cards)
        {
            totalScore.AddCardValue(card);
        }
        return totalScore;
    }

    public static void SortCardHand(Card[] cards)
    {
        cards.Sort((x, y) => {
            if (x.rank < y.rank)
                return 1;
            if (x.rank == y.rank)
                return 0;
            return -1;
        });
    }

    public static void AddCardValue(this ref int score, Card card)
    {
        switch (card.type)
        {
            case CardType.Numbered:
                score += card.rank;
                break;
            case CardType.Face:
                score += 10;
                break;
            case CardType.Ace:
                score += (score + 11 > 21) ? 1 : 11;
                break;
        }
    }

}

