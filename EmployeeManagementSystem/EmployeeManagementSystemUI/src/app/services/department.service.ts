import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Department } from '../models/department.model';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DepartmentService {

  constructor(private _http: HttpClient) { }

  public addDepartment(department: Department): Observable<Department> {
      return this._http.post<Department>(`${environment.apiBaseUrl}/departments`, department);
    }
  
    public getAllDepartments(): Observable<Department[]> {
      return this._http.get<Department[]>(`${environment.apiBaseUrl}/departments`);
    }
  
    public getDepartmentById(id: number): Observable<Department> {
      return this._http.get<Department>(`${environment.apiBaseUrl}/departments/${id}`);
    }
  
    public updateDepartment(id: number, department: Department): Observable<void> {
      return this._http.put<void>(`${environment.apiBaseUrl}/departments/${id}`, department);
    }
  
    public deleteDepartment(id: number): Observable<void> {
      return this._http.delete<void>(`${environment.apiBaseUrl}/departments/${id}`)
    }
}
