import { Component, ElementRef, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { MatDatepicker } from '@angular/material/datepicker';
import { MatSelect } from '@angular/material/select';
import { DateAdapter } from '@angular/material/core';



import { TaskFilter } from '../entities/task-filter';


@Component({
  selector: 'app-task-filter',
  templateUrl: './task-filter.component.html',
  styleUrls: ['./task-filter.component.scss']
})
export class TaskFilterComponent implements OnInit {
  @ViewChild('description') description!: ElementRef;
  @ViewChild('status') status!: MatSelect;
  @ViewChild('date') date!: ElementRef;
  @ViewChild('date') datePicker!: MatDatepicker<any>;

  @Output() onFilter = new EventEmitter<TaskFilter>();
  @Output() onClear = new EventEmitter();
  constructor() {

  }
  ngOnInit(): void {
    // this.datePicker.setLocale('pt-br');
  }


  getDescription(): string {
    return this.description.nativeElement.value.toString();
  }


  getDate(): string {

    return this.date.nativeElement.value;
  }

  getStatus(): string | null {
    if (this.status.value)
      return this.status.value.toString();
    else
      return null;
  }


  clear()
  {
    this.description.nativeElement.value = '';
    this.status.value = '';
    this.date.nativeElement.value = '';
    this.onClear.emit();
  }

  filter() {

    const descriptionFilterText = this.getDescription();
    const dateFilterText = this.getDate();
    const statusFilterText = this.getStatus();

    let dateValues: string[] = [];
    let dateFilter = null;

    if (dateFilterText)
      dateValues = dateFilterText.split('/');

    if (dateValues.length == 3)
      dateFilter = new Date(parseInt(dateValues[2]), parseInt(dateValues[1]), parseInt(dateValues[0]));

    this.onFilter.emit({ description: descriptionFilterText, date: (dateFilter ? dateFilter : null), taskStatusId: (statusFilterText ? parseInt(statusFilterText) : null) });
  }

}
