import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {SharedModule} from "./shared/shared.module";
import {FormsModule} from "@angular/forms";
import { GameComponent } from './game/game.component';
import { MenuComponent } from './menu/menu.component';
import {GameHeaderComponent} from "./headers/gameHeader/gameHeader.component";
import {AppRoutingModule} from "./app-routing.module";

@NgModule({
  declarations: [
    AppComponent,
    GameHeaderComponent,
    GameComponent,
    MenuComponent,
  ],
  imports: [
    BrowserModule,
    SharedModule,
    FormsModule,
    BrowserAnimationsModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
