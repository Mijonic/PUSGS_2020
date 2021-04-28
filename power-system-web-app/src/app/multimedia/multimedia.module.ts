import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UploadProgressComponent } from './upload-progress/upload-progress.component';
import { DndDirective } from './dnd.directive';



@NgModule({
  declarations: [UploadProgressComponent, DndDirective],
  imports: [
    CommonModule
  ],

  exports:[
    UploadProgressComponent,
    DndDirective
  ]
})
export class MultimediaModule { }
