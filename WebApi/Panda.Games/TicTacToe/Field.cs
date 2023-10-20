namespace Panda.Games.TicTacToe;

public class Field
{ 
    public int Id { get; internal set; }
    public string Sign { get; internal set; } = null!;
    public bool Clicked => !string.IsNullOrWhiteSpace(Sign);
    public bool Win { get; internal set; }
}