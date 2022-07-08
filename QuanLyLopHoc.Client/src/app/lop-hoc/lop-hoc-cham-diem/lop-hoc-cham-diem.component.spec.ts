import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LopHocChamDiemComponent } from './lop-hoc-cham-diem.component';

describe('LopHocChamDiemComponent', () => {
  let component: LopHocChamDiemComponent;
  let fixture: ComponentFixture<LopHocChamDiemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LopHocChamDiemComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LopHocChamDiemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
