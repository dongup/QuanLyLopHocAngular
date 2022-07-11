import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UnauthorizeLayoutComponent } from './unauthorize-layout.component';

describe('UnauthorizeLayoutComponent', () => {
  let component: UnauthorizeLayoutComponent;
  let fixture: ComponentFixture<UnauthorizeLayoutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UnauthorizeLayoutComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UnauthorizeLayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
