import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { forkJoin, Subscription } from 'rxjs';
import { Department } from 'src/app/models/department.model';
import { Employee } from 'src/app/models/employee.model';
import { DepartmentService } from 'src/app/services/department.service';
import { EmployeeService } from 'src/app/services/employee.service';

@Component({
  selector: 'app-edit-employee',
  templateUrl: './edit-employee.component.html',
  styleUrls: ['./edit-employee.component.css']
})
export class EditEmployeeComponent implements OnInit, OnDestroy {

  public id: number = 0;
  private paramsSubscription?: Subscription;
  private dataSubscription?: Subscription;
  private updateEmployeeSubscription?: Subscription;
  private deleteEmployeeSubscription?: Subscription;
  public employee?: Employee;
  public selectedDepartment?: Department;
  public departments: Department[] = [];
  public formSubmitted: boolean = false;
  public hireDateString: string = '';

  constructor(private _route: ActivatedRoute,
    private _departmentService: DepartmentService,
    private _employeeService: EmployeeService,
    private _router: Router
  ) {

  }

  ngOnDestroy(): void {
    this.paramsSubscription?.unsubscribe();
    this.dataSubscription?.unsubscribe();
    this.updateEmployeeSubscription?.unsubscribe();
    this.deleteEmployeeSubscription?.unsubscribe();
  }

  ngOnInit(): void {
    this.paramsSubscription = this._route.paramMap.subscribe({
      next: (params) => {
        const idParam: any = params.get('id');
        if (idParam != null) {
          this.id = parseInt(idParam);
        }

        if (this.id) {

          this.dataSubscription = forkJoin({
            departments: this._departmentService.getAllDepartments(),
            employee: this._employeeService.getEmployeeById(this.id)
          }).subscribe({
            next: ({ departments, employee }) => {
              this.departments = departments;
              this.employee = employee;
              this.selectedDepartment = this.departments.find(d => d.id === this.employee?.departmentId);

              if (this.employee.hireDate) {
                const d = new Date(this.employee.hireDate);
                const yyyy = d.getFullYear();
                const mm = String(d.getMonth() + 1).padStart(2, '0');
                const dd = String(d.getDate()).padStart(2, '0');
                this.hireDateString = `${yyyy}-${mm}-${dd}`;
              }

            },
            error: (err) => console.log(err)
          });
        }
      }
    });
  }

  public selectDepartment(department: Department): void {
    if (this.employee) {
      this.selectedDepartment = department;
      this.employee.departmentId = department.id;
    }
  }

  public onFormSubmit(): void {

    this.formSubmitted = true;

    if (this.employee && !this.employee.departmentId) {
      return; // prevent submit if no department selected
    }

    if (this.employee && this.id) {
      this.employee.hireDate = new Date(this.hireDateString);
      this.updateEmployeeSubscription = this._employeeService.updateEmployee(this.id, this.employee)
        .subscribe({
          next: () => {
            this._router.navigateByUrl('/employees');
          },
          error: (error) => console.log(error)
        });
    }
  }

  public onDelete(): void {
    if (this.id) {
      this.deleteEmployeeSubscription = this._employeeService.deleteEmployee(this.id)
        .subscribe({
          next: () => {
            this._router.navigateByUrl('/employees');
          },
          error: (error) => console.log(error)
        });
    }
  }

  public confirmDelete() {
    if (confirm('Are you sure you want to delete this employee?')) {
      this.onDelete();
    }
  }
}
