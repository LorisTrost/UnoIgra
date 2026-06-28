namespace UnoIgra;

public sealed class UnoGame
{
    public List<Player> Players { get; } = new();
    public DrawPile DrawPile { get; } = new();
    public DiscardPile DiscardPile { get; } = new();
    public List<string> Log { get; } = new();

    public int CurrentPlayerIndex { get; private set; }
    public int Direction { get; private set; } = 1;
    public CardColor ActiveColor { get; private set; }
    public bool IsGameOver { get; private set; }
    public Player? Winner { get; private set; }

    public Player CurrentPlayer => Players[CurrentPlayerIndex];

    public void StartNewGame(int numberOfComputerPlayers = 2)
    {
        Players.Clear();
        Log.Clear();
        DrawPile.Clear();
        DiscardPile.Clear();

        IsGameOver = false;
        Winner = null;
        Direction = 1;
        CurrentPlayerIndex = 0;

        Players.Add(new HumanPlayer("Ti"));

        for (int i = 1; i <= numberOfComputerPlayers; i++)
            Players.Add(new ComputerPlayer($"Računalnik {i}"));

        DrawPile.BuildStandardUnoDeck();
        DrawPile.Shuffle();

        for (int card = 0; card < 7; card++)
        {
            foreach (Player player in Players)
                player.Draw(DrawOneCard());
        }

        StartDiscardPileWithNumberCard();
        AddLog("Nova igra se je začela.");
        AddLog($"Prva karta: {DiscardPile.TopCard}.");
    }

    public bool CanHumanPlayCard(int handIndex)
    {
        if (IsGameOver)
            return false;

        if (!CurrentPlayer.IsHuman)
            return false;

        if (handIndex < 0 || handIndex >= CurrentPlayer.Hand.Count)
            return false;

        return CurrentPlayer.Hand[handIndex].CanBePlayedOn(DiscardPile.TopCard, ActiveColor);
    }

    public string PlayHumanCard(int handIndex, CardColor? selectedColor)
    {
        if (IsGameOver)
            return "Igra je že končana.";

        if (!CurrentPlayer.IsHuman)
            return "Zdaj ni tvoja poteza.";

        if (!CanHumanPlayCard(handIndex))
            return "Te karte ne moreš odigrati.";

        Card card = CurrentPlayer.Hand[handIndex];

        if (card.IsWild && selectedColor is null)
            return "Pri črni karti moraš izbrati barvo.";

        PlayCard(CurrentPlayer, handIndex, selectedColor);
        return "Karta je bila odigrana.";
    }

    public string HumanDrawCard()
    {
        if (IsGameOver)
            return "Igra je že končana.";

        if (!CurrentPlayer.IsHuman)
            return "Zdaj ni tvoja poteza.";

        Card drawn = DrawOneCard();
        CurrentPlayer.Draw(drawn);
        AddLog($"{CurrentPlayer.Name} vleče karto.");
        MoveToNextPlayer(1);
        return $"Vlekel si karto: {drawn}.";
    }

    public void ProcessComputerTurns()
    {
        while (!IsGameOver && !CurrentPlayer.IsHuman)
            PlayComputerTurn();
    }

    private void PlayComputerTurn()
    {
        ComputerPlayer computer = (ComputerPlayer)CurrentPlayer;
        int cardIndex = computer.ChooseCardIndex(DiscardPile.TopCard, ActiveColor);

        if (cardIndex == -1)
        {
            Card drawn = DrawOneCard();
            computer.Draw(drawn);
            AddLog($"{computer.Name} nima ustrezne karte in vleče eno karto.");
            MoveToNextPlayer(1);
            return;
        }

        Card card = computer.Hand[cardIndex];
        CardColor? chosenColor = card.IsWild ? computer.ChooseBestColor() : null;
        PlayCard(computer, cardIndex, chosenColor);
    }

    private void PlayCard(Player player, int handIndex, CardColor? selectedColor)
    {
        Card card = player.RemoveCardAt(handIndex);
        DiscardPile.Place(card);
        ActiveColor = card.IsWild ? selectedColor!.Value : card.Color;

        AddLog($"{player.Name} odigra {card}. Aktivna barva: {Card.ColorToSlovene(ActiveColor)}.");

        if (player.Hand.Count == 1)
            AddLog($"{player.Name} ima samo še eno karto: UNO!");

        if (player.Hand.Count == 0)
        {
            Winner = player;
            IsGameOver = true;
            AddLog($"Zmagovalec: {player.Name}.");
            return;
        }

        ApplyCardEffect(card);
    }

    private void ApplyCardEffect(Card card)
    {
        switch (card.Value)
        {
            case CardValue.DrawTwo:
                GiveCardsToNextPlayer(2);
                MoveToNextPlayer(2);
                break;

            case CardValue.WildDrawFour:
                GiveCardsToNextPlayer(4);
                MoveToNextPlayer(2);
                break;

            case CardValue.Skip:
                AddLog($"{PeekNextPlayer().Name} preskoči potezo.");
                MoveToNextPlayer(2);
                break;

            case CardValue.Reverse:
                Direction *= -1;
                AddLog("Smer igre se obrne.");
                MoveToNextPlayer(Players.Count == 2 ? 2 : 1);
                break;

            default:
                MoveToNextPlayer(1);
                break;
        }
    }

    private void GiveCardsToNextPlayer(int amount)
    {
        Player target = PeekNextPlayer();

        for (int i = 0; i < amount; i++)
            target.Draw(DrawOneCard());

        AddLog($"{target.Name} dobi {amount} kart in izgubi potezo.");
    }

    private Player PeekNextPlayer()
    {
        return Players[GetRelativePlayerIndex(1)];
    }

    private void MoveToNextPlayer(int steps)
    {
        CurrentPlayerIndex = GetRelativePlayerIndex(steps);
        AddLog($"Na potezi: {CurrentPlayer.Name}.");
    }

    private int GetRelativePlayerIndex(int steps)
    {
        int index = CurrentPlayerIndex;

        for (int i = 0; i < steps; i++)
        {
            index += Direction;

            if (index < 0)
                index = Players.Count - 1;
            else if (index >= Players.Count)
                index = 0;
        }

        return index;
    }

    private Card DrawOneCard()
    {
        if (DrawPile.Count == 0)
            RecycleDiscardPile();

        return DrawPile.DrawCard();
    }

    private void RecycleDiscardPile()
    {
        List<Card> recycledCards = DiscardPile.TakeAllExceptTopCard();

        if (recycledCards.Count == 0)
            throw new InvalidOperationException("Ni več kart za vlečenje.");

        DrawPile.AddRange(recycledCards);
        DrawPile.Shuffle();
        AddLog("Odložene karte so bile premešane nazaj v kup za vlečenje.");
    }

    private void StartDiscardPileWithNumberCard()
    {
        List<Card> skippedCards = new();

        while (true)
        {
            Card card = DrawPile.DrawCard();

            if (card.IsNumber)
            {
                DiscardPile.Place(card);
                ActiveColor = card.Color;
                DrawPile.AddRange(skippedCards);
                DrawPile.Shuffle();
                return;
            }

            skippedCards.Add(card);
        }
    }

    private void AddLog(string message)
    {
        Log.Add(message);
    }
}
