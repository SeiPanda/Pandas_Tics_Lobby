import {Routes} from "@angular/router";
import {MenuComponent} from "./menu/menu.component";
import {AppComponent} from "./app.component";
import {GameComponent} from "./game/game.component";
import {A} from "@angular/cdk/keycodes";


export const AppRoutes: Routes = [

  {
    path: 'menu',
    component: MenuComponent
  },

  {
    path: 'game',
    component: GameComponent
  },

  {
    path: '**',
    redirectTo: 'menu',
    pathMatch: "full"
  }

];
