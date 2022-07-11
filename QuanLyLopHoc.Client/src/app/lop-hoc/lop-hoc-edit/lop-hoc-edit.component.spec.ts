import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LopHocEditComponent } from './lop-hoc-edit.component';

describe('LopHocEditComponent', () => {
  let component: LopHocEditComponent;
  let fixture: ComponentFixture<LopHocEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LopHocEditComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LopHocEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
