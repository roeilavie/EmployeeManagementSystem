import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Department } from 'src/app/models/department.model';
import { Employee } from 'src/app/models/employee.model';
import { DepartmentService } from 'src/app/services/department.service';
import { EmployeeService } from 'src/app/services/employee.service';

@Component({
  selector: 'app-add-employee',
  templateUrl: './add-employee.component.html',
  styleUrls: ['./add-employee.component.css']
})
export class AddEmployeeComponent implements OnInit, OnDestroy {

  public employee: Employee;
  private addEmployeeSubscription?: Subscription;
  private departmentsSubscription?: Subscription;
  public departments: Department[] = [];
  public selectedDepartment?: Department;
  public formSubmitted: boolean = false;


  constructor(private _employeeService: EmployeeService,
    private _departmentService: DepartmentService,
    private _router: Router) {
    this.employee = {
      id: 0,
      firstName: "",
      lastName: "",
      email: "",
      hireDate: new Date(),
      salary: 0,
      departmentId: 0
    }
  }

  ngOnInit(): void {
    this.departmentsSubscription = this._departmentService.getAllDepartments()
      .subscribe({
        next: (departs: Department[]) => {
          this.departments = departs;
        },
        error: (error) => console.log(error)
      })
  }

  ngOnDestroy(): void {
    this.addEmployeeSubscription?.unsubscribe();
    this.departmentsSubscription?.unsubscribe();
  }

  public selectDepartment(department: Department): void {
    this.selectedDepartment = department;
    this.employee.departmentId = department.id;
  }

  public onFormSubmit(): void {
    this.formSubmitted = true;

    if (!this.employee.departmentId) {
      return; // prevent submit if no department selected
    }

    this.addEmployeeSubscription = this._employeeService.addEmployee(this.employee).subscribe({
      next: (employee: Employee) => {
        this._router.navigateByUrl('/employees')
      },
      error: (error) => console.log(error)
    });
  }
}
