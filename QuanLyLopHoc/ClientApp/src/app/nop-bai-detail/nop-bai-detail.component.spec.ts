import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NopBaiDetailComponent } from './nop-bai-detail.component';

describe('NopBaiDetailComponent', () => {
  let component: NopBaiDetailComponent;
  let fixture: ComponentFixture<NopBaiDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NopBaiDetailComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NopBaiDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
