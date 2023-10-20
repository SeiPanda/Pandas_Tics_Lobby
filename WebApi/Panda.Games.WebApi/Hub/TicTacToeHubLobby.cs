using Microsoft.AspNetCore.SignalR;

namespace Panda.Games.WebApi.Hub;

public class TicTacToeHubLobby : Microsoft.AspNetCore.SignalR.Hub
{
    
    private static readonly Lobby Lobby = new();
    
    public override async Task OnConnectedAsync()
    {
        var username = Context.GetHttpContext()!.Request.Query["username"]!.ToString();
        var privateKey = Context.GetHttpContext()!.Request.Query["key"]!.ToString();
        var connectionId = Context.ConnectionId;
        
        var state = Lobby.JoinLobby(username, connectionId, privateKey);
        if (state.State.Players.Length == 2)
        {
            await Clients
                .Clients(state.State.Players.Select(x => x.Id))
                .SendAsync("Update", state);
        }
    }
	
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await base.OnDisconnectedAsync(exception);
        var state = Lobby.LeaveLobby(Context.ConnectionId);
        if (state == null)
            return;
        
        await Clients
            .Clients(state.State.Players.Select(x => x.Id))
            .SendAsync("Update", state);
    }
	
    public async Task MakeTurn(int fieldIndex)
    {
        if (fieldIndex is > 8 or < 0) 
            return;
		
        var state = Lobby.MakeTurn(Context.ConnectionId, fieldIndex);
        if (state == null)
            return;
        
        await Clients
            .Clients(state.State.Players.Select(x => x.Id))
            .SendAsync("Update", state);
    }

    public async Task NewGame()
    {
        var state = Lobby.NewGame(Context.ConnectionId);
        if (state == null)
            return;
        
        await Clients
            .Clients(state.State.Players.Select(x => x.Id))
            .SendAsync("Update", state);
    }
}