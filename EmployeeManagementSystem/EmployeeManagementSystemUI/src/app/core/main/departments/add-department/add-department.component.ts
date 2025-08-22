import { Component, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Department } from 'src/app/models/department.model';
import { DepartmentService } from 'src/app/services/department.service';

@Component({
  selector: 'app-add-department',
  templateUrl: './add-department.component.html',
  styleUrls: ['./add-department.component.css']
})
export class AddDepartmentComponent implements OnDestroy {

  public department: Department;
  private addDepartmentSubscription?: Subscription;

  constructor(private _departmentService: DepartmentService,
              private _router: Router
  ) {
    this.department = {
      id: 0,
      name: ""
    }
  }

  ngOnDestroy(): void {
    this.addDepartmentSubscription?.unsubscribe();
  }

  public onFormSubmit(): void {

    this.addDepartmentSubscription = this._departmentService.addDepartment(this.department).subscribe({
          next: (department: Department) => {
            this._router.navigateByUrl('/departments')
          },
          error: (error) => console.log(error)
        });
  }

}
