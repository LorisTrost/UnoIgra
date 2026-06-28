namespace UnoIgra;

public sealed class ComputerPlayer : Player
{
    public override bool IsHuman => false;

    public ComputerPlayer(string name) : base(name)
    {
    }

    public int ChooseCardIndex(Card topCard, CardColor activeColor)
    {
        var playableIndexes = Hand
            .Select((card, index) => new { Card = card, Index = index })
            .Where(x => x.Card.CanBePlayedOn(topCard, activeColor))
            .ToList();

        if (playableIndexes.Count == 0)
            return -1;

        var normalCards = playableIndexes.Where(x => !x.Card.IsWild).ToList();
        var candidates = normalCards.Count > 0 ? normalCards : playableIndexes;

        return candidates
            .OrderByDescending(x => ScoreCard(x.Card))
            .ThenByDescending(x => Hand.Count(card => card.Color == x.Card.Color))
            .First()
            .Index;
    }

    private int ScoreCard(Card card)
    {
        int colorSupport = card.Color == CardColor.Wild ? 0 : Hand.Count(other => other.Color == card.Color);

        int baseScore = card.Value switch
        {
            CardValue.WildDrawFour => 90,
            CardValue.DrawTwo => 80,
            CardValue.Skip => 70,
            CardValue.Reverse => 60,
            CardValue.Wild => 50,
            CardValue.Nine => 19,
            CardValue.Eight => 18,
            CardValue.Seven => 17,
            CardValue.Six => 16,
            CardValue.Five => 15,
            CardValue.Four => 14,
            CardValue.Three => 13,
            CardValue.Two => 12,
            CardValue.One => 11,
            CardValue.Zero => 10,
            _ => 0
        };

        return baseScore + colorSupport;
    }
}
