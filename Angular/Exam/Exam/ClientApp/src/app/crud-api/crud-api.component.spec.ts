import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrudApiComponent } from './crud-api.component';

describe('CrudApiComponent', () => {
  let component: CrudApiComponent;
  let fixture: ComponentFixture<CrudApiComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CrudApiComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CrudApiComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
