using Panda.Games.TicTacToe;

namespace Panda.Games;

public class Lobby
{
    private readonly List<GameRoom> _gameRooms = new();

    private GameRoom? FindRoom(string connectionId)
    {
        return _gameRooms.FirstOrDefault(x => x.Game.Players.Any(y => y.Id == connectionId));
    }
    private GameRoom GetFreeRoom(string privateKey)
    {
        GameRoom? openRoom;

        openRoom = _gameRooms.Find(x => x.Name == "Room " + privateKey);

        if (openRoom != null) 
            return openRoom;
        
        openRoom = new GameRoom("Room " + privateKey, "private");
        _gameRooms.Add(openRoom);

        return openRoom;
    }
    
    private GameRoom GetFreeRoom()
    {
        GameRoom? openRoom;

        var rooms =_gameRooms.FindAll(x => x.Kind == "public");
        // first room ever
        if (rooms.Count == 0)
        {
            openRoom = new GameRoom("Room 1", "public");
            _gameRooms.Add(openRoom);
        }

        // find open room containing less than 2 players
        // if room is null we need to create a new one
        openRoom = rooms.FirstOrDefault(x => x.Game.Players.Length == 1) 
                   ?? rooms.FirstOrDefault(x => x.Game.Players.Length == 0);

        if (openRoom != null) 
            return openRoom;
        
        openRoom = new GameRoom("Room " + (rooms.Count + 1), "public");
        _gameRooms.Add(openRoom);

        return openRoom;
    }

    public GameRoomState JoinLobby(string userName, string id, string privateKey)
    {

        if (privateKey != "")
        {
            var openRoom = GetFreeRoom(privateKey);
            openRoom.Game.InitPlayer(userName, id);

            if (openRoom.Game.Players.Length == 2)
            {
                openRoom.Game.Restart();
            }
        
            return openRoom.State;
        }
        else
        {
            var openRoom = GetFreeRoom();
            openRoom.Game.InitPlayer(userName, id);

            if (openRoom.Game.Players.Length == 2)
            {
                openRoom.Game.Restart();
            }
        
            return openRoom.State;
        }
    }
    
    public GameRoomState? LeaveLobby(string connectionId)
    {
        var room = FindRoom(connectionId);
        if (room == null)
            return null;
        
        room.Game.RemovePlayer(connectionId);

        room.Game.Players[0].Wins = 0;
        
        return room.State;
    }
    
    public GameRoomState? MakeTurn(string connectionId, int fieldIndex)
    {
        var room = FindRoom(connectionId);
        if (room == null)
            return null;
        
        room.Game.MakeTurn(fieldIndex);
        return room.State;
    }

    public GameRoomState? NewGame(string connectionId)
    {
        var room = FindRoom(connectionId);
        if (room == null)
            return null;
        
        room.VoteRestart(connectionId);
        return room.State;
    }
}