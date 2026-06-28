namespace UnoIgra;

public enum CardColor
{
    Red,
    Yellow,
    Green,
    Blue,
    Wild
}

public enum CardValue
{
    Zero,
    One,
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Skip,
    Reverse,
    DrawTwo,
    Wild,
    WildDrawFour
}

public sealed class Card
{
    public CardColor Color { get; }
    public CardValue Value { get; }

    public bool IsWild => Color == CardColor.Wild || Value == CardValue.Wild || Value == CardValue.WildDrawFour;
    public bool IsAction => Value is CardValue.Skip or CardValue.Reverse or CardValue.DrawTwo or CardValue.Wild or CardValue.WildDrawFour;
    public bool IsNumber => Value >= CardValue.Zero && Value <= CardValue.Nine;

    public string ImageFileName
    {
        get
        {
            if (Value == CardValue.Wild)
                return "wild.png";

            if (Value == CardValue.WildDrawFour)
                return "wild_draw_four.png";

            string color = Color.ToString().ToLowerInvariant();
            string value = Value switch
            {
                CardValue.Zero => "0",
                CardValue.One => "1",
                CardValue.Two => "2",
                CardValue.Three => "3",
                CardValue.Four => "4",
                CardValue.Five => "5",
                CardValue.Six => "6",
                CardValue.Seven => "7",
                CardValue.Eight => "8",
                CardValue.Nine => "9",
                CardValue.Skip => "skip",
                CardValue.Reverse => "reverse",
                CardValue.DrawTwo => "draw_two",
                _ => Value.ToString().ToLowerInvariant()
            };

            return $"{color}_{value}.png";
        }
    }

    public Card(CardColor color, CardValue value)
    {
        Color = color;
        Value = value;
    }

    public bool CanBePlayedOn(Card topCard, CardColor activeColor)
    {
        if (IsWild)
            return true;

        return Color == activeColor || Value == topCard.Value;
    }

    public override string ToString()
    {
        if (Value == CardValue.Wild)
            return "Menjava barve";

        if (Value == CardValue.WildDrawFour)
            return "+4 menjava";

        return $"{ColorToSlovene(Color)} {ValueToSlovene(Value)}";
    }

    public static string ColorToSlovene(CardColor color)
    {
        return color switch
        {
            CardColor.Red => "Rdeča",
            CardColor.Yellow => "Rumena",
            CardColor.Green => "Zelena",
            CardColor.Blue => "Modra",
            CardColor.Wild => "Črna",
            _ => color.ToString()
        };
    }

    public static string ValueToSlovene(CardValue value)
    {
        return value switch
        {
            CardValue.Zero => "0",
            CardValue.One => "1",
            CardValue.Two => "2",
            CardValue.Three => "3",
            CardValue.Four => "4",
            CardValue.Five => "5",
            CardValue.Six => "6",
            CardValue.Seven => "7",
            CardValue.Eight => "8",
            CardValue.Nine => "9",
            CardValue.Skip => "Preskok",
            CardValue.Reverse => "Obrat",
            CardValue.DrawTwo => "+2",
            CardValue.Wild => "Menjava barve",
            CardValue.WildDrawFour => "+4 menjava",
            _ => value.ToString()
        };
    }
}
