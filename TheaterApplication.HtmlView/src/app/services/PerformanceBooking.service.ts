import { environment } from './../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable()
export class PerformanceBookingService {
  private basePath: string;

  public constructor(private httpClient: HttpClient) {
    this.basePath = `${environment.basePath}performances`;
  }

  public create(performanceId: number, scheduleId: number, posterId: number): Promise<number> {
    const url = `${this.basePath}/${performanceId}/schedules/${scheduleId}/posters/${posterId}/bookings`;

    const result = this.httpClient.put<number>(url, {}).toPromise();

    return result;
  }

}
