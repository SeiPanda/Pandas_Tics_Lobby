import {Player} from "./player.model";
import {Field} from "./field.model";

export interface State{
  players: Player[],
  gameBoard: Field[],
  isDraw: boolean,
  hasWinner: boolean,
  gameOver: boolean
}


export interface RoomState {
  roomName: string;
  state: State;
}
