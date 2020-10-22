import { PerformanceScheduleVmModel } from './PerformanceScheduleVm.model';

export class PerformancePosterVmModel {
  public id: number;
  public eventDate: Date;
  public differenceFromStartDays: number;
  public bookedCount: number;
  public freeTickets: number;

  public schedule: PerformanceScheduleVmModel;
}
