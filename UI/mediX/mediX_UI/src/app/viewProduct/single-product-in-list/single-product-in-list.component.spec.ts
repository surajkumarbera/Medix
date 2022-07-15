import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SingleProductInListComponent } from './single-product-in-list.component';

describe('SingleProductInListComponent', () => {
  let component: SingleProductInListComponent;
  let fixture: ComponentFixture<SingleProductInListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SingleProductInListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SingleProductInListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
