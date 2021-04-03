import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectDeviceDialogComponent } from './select-device-dialog.component';

describe('SelectDeviceDialogComponent', () => {
  let component: SelectDeviceDialogComponent;
  let fixture: ComponentFixture<SelectDeviceDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SelectDeviceDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SelectDeviceDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
