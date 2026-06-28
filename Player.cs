namespace UnoIgra;

public abstract class Player
{
    public string Name { get; }
    public List<Card> Hand { get; } = new();
    public abstract bool IsHuman { get; }

    protected Player(string name)
    {
        Name = name;
    }

    public void Draw(Card card)
    {
        Hand.Add(card);
    }

    public Card RemoveCardAt(int index)
    {
        Card card = Hand[index];
        Hand.RemoveAt(index);
        return card;
    }

    public List<Card> GetPlayableCards(Card topCard, CardColor activeColor)
    {
        return Hand.Where(card => card.CanBePlayedOn(topCard, activeColor)).ToList();
    }

    public bool HasPlayableCard(Card topCard, CardColor activeColor)
    {
        return Hand.Any(card => card.CanBePlayedOn(topCard, activeColor));
    }

    public virtual CardColor ChooseBestColor()
    {
        CardColor[] colors =
        {
            CardColor.Red,
            CardColor.Yellow,
            CardColor.Green,
            CardColor.Blue
        };

        return colors
            .OrderByDescending(color => Hand.Count(card => card.Color == color))
            .ThenBy(color => color.ToString())
            .First();
    }
}
