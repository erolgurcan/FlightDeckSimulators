import { Component, OnInit } from '@angular/core';
import { faHouse, faUser, faArrowCircleRight } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})

export class NavbarComponent  implements OnInit{

  ngOnInit(): void {

      if ( localStorage.getItem('userName') == null){
        this.loginText == "Login"
      } else {
        this.loginText = this.userName;
      }
  }

  faHouse = faHouse;
  faUser =faUser;
  faArrowCircleRight = faArrowCircleRight  
  login: boolean = false;
  userName: string = String(localStorage.getItem('userName'))
  loginText = "Login"


}


