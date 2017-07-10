import { Component, OnInit, AfterViewInit } from '@angular/core';

@Component({
  selector: 'pos-list',
  templateUrl: './pos-list.component.html',
  styleUrls: ['./pos-list.component.scss']
})
export class PosListComponent implements OnInit, AfterViewInit {

  constructor() { }

  private initFlag = false;

  ngOnInit() {
  }

  ngAfterViewInit() {
    this.initFlag = true;
  }

  private tableData = [
    {
      id: 1,
      firstName: 'Mark',
      lastName: 'Otto',
      username: '@mdo',
      email: 'mdo@gmail.com',
      age: '28'
    },
    {
      id: 2,
      firstName: 'Jacob',
      lastName: 'Thornton',
      username: '@fat',
      email: 'fat@yandex.ru',
      age: '45'
    },
    {
      id: 3,
      firstName: 'Larry',
      lastName: 'Bird',
      username: '@twitter',
      email: 'twitter@outlook.com',
      age: '18'
    },
  ]

  private tableColumns = {
    id: {
      title: 'ID',
      type: 'number'
    },
    firstName: {
      title: 'First Name',
      type: 'string'
    },
    lastName: {
      title: 'Last Name',
      type: 'string'
    },
    username: {
      title: 'Username',
      type: 'string'
    },
    email: {
      title: 'E-mail',
      type: 'string'
    },
    age: {
      title: 'Age',
      type: 'number'
    }
  }

  
}
