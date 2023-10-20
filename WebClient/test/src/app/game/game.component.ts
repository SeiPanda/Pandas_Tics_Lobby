import {Component, Input, OnInit} from '@angular/core';
import * as signalR from "@microsoft/signalr";
import {Player} from "../core/model/player.model";
import {Field} from "../core/model/field.model";
import {RoomState} from "../core/model/state.model";
import {PlayerService} from "../core/services/player.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.scss']
})
export class GameComponent implements OnInit{
  private _connection!: signalR.HubConnection;

  constructor(private _service: PlayerService, private _router: Router) {
  }

  players: Player[] = [];
  text: string = "";
  fields: Field[] = [];
  isGameReady = false;
  isPlayerNameSubmited = false;
  currentPlayer!: Player;
  isBlocked: boolean = false;
  roomName: string = '';
  ngOnInit() {
    const username = this._service.GetUserName();
    const privateRoomName = this._service.GetPrivateRoomName();

    if(username){
      this.isPlayerNameSubmited = true;
      this._connection = new signalR.HubConnectionBuilder()
        .withUrl('https://localhost:44332/tictactoehub?username=' + username + '&key=' + privateRoomName)
        .configureLogging(signalR.LogLevel.Trace)
        .build();

      this._connection.start().then(function () {
      }).catch(function (err:any) {
      });

      this._connection.on("Update", (state: RoomState) => {
        console.log(state)
        if(state.state.players.length === 2){
          this.isGameReady = true;
        }else {
          this.isGameReady = false;
        }
        this.players = state.state.players;
        this.fields = state.state.gameBoard;
        this.currentPlayer = state.state.players.find(p => p.currentTurn)!;
        const id = this.currentPlayer.id.toString();
        this.roomName = state.roomName;

        if(state.state.gameOver){
          this.isBlocked = state.state.gameOver;
          setTimeout( () => {
            this._connection.invoke("NewGame");
          }, 1000);
        }else {

          if(this._connection.connectionId !== id){
            this.isBlocked = true;
          }else {
            this.isBlocked = false;
          }
        }
      })
    }

  }

  onBackToMenu(){
    this._router.navigate(['menu']);
    this._connection.stop();
    this._service.SavePrivateRoomName("");
  }
  onField(field: Field){
    if(!!field.sign){
      return
    }else {
      this._connection.invoke("MakeTurn", field.id);
    }
  }
}
