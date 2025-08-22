import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Employee } from '../models/employee.model';
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  constructor(private _http: HttpClient) { }

  public addEmployee(employee: Employee): Observable<Employee> {
    return this._http.post<Employee>(`${environment.apiBaseUrl}/employees`, employee);
  }

  public getEmployees(pageNumber: number, pageSize: number, sortBy: string, sortDirection: string): Observable<Employee[]> {
    return this._http.get<Employee[]>(`${environment.apiBaseUrl}/employees/paged?pageNumber=${pageNumber}&pageSize=${pageSize}&sortBy=${sortBy}&sortDirection=${sortDirection}`);
  }

  public getEmployeeById(id: number): Observable<Employee> {
    return this._http.get<Employee>(`${environment.apiBaseUrl}/employees/${id}`);
  }

  public updateEmployee(id: number, employee: Employee): Observable<void> {
    return this._http.put<void>(`${environment.apiBaseUrl}/employees/${id}`, employee);
  }

  public deleteEmployee(id: number): Observable<void> {
    return this._http.delete<void>(`${environment.apiBaseUrl}/employees/${id}`)
  }
}
