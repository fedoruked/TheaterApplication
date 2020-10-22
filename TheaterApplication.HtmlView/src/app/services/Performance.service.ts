import { PerformanceVmModel } from './../Models/PerformanceVm.model';
import { environment } from './../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PerformancePmModel } from '../Models/PerformancePm';

@Injectable()
export class PerformanceService {
  private basePath: string;

  constructor(private httpClient: HttpClient) {
    this.basePath = `${environment.basePath}performances`;
  }

  public delete(id: number): Promise<void> {
    const result = this.httpClient.delete<void>(`${this.basePath}/${id}`).toPromise();

    return result;
  }

  public getAll(): Promise<PerformanceVmModel[]>{
    const result = this.httpClient.get<PerformanceVmModel[]>(this.basePath).toPromise();

    return result;
  }

  public create(performance: PerformancePmModel): Promise<void> {
    const body = JSON.stringify(performance);
    const result = this.httpClient.put<void>(this.basePath, body).toPromise();

    return result;
  }

}
