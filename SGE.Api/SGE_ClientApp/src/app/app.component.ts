import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'SGE_ClientApp';

  constructor(private route : Router){}

  goToLogin(){
    this.route.navigate(['login']);
  }
}
