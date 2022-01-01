import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MainComponent } from './main.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import {DialogModule} from 'primeng/dialog';
import {ButtonModule} from 'primeng/button';
import {DropdownModule} from 'primeng/dropdown';
import {PanelModule} from 'primeng/panel';
import {TableModule} from 'primeng/table';
import { ManageStudentComponent } from './manage-student/manage-student.component';
import { ManageStayinComponent } from './manage-stayin/manage-stayin.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { RoleGuard } from 'src/app/core/auth.guard';
import { Role } from 'src/app/shared/models/Role.enum';
import { DateVNPipe } from 'src/app/shared/pipes/DateVN.pipe';

export const mainRoute: Routes = [
  {
    path: '',
    component: MainComponent,
    children: [
      {
        path: '',
        redirectTo:'dashboard',
        pathMatch: 'full'
      },
      {
        path: 'dashboard',
        component: DashboardComponent,
      },
      {
        path: 'manage-student',
        component: ManageStudentComponent,
        canActivate: [RoleGuard], data: { roles: [Role.Admin] },
      },
      {
        path: 'manage-stayin',
        component: ManageStayinComponent,
        // canActivate: [RoleGuard], data: { roles: [Role.Admin] },
      },
    ]
  }
]
@NgModule({
  imports: [
    CommonModule,
    CommonModule,
    SharedModule,
    HttpClientModule,
    ReactiveFormsModule,
    RouterModule.forChild(mainRoute),
    NgbModule,
    FormsModule,
    DialogModule,
    ButtonModule,
    DropdownModule,
    PanelModule,
    TableModule
  ],
  declarations: [MainComponent, ManageStudentComponent, ManageStayinComponent, DashboardComponent, DateVNPipe]
})
export class MainModule { }
