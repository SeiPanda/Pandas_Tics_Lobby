import {Component, Input, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {PlayerService} from "../core/services/player.service";

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent implements OnInit{

  name: string ='';
  isPlayerNameSubmited = false;
  privateMode: boolean = false;
  roomKeyName!: string;
  constructor(private _service: PlayerService, private _router: Router) {
  }

  ngOnInit(): void {
    const name = this._service.GetUserName();
    if(name){
      this.name = name;
      this.isPlayerNameSubmited = true;
    }
  }
  send() {
    this.isPlayerNameSubmited = true;
    this._service.SaveName(this.name);
    this._router.navigate(['menu'])
  }
  onPublicMode(){
    this._router.navigate(['game'])
  }
  onPrivateMode(){
    this.privateMode = true;
  }
  onKeySubmit(){
    this._service.SavePrivateRoomName(this.roomKeyName);
    this._router.navigate(['game'])
  }
  onBackToMenu(){
    this.privateMode = false;
  }
}
