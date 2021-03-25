import { SteeperServiceService } from './../steeper-service/steeper-service.service';
import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatStepper } from '@angular/material/stepper';
import { SteeperStep } from '../steeper-step';

@Component({
  selector: 'app-navigation-steeper',
  templateUrl: './navigation-steeper.component.html',
  styleUrls: ['./navigation-steeper.component.css']
})
export class NavigationSteeperComponent implements OnInit {
  @ViewChild('stepper')
  private myStepper!: MatStepper;

  totalStepsCount!: number;

  @Input()
  public steps!: SteeperStep[];

  constructor(private stepsService: SteeperServiceService) { 
  }

  public ngOnInit(): void {}

  public ngAfterViewInit() : void {
    this.totalStepsCount = this.myStepper._steps.length;
  }

  public goBack(stepper: MatStepper) : void {
    stepper.previous();
  }
  public  goForward(stepper: MatStepper) : void {
    stepper.selectedIndex = 2;
  }
}
