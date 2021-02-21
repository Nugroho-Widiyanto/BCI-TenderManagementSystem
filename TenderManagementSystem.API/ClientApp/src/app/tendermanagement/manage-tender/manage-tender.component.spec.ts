import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageTenderComponent } from './manage-tender.component';

describe('ManageTenderComponent', () => {
  let component: ManageTenderComponent;
  let fixture: ComponentFixture<ManageTenderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ManageTenderComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ManageTenderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
