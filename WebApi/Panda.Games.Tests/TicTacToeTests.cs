namespace Panda.Games.Tests;

public class TicTacToeTests
{
    [Fact]
    public void PlayerCreated()
    {
        TicTacToe.TicTacToe game = new TicTacToe.TicTacToe();
        game.InitPlayer("t", "1");
        
        Assert.True(game.Players.Length == 1);
    }
    
    [Fact]
    public void GamePrepared()
    {
        TicTacToe.TicTacToe game = new TicTacToe.TicTacToe();
        
        game.InitPlayer("t", "1");
        game.MakeTurn(0);
        game.Restart();
        
        Assert.Equal(1, game.Players.Length);
        Assert.True(game.GameBoard.All(x => x.Sign == ""));
    }

    [Fact]
    public void SignInFieldSet()
    {
        TicTacToe.TicTacToe game = new TicTacToe.TicTacToe();
        game.InitPlayer("t", "1");
        game.InitPlayer("b", "2");
        
        game.MakeTurn(0);
        Assert.True(game.GameBoard[0].Sign != "");
    }

    [Fact]
    public void RightSignSet()
    {
        TicTacToe.TicTacToe game = new TicTacToe.TicTacToe();
        game.InitPlayer("t", "1");
        game.InitPlayer("b", "2");
        game.MakeTurn(0);
        game.MakeTurn(1);
        
        Assert.True(game.GameBoard[0].Sign == "x");
        Assert.True(game.GameBoard[1].Sign == "o");
    }

    [Fact]
    public void PlayerSet()
    {
        TicTacToe.TicTacToe game = new TicTacToe.TicTacToe();
        game.InitPlayer("t", "1");
        game.InitPlayer("b", "2");
        game.MakeTurn(0);
        
        Assert.True(game.Players[1].CurrentTurn);
    }

    [Fact]
    public void DetectedWin()
    {
        TicTacToe.TicTacToe game = new TicTacToe.TicTacToe();
        game.Restart();
        game.InitPlayer("t", "1");
        game.InitPlayer("h", "2");

        game.MakeTurn(0);
        game.MakeTurn(1);
        game.MakeTurn(2);
        game.MakeTurn(3);
        game.MakeTurn(4);
        game.MakeTurn(5);
        game.MakeTurn(6);
        
        Assert.True(game.Players[0].Wins == 1);
    }

    [Fact]
    public void OnlyTwoPlayer()
    {
        TicTacToe.TicTacToe game = new TicTacToe.TicTacToe();
        game.Restart();
        game.InitPlayer("t", "1");
        game.InitPlayer("t", "1");
        game.InitPlayer("t", "1");

        Assert.True(game.Players.Length == 2);
    }

    [Fact]
    public void SetFieldOnlyByTwoPlayer()
    {
        TicTacToe.TicTacToe game = new TicTacToe.TicTacToe();
        game.Restart();
        game.InitPlayer("t", "1");
        
        game.MakeTurn(0);
        
        Assert.True(game.GameBoard[0].Sign == "");
    }
}