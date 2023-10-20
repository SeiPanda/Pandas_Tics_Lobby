using System.Collections.Immutable;

namespace Panda.Games.TicTacToe;

public class GameState
{
	public ImmutableArray<Player> Players { get; internal set; } = ImmutableArray<Player>.Empty;
	public ImmutableArray<Field> GameBoard { get; internal set; } = ImmutableArray<Field>.Empty;
	public bool IsDraw { get; internal set; }
	public bool HasWinner { get; internal set; }
	public bool GameOver { get; internal set; }
}