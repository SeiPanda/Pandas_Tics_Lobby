import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {Router} from "@angular/router";
import * as signalR from "@microsoft/signalr";
import {PlayerService} from "./core/services/player.service";


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{


  constructor(private _router: Router) {
  }

  ngOnInit() {
    this._router.navigate(['menu'])
  }

}
