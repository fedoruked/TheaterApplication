import { environment } from './../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PerformancePosterVmModel } from '../Models/PerformancePosterVm.model';
import { PerformancePosterPmModel } from '../Models/PerformancePosterPm.model';
import { Moment } from 'moment';
import * as moment from 'moment';
import { DataWithPaging } from '../Models/DataWithPaging';

@Injectable()
export class PerformancePosterService {
  private basePath: string;

  public constructor(private httpClient: HttpClient) {
    this.basePath = `${environment.basePath}performances`;
  }

  public getPage(page: number, pageSize: number, keyword: string,
                 fromDate: Moment, toDate: Moment): Promise<DataWithPaging<PerformancePosterVmModel>> {

      // TODO add general parser
      const fromDateStr = fromDate.format('YYYY-MM-DD');
      const toDateStr = toDate.format('YYYY-MM-DD');

      const url = `${this.basePath}/posters?page=${page}` +
        `&pageSize=${pageSize}&keyword=${keyword}&fromDate=${fromDateStr}` +
        `&toDate=${toDateStr}`;

      const result = this.httpClient.get<DataWithPaging<PerformancePosterVmModel>>(url).toPromise();

      return result;
  }

  public create(performanceId: number, scheduleId: number, posterPm: PerformancePosterPmModel): Promise<number>{
    const url = `${this.basePath}/${performanceId}/schedules/${scheduleId}/posters`;
    const body = JSON.stringify(posterPm);

    const result = this.httpClient.put<number>(url, body).toPromise();

    return result;
  }

}
