import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RoleGuard } from './core/auth.guard';
import { PageNotFoundComponent } from './shared/components/page-not-found/page-not-found.component';
import { UnauthorizedComponent } from './shared/components/unauthorized/unauthorized.component';
import { Role } from './shared/models/Role.enum';

const routes: Routes = [
  {
    path: '', 
    loadChildren: () => import('./pages/main/main.module').then(m => m.MainModule),
   
  },
  { 
    path: 'auth', 
    loadChildren: () => import('./pages/auth/auth.module').then(m => m.AuthModule)
  },
  { path: 'unauthorized', component: UnauthorizedComponent},
  { path: "**", component: PageNotFoundComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
