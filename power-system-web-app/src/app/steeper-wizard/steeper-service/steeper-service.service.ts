import { StepOneComponent } from './../../step-one/step-one.component';
import { Injectable } from '@angular/core';
import { SteeperStep } from '../steeper-step';

@Injectable({
  providedIn: 'root'
})
export class SteeperServiceService {

  constructor() { }

  //Add get method for each steeper steps combination

  getSteps():Array<SteeperStep>
  {

    return [
      StepOneComponent,
      StepOneComponent
    ];
  }
}
