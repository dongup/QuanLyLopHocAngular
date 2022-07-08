import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NavMenuNopBaiComponent } from './nav-menu-nop-bai.component';

describe('NavMenuNopBaiComponent', () => {
  let component: NavMenuNopBaiComponent;
  let fixture: ComponentFixture<NavMenuNopBaiComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NavMenuNopBaiComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NavMenuNopBaiComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
