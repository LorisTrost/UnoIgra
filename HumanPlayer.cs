namespace UnoIgra;

public sealed class HumanPlayer : Player
{
    public override bool IsHuman => true;

    public HumanPlayer(string name) : base(name)
    {
    }
}
