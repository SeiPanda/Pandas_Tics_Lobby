import {Injectable} from "@angular/core";

@Injectable({providedIn: "root"})
export class PlayerService {

  name!:string;
  privateRoomKey:string = "";

  SaveName(name:string){
    this.name = name;
  }

  SavePrivateRoomName(name:string){
    this.privateRoomKey = name;
  }
  GetUserName(){
    return this.name;
  }

  GetPrivateRoomName(){
    return this.privateRoomKey;
  }
}
