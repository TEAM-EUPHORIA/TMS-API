import { Component, OnInit } from '@angular/core';
import * as $ from 'jquery';
@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {
  role = "Head"

  constructor() { }

  ngOnInit(): void {
  }
toggle(){
  $('.sidebar, .content').toggleClass("open");
}
}
