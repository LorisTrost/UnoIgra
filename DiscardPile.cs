namespace UnoIgra;

public sealed class DiscardPile : CardPile
{
    public Card TopCard
    {
        get
        {
            if (Count == 0)
                throw new InvalidOperationException("Odlagalni kup je prazen.");

            return CardsInternal[^1];
        }
    }

    public void Place(Card card)
    {
        Add(card);
    }

    public List<Card> TakeAllExceptTopCard()
    {
        if (Count <= 1)
            return new List<Card>();

        Card top = TopCard;
        List<Card> recyclable = CardsInternal.Take(CardsInternal.Count - 1).ToList();
        CardsInternal.Clear();
        CardsInternal.Add(top);
        return recyclable;
    }
}
