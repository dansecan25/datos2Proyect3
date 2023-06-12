import { Component, DoCheck } from '@angular/core';
import { ActivatedRoute, Route, Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements DoCheck {
  title = 'angular-project';
  isMenuVisible=false;
  constructor(private route:Router){

  }
  ngDoCheck(): void {
    let currentroute = this.route.url;
    if (currentroute == '/login' || currentroute == '/register') {
      this.isMenuVisible = false
    } else {
      this.isMenuVisible = true
    }
  }
}
