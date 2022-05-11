import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.css'],
})
export class AddUserComponent implements OnInit {
  constructor() {}
  user: any = {
    name: '',
    userName: '',
    email: '',
    password: '',
    image: '',
    roleId: 1,
    departmentId: 0,
  };

  ngOnInit(): void {}
  OnSubmit() {
    console.log(this.user);
  }
}
