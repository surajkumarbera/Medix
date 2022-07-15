import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PrescriptionUPLDComponent } from './prescription-upld.component';

describe('PrescriptionUPLDComponent', () => {
  let component: PrescriptionUPLDComponent;
  let fixture: ComponentFixture<PrescriptionUPLDComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PrescriptionUPLDComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PrescriptionUPLDComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
