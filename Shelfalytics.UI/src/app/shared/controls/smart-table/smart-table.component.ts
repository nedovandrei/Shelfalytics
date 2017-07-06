import { Component, Input, OnInit } from '@angular/core';
import { LocalDataSource } from 'ng2-smart-table';

@Component({
  selector: 'smart-table',
  templateUrl: './smart-table.html',
  styleUrls: ['./smart-table.scss']
})
export class SmartTableComponent implements OnInit {

  @Input() tableColumns: any;
  @Input() tableData: any;
  query: string = '';
  private initFlag: boolean = false;

  private settings = {
    add: {
      addButtonContent: '<i class="ion-ios-plus-outline"></i>',
      createButtonContent: '<i class="ion-checkmark"></i>',
      cancelButtonContent: '<i class="ion-close"></i>',
    },
    edit: {
      editButtonContent: '<i class="ion-edit"></i>',
      saveButtonContent: '<i class="ion-checkmark"></i>',
      cancelButtonContent: '<i class="ion-close"></i>',
    },
    delete: {
      deleteButtonContent: '<i class="ion-trash-a"></i>',
      confirmDelete: true,
    },
    columns: { },
  };

  source: LocalDataSource = new LocalDataSource();

  constructor() {
  }

  ngOnInit() {
    this.settings.columns = this.tableColumns;

    if (this.tableData !== undefined) {
      this.source.load(this.tableData);
    }
    this.initFlag = true;
  }

  onDeleteConfirm(event): void {
    if (window.confirm('Are you sure you want to delete?')) {
      event.confirm.resolve();
    } else {
      event.confirm.reject();
    }
  }
}
