import { CodeVerifyComponent } from './components/auth/code-verify/code-verify.component';
import { RegistrationComponent } from './components/auth/registration/registration.component';
import { HeaderComponent } from './components/shared/header/header.component';
import { PerformanceService } from './services/Performance.service';
import { PerformanceListComponent } from './components/poster/PerformanceList/PerformanceList.component';
import { PosterComponent } from './pages/poster/poster.component';
import { UserService } from './services/User.service';
import { LoginComponent } from './components/auth/login/login.component';
import { AuthComponent } from './pages/auth/auth.component';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { CookieStoreService } from './services/CookieStore.service';
import { AdminPanelComponent } from './pages/admin-panel/admin-panel.component';
import { GlobalHttpInterceptor } from './services/Interceptors/GlobalHttp.Interceptor';
import { EditPerformanceListComponent } from './components/admin-panel/edit-performance-list/edit-performance-list.component';
import { PerformancePosterService } from './services/PerformancePoster.service';
import { PerformanceBookingService } from './services/PerformanceBooking.service';
import { DpDatePickerModule } from 'ng2-date-picker';
import { NgxPaginationModule } from 'ngx-pagination';
import { EditPerformanceComponent } from './components/admin-panel/edit-performance/edit-performance.component';
import { PerformanceScheuleService } from './services/PerformanceScedule.service';

@NgModule({
  declarations: [
    AppComponent,
    AuthComponent,
    LoginComponent,
    PosterComponent,
    PerformanceListComponent,
    HeaderComponent,
    AdminPanelComponent,
    EditPerformanceListComponent,
    EditPerformanceComponent,
    RegistrationComponent,
    CodeVerifyComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    HttpClientModule,
    DpDatePickerModule,
    NgxPaginationModule,
  ],
  providers: [
    UserService,
    PerformanceService,
    CookieService,
    CookieStoreService,
    PerformancePosterService,
    PerformanceBookingService,
    PerformanceScheuleService,

    { provide: HTTP_INTERCEPTORS, useClass: GlobalHttpInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
