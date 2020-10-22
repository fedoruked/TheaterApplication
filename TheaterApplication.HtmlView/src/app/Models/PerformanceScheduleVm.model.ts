import { PerformanceVmModel } from 'src/app/Models/PerformanceVm.model';

export class PerformanceScheduleVmModel {
  public id: number;
  public startAt: Date;
  public ticketsCount: number;
  public isRepeat: boolean;

  public performance: PerformanceVmModel;
}
