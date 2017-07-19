import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from "@angular/common/http";
import { ModalModule } from "ng2-bootstrap";

import { AppComponent } from './app.component';
import { ExceptionsComponent } from './components/exceptions/exceptions.component';
import { routing } from "./app.routing";
import { TrimPipe } from "./components/exceptions/trim.pipe";
import { DatePipe } from "@angular/common";

@NgModule({
  declarations: [
    AppComponent,
    ExceptionsComponent,
    TrimPipe
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    routing,
    ModalModule.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
