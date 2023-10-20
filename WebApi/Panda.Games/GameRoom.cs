namespace Panda.Games;

public class GameRoom
{
    private readonly List<string> _restartVotings = new();
    public string Name { get; }
    public string Kind { get; }

    public TicTacToe.TicTacToe Game { get; } = new TicTacToe.TicTacToe();

    public GameRoom(string name, string roomKind)
    {
        Name = name;
        Kind = roomKind;

    }
    
    public GameRoomState State => new()
    {
        State = Game.State,
        RoomName = Name,
        RoomKind = Kind
    };
    public void VoteRestart(string connectionId)
    {
        _restartVotings.Add(connectionId);
        
        foreach (var id in Game.Players.Select(x => x.Id))
        {
            var yep = _restartVotings.Contains(id);
            if (yep == false)
                return;
        }

        _restartVotings.Clear();
        Game.Restart();
    }
}