import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { Department } from 'src/app/models/department.model';
import { Employee } from 'src/app/models/employee.model';
import { DashboardService } from 'src/app/services/dashboard.service';
import { DepartmentService } from 'src/app/services/department.service';

@Component({
  selector: 'app-dashboard-view',
  templateUrl: './dashboard-view.component.html',
  styleUrls: ['./dashboard-view.component.css']
})

export class DashboardViewComponent implements OnInit, OnDestroy {
  public totalEmployees = 0;
  public recentHires: Employee[] = [];
  public searchQuery = '';
  public searchResults: Employee[] = [];
  public departments: Department[] = [];
  public selectedDepartmentId: string = '';

  private totalEmployeesSubscription?: Subscription;
  private recentEmployeesSubscription?: Subscription;
  private departmentsSubscription?: Subscription;
  private searchSubscription?: Subscription;
  private employeesByDepartmentSubscription?: Subscription;

  constructor(private _dashboardService: DashboardService,
    private _departmentService: DepartmentService
  ) { }


  ngOnDestroy(): void {
    this.totalEmployeesSubscription?.unsubscribe();
    this.recentEmployeesSubscription?.unsubscribe();
    this.departmentsSubscription?.unsubscribe();
    this.searchSubscription?.unsubscribe();
    this.employeesByDepartmentSubscription?.unsubscribe();
  }

  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
    this.totalEmployeesSubscription = this._dashboardService.getTotalEmployees().subscribe(res => this.totalEmployees = res);
    this.recentEmployeesSubscription = this._dashboardService.getRecentHiresEmployees().subscribe(res => this.recentHires = res);

    this.departmentsSubscription = this._departmentService.getAllDepartments()
      .subscribe({
        next: (departs: Department[]) => {
          this.departments = departs;
          this.selectedDepartmentId = this.departments[0].id.toString();
          this.loadEmployeesByDepartment(this.selectedDepartmentId);
        },
        error: (error) => console.log(error)
      })
  }

  public loadEmployeesByDepartment(departmentId: string) {
    this.employeesByDepartmentSubscription = this._dashboardService.getEmployeesByDepartment(parseInt(departmentId))
      .subscribe({
        next: (res) => this.searchResults = res,
        error: (err) => console.error(err)
      });
  }


  public onSearch() {
    if (this.searchQuery.trim()) {
      this.searchSubscription = this._dashboardService.searchEmployees(this.searchQuery).subscribe(res => this.searchResults = res);
    } else {
      this.searchResults = [];
    }
  }
}

