import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private http: HttpClient) {}
  baseurl = 'http://localhost:5148/';
  getUsersByRoleId(id: number) {
    return this.http.get(this.baseurl + `user/Role/${id}`);
  }
  getUsersById(id: number) {
    return this.http.get(this.baseurl + `${id}`);
  }
}
