namespace UnoIgra;

public abstract class CardPile
{
    private static readonly Random Random = new();
    protected readonly List<Card> CardsInternal = new();

    public int Count => CardsInternal.Count;
    public IReadOnlyList<Card> Cards => CardsInternal.AsReadOnly();

    public void Add(Card card)
    {
        CardsInternal.Add(card);
    }

    public void AddRange(IEnumerable<Card> cards)
    {
        CardsInternal.AddRange(cards);
    }

    public void Clear()
    {
        CardsInternal.Clear();
    }

    public void Shuffle()
    {
        for (int i = CardsInternal.Count - 1; i > 0; i--)
        {
            int j = Random.Next(i + 1);
            (CardsInternal[i], CardsInternal[j]) = (CardsInternal[j], CardsInternal[i]);
        }
    }
}
