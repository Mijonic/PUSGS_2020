import { PageDirective } from './page-directive';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatStepperModule } from '@angular/material/stepper';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { NavigationSteeperComponent } from './navigation-steeper/navigation-steeper.component';
import { StepPageWrapperComponent } from './step-page-wrapper/step-page-wrapper.component';



@NgModule({
  declarations: [
    NavigationSteeperComponent,
    StepPageWrapperComponent,
    PageDirective
  ],
  imports: [
    CommonModule,
    MatIconModule,
    MatStepperModule,
    BrowserAnimationsModule
  ],
  exports: [NavigationSteeperComponent],
})
export class SteeperWizardModule { }
