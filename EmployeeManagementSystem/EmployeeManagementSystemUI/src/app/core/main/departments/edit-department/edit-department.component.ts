import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Department } from 'src/app/models/department.model';
import { DepartmentService } from 'src/app/services/department.service';

@Component({
  selector: 'app-edit-department',
  templateUrl: './edit-department.component.html',
  styleUrls: ['./edit-department.component.css']
})
export class EditDepartmentComponent implements OnInit, OnDestroy {

  public id: number = 0;
  private paramsSubscription?: Subscription;
  private departmentSubscription?: Subscription;
  private updateDepartmentSubscription?: Subscription;
  private deleteDepartmentSubscription?: Subscription;
  public department?: Department;

  constructor(private _route: ActivatedRoute,
    private _departmentService: DepartmentService,
    private _router: Router
  ) {

  }

  ngOnDestroy(): void {
    this.paramsSubscription?.unsubscribe();
    this.departmentSubscription?.unsubscribe();
    this.updateDepartmentSubscription?.unsubscribe();
    this.deleteDepartmentSubscription?.unsubscribe();
  }

  ngOnInit(): void {
    this.paramsSubscription = this._route.paramMap.subscribe({
      next: (params) => {
        const idParam: any = params.get('id');
        if (idParam != null) {
          this.id = parseInt(idParam);
        }

        if (this.id) {
          this.deleteDepartmentSubscription = this._departmentService.getDepartmentById(this.id)
            .subscribe({
              next: (depart: Department) => {
                this.department = depart;
              },
              error: (error) => console.log(error)
            });
        }
      }
    });
  }

  public onFormSubmit(): void {
    if (this.department && this.id) {
      this.updateDepartmentSubscription = this._departmentService.updateDepartment(this.id, this.department)
        .subscribe({
          next: () => {
            this._router.navigateByUrl('/departments');
          },
          error: (error) => console.log(error)
        });
    }
  }

  public onDelete(): void {
    if (this.id) {
      this.deleteDepartmentSubscription = this._departmentService.deleteDepartment(this.id)
        .subscribe({
          next: () => {
            this._router.navigateByUrl('/departments');
          },
          error: (error) => {
            alert("Cannot delete department with employees");
          }
        });
    }
  }

  public confirmDelete() {
    if (confirm('Are you sure you want to delete this department?')) {
      this.onDelete();
    }
  }

}
