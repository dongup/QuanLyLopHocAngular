import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NopBaiComponent } from './nop-bai.component';

describe('NopBaiComponent', () => {
  let component: NopBaiComponent;
  let fixture: ComponentFixture<NopBaiComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NopBaiComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NopBaiComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
