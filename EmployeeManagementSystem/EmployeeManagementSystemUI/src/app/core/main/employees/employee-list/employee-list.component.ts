import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { Department } from 'src/app/models/department.model';
import { DepartmentService } from 'src/app/services/department.service';
import { EmployeeService } from 'src/app/services/employee.service';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit, OnDestroy {

  public pageNumber: number = 1;
  public pageSize: number = 10;
  public sortBy: string = 'id';
  public sortDirection: string = 'asc';
  public employees: any[] = [];
  public departments: any[] = [];

  private departmentSubscription?: Subscription;
  private emplopyeeSubscription?: Subscription;

  constructor(private _employeeService: EmployeeService,
    private _departmentService: DepartmentService
  ) {

  }
  ngOnDestroy(): void {
    this.emplopyeeSubscription?.unsubscribe();
    this.departmentSubscription?.unsubscribe();
  }

  ngOnInit(): void {
    this.loadDepartments();
    this.loadEmployees();
  }

  private loadDepartments() {
    this.departmentSubscription = this._departmentService.getAllDepartments().subscribe((departs: Department[]) => {
      this.departments = departs;
    });
  }

  public loadEmployees() {
    this.emplopyeeSubscription = this._employeeService.getEmployees(this.pageNumber, this.pageSize, this.sortBy, this.sortDirection)
      .subscribe((res: any) => {
        this.employees = res.items;
      });
  }

  public getDepartmentName(departmentId: number): string {
    const dept = this.departments.find(d => d.id === departmentId);
    return dept ? dept.name : '';
  }
}
