import { PerformanceBookingService } from './../../../services/PerformanceBooking.service';
import { PerformancePosterPmModel } from './../../../Models/PerformancePosterPm.model';
import { Router } from '@angular/router';
import { CookieStoreService } from './../../../services/CookieStore.service';
import { PerformancePosterService } from './../../../services/PerformancePoster.service';
import { PerformanceVmModel } from './../../../Models/PerformanceVm.model';
import { Component, OnInit } from '@angular/core';
import { PerformancePosterVmModel } from 'src/app/Models/PerformancePosterVm.model';
import { Moment } from 'moment';
import * as moment from 'moment';

@Component({
  selector: 'app-performance-list',
  templateUrl: './PerformanceList.component.html',
  styleUrls: ['./PerformanceList.component.scss']
})
export class PerformanceListComponent implements OnInit {

  public list: PerformanceVmModel[];
  public posters: PerformancePosterVmModel[];
  public fromDate: Moment;
  public toDate: Moment;
  public keyword: string;

  public pageSize: number;
  public page: number;
  public totalItems: number;

  public pages: any[];

  constructor(private performancePosterService: PerformancePosterService,
              private cookieStoreService: CookieStoreService,
              private performanceBookingService: PerformanceBookingService,
              private router: Router) { }

  ngOnInit() {
    this.keyword = '';
    this.pageSize = 5;
    this.page = 1;
    this.totalItems = 0;

    const now = new Date();

    this.fromDate = moment(new Date(now.getFullYear(), now.getMonth(), now.getDate()));
    this.toDate = moment(new Date(now.getFullYear() + 1, now.getMonth(), now.getDate()));

    this.loadData();
  }

  public onCreateBooking(poster: PerformancePosterVmModel): void {
    if (!this.isLogin()) {
      this.router.navigateByUrl('/auth');
      return;
    }

    if (poster.id === 0) {
      // todo create mapper
      const posterPm = new PerformancePosterPmModel();
      posterPm.differenceFromStartDays = poster.differenceFromStartDays;
      posterPm.eventDate = poster.eventDate;

      this.performancePosterService.create(poster.schedule.performance.id,
        poster.schedule.id, posterPm).then((id) => {

          poster.id = id;
          this.createBooking(poster.schedule.performance.id, poster.schedule.id, poster.id);
      });
    } else {
      this.createBooking(poster.schedule.performance.id, poster.schedule.id, poster.id);
    }
  }

  public loadData(): void {
    this.performancePosterService.getPage(this.page, this.pageSize, this.keyword, this.fromDate, this.toDate).then((data) => {
      this.posters = data.data;
      this.totalItems = data.totalCount;
    });
  }

  public onPageChange(page: any): void {
    this.page = page;
    this.loadData();
  }

  private isLogin(): boolean {
    const result = this.cookieStoreService.user != null;

    return result;
  }

  private createBooking(performanceId: number, scheduleId: number, posterId: number): void {
    this.performanceBookingService.create(performanceId, scheduleId, posterId).then(() => {
      this.loadData();
    });
  }
}
