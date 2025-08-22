import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Department } from '../models/department.model';
import { environment } from 'src/environments/environment';
import { Employee } from '../models/employee.model';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {

  constructor(private _http: HttpClient) { }

  public getTotalEmployees(): Observable<number> {
    return this._http.get<number>(`${environment.apiBaseUrl}/dashboard/total-employees`);
  }

  public getEmployeesByDepartment(id: number): Observable<Employee[]> {
    return this._http.get<Employee[]>(`${environment.apiBaseUrl}/dashboard/employees-by-department/${id}`);
  }

  public getRecentHiresEmployees(): Observable<Employee[]> {
    return this._http.get<Employee[]>(`${environment.apiBaseUrl}/dashboard/recent-hires`);
  }
  
  public searchEmployees(query: string): Observable<Employee[]> {
    return this._http.get<Employee[]>(`${environment.apiBaseUrl}/dashboard/search?query=${query}`);
  }
}
