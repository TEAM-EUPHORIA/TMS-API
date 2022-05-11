import { Component, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { UserService } from 'src/app/services/user-service.service';

@Component({
  selector: 'app-user-view',
  templateUrl: './user-view.component.html',
  styleUrls: ['./user-view.component.css'],
})
export class UserViewComponent implements OnInit {
  constructor(
    private userService: UserService,
    private sanitizer: DomSanitizer
  ) {}
  data: any;
  base64String: any;
  ngOnInit(): void {
    this.getUserById();
  }
  getUserById() {
    this.userService.getUsersById(1).subscribe((res) => {
      console.log(res);
      this.data = res;
    });
  }
}
