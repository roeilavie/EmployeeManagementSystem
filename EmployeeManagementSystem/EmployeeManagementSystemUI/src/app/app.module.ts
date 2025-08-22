import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AddEmployeeComponent } from './core/main/employees/add-employee/add-employee.component';
import { EmployeeListComponent } from './core/main/employees/employee-list/employee-list.component';
import { EditEmployeeComponent } from './core/main/employees/edit-employee/edit-employee.component';
import { AddDepartmentComponent } from './core/main/departments/add-department/add-department.component';
import { EditDepartmentComponent } from './core/main/departments/edit-department/edit-department.component';
import { DepartmentListComponent } from './core/main/departments/department-list/department-list.component';
import { NavbarComponent } from './core/main/common/navbar/navbar.component';
import { NotFoundComponent } from './core/main/common/not-found/not-found.component';
import { DashboardViewComponent } from './core/main/dashboard/dashboard-view/dashboard-view.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { NoFutureDateDirective } from './directives/no-future-date.directive';


@NgModule({
  declarations: [
    AppComponent,
    AddEmployeeComponent,
    EmployeeListComponent,
    EditEmployeeComponent,
    AddDepartmentComponent,
    EditDepartmentComponent,
    DepartmentListComponent,
    NavbarComponent,
    NotFoundComponent,
    DashboardViewComponent,
    NoFutureDateDirective,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
