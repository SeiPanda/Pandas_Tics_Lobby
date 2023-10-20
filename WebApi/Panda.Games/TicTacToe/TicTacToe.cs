using System.Collections.Immutable;

namespace Panda.Games.TicTacToe;

public class TicTacToe
{
    private readonly List<Player> _players = new();
    private readonly Field[] _fields = {
        new() { Id = 0, Sign = "",  Win = false },
        new() { Id = 1, Sign = "",  Win = false },
        new() { Id = 2, Sign = "",  Win = false },
        new() { Id = 3, Sign = "",  Win = false },
        new() { Id = 4, Sign = "",  Win = false },
        new() { Id = 5, Sign = "",  Win = false },
        new() { Id = 6, Sign = "",  Win = false },
        new() { Id = 7, Sign = "",  Win = false },
        new() { Id = 8, Sign = "",  Win = false }
    };

    private bool _hasWinner = true;
    private bool _isDraw = true;
    
    public ImmutableArray<Player> Players => _players.ToImmutableArray();
    public ImmutableArray<Field> GameBoard => _fields.ToImmutableArray();
    public bool IsDraw => _isDraw;
    public bool HasWinner => _hasWinner;
    public bool GameOver => _hasWinner || _isDraw;
    
    public GameState State => new()
    {
        Players = Players,
        GameBoard = GameBoard,
        IsDraw = IsDraw,
        HasWinner = HasWinner,
        GameOver = GameOver
    };
    
    public void MakeTurn(int fieldIndex)
    {
        if (_players.Count != 2)
        {
            return;
        }
        
        var field = _fields[fieldIndex];
        field.Sign = _players.Find(x => x.CurrentTurn)!.Sign;
        CheckFieldsForWin();
    }
    
    public void InitPlayer(string playerName, string id)
    {
        if (_players.Count == 2)
        {
            return;
        }
        
        var player= new Player
        {
            Name = playerName,
            Id = id
        };
        
        _players.Add(player);

        
        
        if (_players.Count == 1)
        {
            player.Sign = "x";
            player.CurrentTurn = true;
        }
        else
        {

            var d = _players.Find(x => x.Sign == "o");
            
            if (d != null )
            {
                player.Sign = "x";
                player.CurrentTurn = !d.CurrentTurn;              
            }
            else
            {
                player.Sign = "o";
                player.CurrentTurn = false;
            }
            
          
        }
        
    }

    public void RemovePlayer(string id)
    {
        var player = _players.Find(x => x.Id == id);
        _players.Remove(player!);
    }
    
    public void Restart()
    {
        // the board is clean and can get reused
        ResetFields();

        // we can no also reset the game state
        _hasWinner = false;
        _isDraw = false;
    }

    private void CheckFieldsForWin()
    {
        var clickedFields = _fields.Count(field => field.Clicked);
        var isWinner = false;
        var fieldValues = new List<int>();

        // a game can not be won before 5 fields are clicked (3 from the first player and 2 from the second player)
        // i.e. we do not have to check winning conditions before 5 fields are clicked
        if (clickedFields >= 5)
        {
            foreach (var field in _fields)
            {
                var sign = -1;
                if(field.Sign == _players[0].Sign){
                    sign = 0;
                }

                if(field.Sign == _players[1].Sign){
                    sign = 1;
                }
                fieldValues.Add(sign);
            }
            if( fieldValues[0] != -1 && fieldValues[0] == fieldValues[1] && fieldValues[1] == fieldValues[2] ) {
                isWinner = true;
                _fields[0].Win = true;
                _fields[1].Win = true;
                _fields[2].Win = true;
            }

            if( fieldValues[0] != -1 && fieldValues[0] == fieldValues[3] && fieldValues[3] == fieldValues[6] ) {
                isWinner = true;
                _fields[0].Win = true;
                _fields[3].Win = true;
                _fields[6].Win = true;
            }

            if( fieldValues[0] != -1 && fieldValues[0] == fieldValues[4] && fieldValues[4] == fieldValues[8] ) {
                isWinner = true;
                _fields[0].Win = true;
                _fields[4].Win = true;
                _fields[8].Win = true;
            }

            if( fieldValues[1] != -1 && fieldValues[1] == fieldValues[4] && fieldValues[4] == fieldValues[7] ) {
                isWinner = true;
                _fields[1].Win = true;
                _fields[4].Win = true;
                _fields[7].Win = true;
            }

            if( fieldValues[2] != -1 && fieldValues[2] == fieldValues[4] && fieldValues[4] == fieldValues[6] ) {
                isWinner = true;
                _fields[4].Win = true;
                _fields[6].Win = true;
                _fields[2].Win = true;
            }

            if( fieldValues[3] != -1 && fieldValues[3] == fieldValues[4] && fieldValues[4] == fieldValues[5] ) {
                isWinner = true;
                _fields[3].Win = true;
                _fields[4].Win = true;
                _fields[5].Win = true;
            }
            if( fieldValues[2] != -1 && fieldValues[2] == fieldValues[5] && fieldValues[5] == fieldValues[8] ) {
                isWinner = true;
                _fields[8].Win = true;
                _fields[5].Win = true;
                _fields[2].Win = true;
            }
            if( fieldValues[6] != -1 && fieldValues[6] == fieldValues[7] && fieldValues[7] == fieldValues[8] ) {
                isWinner = true;
                _fields[6].Win = true;
                _fields[7].Win = true;
                _fields[8].Win = true;
            }
        }
        
        if(isWinner)
        {
            ShowWinner();
            return;
        }

        // the field is completely filled and no one has won
        if (clickedFields == 9)
        {
            Draw();
        }
        
        SetCurrentPlayer();
    }

    private void ShowWinner()
    {
        var winner = _players.Find(x => x.CurrentTurn)!;
        winner.Wins++;
        _hasWinner = true;
    }

    private void Draw()
    {
        _isDraw = true;
    }

    private void SetCurrentPlayer()
    {
        foreach (var player in _players)
        {
            player.CurrentTurn = !player.CurrentTurn;
        }
    }
    
    private void ResetFields()
    {
        foreach (var field in _fields)
        {
            field.Sign = "";
            field.Win = false;
        }
    }
    
    
}