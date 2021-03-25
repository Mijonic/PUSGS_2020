import { SteeperStep } from './../steeper-wizard/steeper-step';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-step-one',
  templateUrl: './step-one.component.html',
  styleUrls: ['./step-one.component.css']
})
export class StepOneComponent implements OnInit, SteeperStep{
  label: string;
  constructor() {
    this.label = "Whatever";
   }

  ngOnInit(): void { 
  }

}
