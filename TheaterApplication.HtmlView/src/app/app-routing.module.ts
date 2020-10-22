import { CodeVerifyComponent } from './components/auth/code-verify/code-verify.component';
import { RegistrationComponent } from './components/auth/registration/registration.component';
import { IsAdminGuard } from './guards/IsAdmin.guard';
import { AdminPanelComponent } from './pages/admin-panel/admin-panel.component';
import { PosterComponent } from './pages/poster/poster.component';
import { LoginComponent } from './components/auth/login/login.component';
import { AuthComponent } from './pages/auth/auth.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EditPerformanceListComponent } from './components/admin-panel/edit-performance-list/edit-performance-list.component';
import { EditPerformanceComponent } from './components/admin-panel/edit-performance/edit-performance.component';

const routes: Routes = [
  {
    path: '',
    component: PosterComponent
  },
  {
    path: 'auth',
    component: AuthComponent,
    children: [
      { path: '', redirectTo: 'login', pathMatch: 'full' },
      { path: 'login', component: LoginComponent },
      { path: 'registration', component: RegistrationComponent },
      { path: 'registration/code', component: CodeVerifyComponent }
    ]
  },
  {
    path: 'admin-panel',
    canActivate: [IsAdminGuard],
    component: AdminPanelComponent,
    children: [
      { path: '', redirectTo: 'performances', pathMatch: 'full' },
      { path: 'performances', component: EditPerformanceListComponent }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
