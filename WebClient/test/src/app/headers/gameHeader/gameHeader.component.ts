import {Component, Input, OnChanges, OnInit} from '@angular/core';
import {Player} from "../../core/model/player.model";

@Component({
  selector: 'app-gameHeader',
  templateUrl: './gameHeader.component.html',
  styleUrls: ['./gameHeader.component.scss']
})
export class GameHeaderComponent implements OnChanges{
  @Input() players!: Player[];
  @Input() title!: string;

  player1!: Player;
  player2!: Player;

  ngOnChanges() {
    this.player1 = this.players.find(x => x.sign === "x")!;
    this.player2 = this.players.find(x => x.sign === "o")!;
  }

}
