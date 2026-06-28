namespace UnoIgra;

public sealed class DrawPile : CardPile
{
    public void BuildStandardUnoDeck()
    {
        Clear();

        CardColor[] colors =
        {
            CardColor.Red,
            CardColor.Yellow,
            CardColor.Green,
            CardColor.Blue
        };

        foreach (CardColor color in colors)
        {
            Add(new Card(color, CardValue.Zero));

            for (int copy = 0; copy < 2; copy++)
            {
                Add(new Card(color, CardValue.One));
                Add(new Card(color, CardValue.Two));
                Add(new Card(color, CardValue.Three));
                Add(new Card(color, CardValue.Four));
                Add(new Card(color, CardValue.Five));
                Add(new Card(color, CardValue.Six));
                Add(new Card(color, CardValue.Seven));
                Add(new Card(color, CardValue.Eight));
                Add(new Card(color, CardValue.Nine));
                Add(new Card(color, CardValue.Skip));
                Add(new Card(color, CardValue.Reverse));
                Add(new Card(color, CardValue.DrawTwo));
            }
        }

        for (int i = 0; i < 4; i++)
        {
            Add(new Card(CardColor.Wild, CardValue.Wild));
            Add(new Card(CardColor.Wild, CardValue.WildDrawFour));
        }
    }

    public Card DrawCard()
    {
        if (Count == 0)
            throw new InvalidOperationException("Kup za vlečenje je prazen.");

        int lastIndex = CardsInternal.Count - 1;
        Card card = CardsInternal[lastIndex];
        CardsInternal.RemoveAt(lastIndex);
        return card;
    }
}
