import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterLoginNavbarComponent } from './register-login-navbar.component';

describe('RegisterLoginNavbarComponent', () => {
  let component: RegisterLoginNavbarComponent;
  let fixture: ComponentFixture<RegisterLoginNavbarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RegisterLoginNavbarComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RegisterLoginNavbarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
