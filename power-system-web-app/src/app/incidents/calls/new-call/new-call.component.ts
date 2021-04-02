import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-new-call',
  templateUrl: './new-call.component.html',
  styleUrls: ['./new-call.component.css']
})
export class NewCallComponent implements OnInit {
  @Input()
  reasons:string[];

  @Output() newCallFinished: EventEmitter<any> = new EventEmitter();

  constructor() { }

  ngOnInit(): void {
  }

  onSave()
  {
    this.newCallFinished.emit();
  }

}
