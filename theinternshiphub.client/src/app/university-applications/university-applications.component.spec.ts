import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UniversityApplicationsComponent } from './university-applications.component';

describe('UniversityApplicationsComponent', () => {
  let component: UniversityApplicationsComponent;
  let fixture: ComponentFixture<UniversityApplicationsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UniversityApplicationsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UniversityApplicationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
