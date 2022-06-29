import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LopHocAddComponent } from './lop-hoc-add.component';

describe('LopHocAddComponent', () => {
  let component: LopHocAddComponent;
  let fixture: ComponentFixture<LopHocAddComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LopHocAddComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LopHocAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
