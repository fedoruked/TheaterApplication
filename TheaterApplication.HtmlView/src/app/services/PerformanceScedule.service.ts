import { environment } from './../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { PerformanceSchedulePmModel } from '../Models/PerformanceSchedulePm.model';
import { Injectable } from '@angular/core';

@Injectable()
export class PerformanceScheuleService {
  private basePath: string;

  public constructor(private httpClient: HttpClient) {
    this.basePath = `${environment.basePath}performances`;
  }

  public create(performanceId: number, schedule: PerformanceSchedulePmModel): Promise<void> {
    const body = JSON.stringify(schedule);

    const result = this.httpClient.put<void>(`${this.basePath}/${performanceId}/schedules`, body).toPromise();
    return result;
  }

  public delete(performanceId: number, id: number): Promise<void> {
    const result = this.httpClient.delete<void>(`${this.basePath}/${performanceId}/schedules/${id}`).toPromise();

    return result;
  }
}
