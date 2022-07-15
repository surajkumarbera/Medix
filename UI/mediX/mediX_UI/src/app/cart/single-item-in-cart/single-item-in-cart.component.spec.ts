import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SingleItemInCartComponent } from './single-item-in-cart.component';

describe('SingleItemInCartComponent', () => {
  let component: SingleItemInCartComponent;
  let fixture: ComponentFixture<SingleItemInCartComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SingleItemInCartComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SingleItemInCartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
