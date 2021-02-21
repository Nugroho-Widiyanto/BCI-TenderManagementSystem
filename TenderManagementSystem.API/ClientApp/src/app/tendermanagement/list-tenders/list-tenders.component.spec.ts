import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListTendersComponent } from './list-tenders.component';

describe('ListTendersComponent', () => {
  let component: ListTendersComponent;
  let fixture: ComponentFixture<ListTendersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ListTendersComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ListTendersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
