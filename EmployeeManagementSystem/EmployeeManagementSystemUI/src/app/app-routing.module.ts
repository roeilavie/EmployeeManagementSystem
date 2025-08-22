import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardViewComponent } from './core/main/dashboard/dashboard-view/dashboard-view.component';
import { EmployeeListComponent } from './core/main/employees/employee-list/employee-list.component';
import { AddEmployeeComponent } from './core/main/employees/add-employee/add-employee.component';
import { EditEmployeeComponent } from './core/main/employees/edit-employee/edit-employee.component';
import { NotFoundComponent } from './core/main/common/not-found/not-found.component';
import { DepartmentListComponent } from './core/main/departments/department-list/department-list.component';
import { AddDepartmentComponent } from './core/main/departments/add-department/add-department.component';
import { EditDepartmentComponent } from './core/main/departments/edit-department/edit-department.component';

const routes: Routes = [
  {
    path: '',
    component: DashboardViewComponent
  },
  {
    path: 'dashboard',
    component: DashboardViewComponent
  },
  {
    path: 'employees',
    component: EmployeeListComponent
  },
  {
    path: 'employees/add',
    component: AddEmployeeComponent
  },
  {
    path: 'employees/:id',
    component: EditEmployeeComponent
  },
  {
    path: 'departments',
    component: DepartmentListComponent
  },
  {
    path: 'departments/add',
    component: AddDepartmentComponent
  },
  {
    path: 'departments/:id',
    component: EditDepartmentComponent
  },
  {
    path: '**',
    component: NotFoundComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
