using Panda.Games.TicTacToe;

namespace Panda.Games;

public class GameRoomState
{
    public string RoomKind { get; set; }
    public GameState State { get; set; }
    
    public string RoomName { get; set; }
}