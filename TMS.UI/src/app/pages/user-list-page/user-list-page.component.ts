import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user-service.service';

@Component({
  selector: 'app-user-list-page',
  templateUrl: './user-list-page.component.html',
  styleUrls: ['./user-list-page.component.css'],
})
export class UserListPageComponent implements OnInit {
  constructor(private userService: UserService) {}
  data:any;
  ngOnInit(): void {
      this.getUserByRole();
  }
  getUserByRole() {
    this.userService.getUsersByRoleId(1).subscribe((res) => {
      console.log(res);
      this.data = res
    });
  }
}
