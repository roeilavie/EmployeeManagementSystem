import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Department } from 'src/app/models/department.model';
import { DepartmentService } from 'src/app/services/department.service';

@Component({
  selector: 'app-department-list',
  templateUrl: './department-list.component.html',
  styleUrls: ['./department-list.component.css']
})

export class DepartmentListComponent implements OnInit {

  departments$?: Observable<Department[]>;

  constructor(private _departmentService: DepartmentService) {
    
  }

  ngOnInit(): void {
    this.departments$ = this._departmentService.getAllDepartments();
  }

}
