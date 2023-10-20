using Microsoft.AspNetCore.SignalR;

namespace Panda.Games.WebApi.Hub;

public class TicTacToeHub : Microsoft.AspNetCore.SignalR.Hub
{
	private static readonly TicTacToe.TicTacToe Game = new();
	
	private static readonly Dictionary<string, TicTacToe.TicTacToe> GameRooms = new Dictionary<string, TicTacToe.TicTacToe>();

	public override async Task OnConnectedAsync()
	{

		var username = Context.GetHttpContext()!.Request.Query["username"]!.ToString();
		var connectionId = Context.ConnectionId;
		

		Game.InitPlayer(username, connectionId);
		
		
		if (Game.Players.Length <= 2) 
			await base.OnConnectedAsync();

		if (Game.Players.Length == 2)
		{
			// a player could have left before and we want to restart the game when a new one joins and we actually are 2 players :>
			
			Game.Restart(); 
			await Clients.All.SendAsync("Update", Game.State);
		}
	}
	
	public override async Task OnDisconnectedAsync(Exception? exception)
	{
		await base.OnDisconnectedAsync(exception);
		Game.RemovePlayer(Context.ConnectionId);
		await Clients.All.SendAsync("Update", Game.State);
	}
	
	public async Task MakeTurn(int fieldIndex)
	{
		if (fieldIndex is > 8 or < 0) 
			return;
		
		Game.MakeTurn(fieldIndex);
		await Clients.All.SendAsync("Update", Game.State);
	}

	public async Task NewGame()
	{
		Game.Restart();
		await Clients.All.SendAsync("Update", Game.State);
	}
}