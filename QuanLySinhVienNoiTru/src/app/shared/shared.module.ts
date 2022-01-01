import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LeftSlidebarComponent } from './components/left-slidebar/left-slidebar.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { TopbarComponent } from './components/topbar/topbar.component';
import { UnauthorizedComponent } from './components/unauthorized/unauthorized.component';
import { RouterModule } from '@angular/router';


@NgModule({
  imports: [
    CommonModule,
    RouterModule
  ],
  declarations: [
    LeftSlidebarComponent,
    PageNotFoundComponent,
    TopbarComponent,
    UnauthorizedComponent
  ],
  exports: [
    TopbarComponent, LeftSlidebarComponent
  ]
})
export class SharedModule { }
