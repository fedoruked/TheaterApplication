import { PerformanceScheduleVmModel } from './PerformanceScheduleVm.model';
export class PerformanceVmModel {
  public id: number;
  public name: string;
  public durationMinutes: number;

  public schedules: PerformanceScheduleVmModel[];
}
