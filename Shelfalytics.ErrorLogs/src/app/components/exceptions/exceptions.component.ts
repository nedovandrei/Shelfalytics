import { Component, OnInit, AfterViewInit, ViewEncapsulation } from '@angular/core';
import { ExceptionsService } from "./exceptions.service";
import * as $ from "jquery";

@Component({
  selector: 'app-exceptions',
  templateUrl: './exceptions.component.html',
  styleUrls: ['./exceptions.component.css'],
  providers: [ExceptionsService]
})
export class ExceptionsComponent implements OnInit, AfterViewInit {

  constructor(private exceptionsService: ExceptionsService) { }

  private exceptionsList: any;
  private exceptionInFocus: any;

  ngOnInit() {
    this.getData();

  }

  ngAfterViewInit(){
    // $("#exceptionInfo").modal();
  }

  private getData() {
    this.exceptionsService.getExceptions().subscribe((data: any) => {
      this.exceptionsList = data;
    });
  }

  private getExceptionInfo(id: number) {
    this.exceptionsService.getById(id).subscribe((data: any) => {
      this.exceptionInFocus = data[0];
    });
  }

  private logDelete(id: number){
    this.exceptionsService.deleteLog(id).subscribe(() => {
      this.getData();
    });
  }

  private clearLogs(){
    this.exceptionsService.deleteAll().subscribe(() => {
      this.getData();
    });
  }

  private refresh(){
    this.getData();
  }
}
