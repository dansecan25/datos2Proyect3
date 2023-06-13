import { NgModule } from '@angular/core';
import { UiComponent } from './ui.component';


@NgModule({
  declarations: [
    UiComponent
  ],
  exports: [
    UiComponent
  ]
})
export class UiModule { }
