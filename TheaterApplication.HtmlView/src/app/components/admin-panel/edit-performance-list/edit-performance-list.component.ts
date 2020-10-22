import { PerformanceSchedulePmModel } from './../../../Models/PerformanceSchedulePm.model';
import { Moment } from 'moment';
import { PerformancePmModel } from './../../../Models/PerformancePm';
import { Component, OnInit } from '@angular/core';
import { PerformanceVmModel } from 'src/app/Models/PerformanceVm.model';
import { PerformanceService } from 'src/app/services/Performance.service';
import { PerformanceScheuleService } from 'src/app/services/PerformanceScedule.service';

@Component({
  selector: 'app-edit-performance-list',
  templateUrl: './edit-performance-list.component.html',
  styleUrls: ['./edit-performance-list.component.scss']
})
export class EditPerformanceListComponent implements OnInit {

  public list: PerformanceVmModel[];

  constructor(private performanceService: PerformanceService,
              private performanceScheuleService: PerformanceScheuleService) { }

  public name: string;
  public duration: number;

  public editPerformanceId: number;
  public editStartAt: Moment;
  public editTotalTickets: number;
  public editIsRepeat: boolean;

  ngOnInit() {
    this.loadData();
  }

  public onDelete(id: number, name: string): void {

    const isConfirm = confirm(`Are you sure you want to delete perfomance "${name}"?`);

    if (isConfirm) {
      this.performanceService.delete(id).then(() => {
        this.loadData();
      });
    }
  }

  public onCreatePerformance(): void {
    // TODO validation
    if (this.name !== null && this.name !== '') {
      const performance = new PerformancePmModel();
      performance.name = this.name;
      performance.durationMinutes = this.duration;

      this.performanceService.create(performance).then(() => {
        this.name = null;
        this.duration = null;

        this.loadData();
      });
    }
  }

  public onClickAdd(performanceId: number) {
    this.clearEditData();

    this.editPerformanceId = performanceId;
  }

  public onCreateSchedule(): void {
    // TODO validation
    const schedule = new PerformanceSchedulePmModel();
    schedule.isRepeat = this.editIsRepeat;
    schedule.startAt = this.editStartAt.toDate();
    schedule.ticketsCount = this.editTotalTickets;

    this.performanceScheuleService.create(this.editPerformanceId, schedule).then(() => {
      this.clearEditData();
      this.loadData();
    });
  }

  public onDeleteSchedule(performanceId: number, id: number): void {
    const isConfirm = confirm(`Are you sure you want to delete schedule"?`);

    if (isConfirm) {
      this.performanceScheuleService.delete(performanceId, id).then(() => {
        this.loadData();
      });
    }
  }

  private clearEditData(): void {
    this.editPerformanceId = null;
    this.editStartAt = null;
    this.editTotalTickets = null;
    this.editIsRepeat = null;
  }

  private loadData(): void {
    this.performanceService.getAll().then((performances) => {
      this.list = performances;
    });
  }

}
