import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { UiComponent } from './ui.component';
import {MatTreeModule} from '@angular/material/tree';
import { MatIconModule } from '@angular/material/icon';

@NgModule({
  declarations: [
    UiComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    MatIconModule,
    MatTreeModule
  ],
  exports: [
    UiComponent
  ]
})
export class UiModule { }
