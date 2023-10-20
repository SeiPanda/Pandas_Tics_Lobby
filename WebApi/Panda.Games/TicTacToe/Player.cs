namespace Panda.Games.TicTacToe;

// setters are all internal, because we do not want other applications to mess with our current player state ;)
public class Player
{
	public string Id { get; internal set; } = null!;
	public string Name { get; internal set; } = null!;
	public int Wins { get; internal set; }
	public string Sign { get; internal set; } = null!;
	public bool CurrentTurn { get; internal set; }
}



public class LobbyPlayer
{
	public string Id { get; internal set; } = null!;
	public string Name { get; internal set; } = null!;
}