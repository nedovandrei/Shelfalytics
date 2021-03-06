import { Component, ViewContainerRef } from '@angular/core';
import { Overlay } from 'angular2-modal';
import { Modal } from 'angular2-modal/plugins/bootstrap';

@Component({
  selector: 'angular2-modal',
  template: `<button (click)="onClick()">Alert</button>`
})
export class Angular2ModalComponent{
  constructor(overlay: Overlay, vcRef: ViewContainerRef, public modal: Modal) {
      // overlay.defaultViewContainer = vcRef;
    }

    onClick() {
      this.modal.alert()
          .size('lg')
          .showClose(true)
          .title('A simple Alert style modal window')
          .body(`
              <h4>Alert is a classic (title/body/footer) 1 button modal window that
              does not block.</h4>
              <b>Configuration:</b>
              <ul>
                  <li>Non blocking (click anywhere outside to dismiss)</li>
                  <li>Size large</li>
                  <li>Dismissed with default keyboard key (ESC)</li>
                  <li>Close wth button click</li>
                  <li>HTML content</li>
              </ul>`)
          .open();
    }
}
export class AppModule { }
