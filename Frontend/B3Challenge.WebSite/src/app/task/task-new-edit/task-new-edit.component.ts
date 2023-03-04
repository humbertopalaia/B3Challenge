import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Output, OnInit, ViewChild, ElementRef } from '@angular/core';
import { FormGroup, FormControl, Validators, AbstractControl } from '@angular/forms';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';
import { MatDialogRef } from '@angular/material/dialog';

import { MatSelect } from '@angular/material/select';
import { Task } from '../entities/task';

@Component({
  selector: 'app-task-new-edit',
  templateUrl: './task-new-edit.component.html',
  styleUrls: ['./task-new-edit.component.scss']
})

export class TaskNewEditComponent implements OnInit {

  @Output() onSave = new EventEmitter<Task>();
  @Output() onCancel = new EventEmitter();

  private id!: number;
  private currentDate!:string;

  public taskForm = new FormGroup({
    description: new FormControl(''),
    date: new FormControl(''),
    status: new FormControl('')
  });

  constructor(private dialogRef: MatDialogRef<TaskNewEditComponent>, private datePipe: DatePipe) {
    this.currentDate 
  }

  ngOnInit(): void {

  }
  
  onSelectContractDate(event:MatDatepickerInputEvent<any,any> ) {

      this.currentDate = this.datePipe.transform(event.value,'yyyy-MM-dd')!;
    
    }

  fillFields(task: Task) {

    if(!task.date)
      task.date = new Date();

    
    const dateFormated =  this.datePipe.transform(task.date,'yyyy-MM-dd');
    console.log(dateFormated);
    
    this.id = task.id;
    this.taskForm.patchValue({ description: task.description });
    this.taskForm.patchValue({ date: dateFormated });
    this.currentDate = this.datePipe.transform(task.date,'yyyy-MM-dd')!;
    this.taskForm.patchValue({ status: task.taskStatusId.toString() });
  }

  cancel() {
    this.dialogRef.close();
  }

  save() {

    const id = this.id;
    const newDescription = this.taskForm.get('description')!.value;
    
    const newStatusId = this.taskForm.get('status')!.value;
    
    const splitedDate = this.currentDate.split('-');
    let newDate = null;
    if(splitedDate.length == 3)
    {
      newDate = new Date(parseInt(splitedDate[0]), parseInt(splitedDate[1])-1, parseInt(splitedDate[2]));
    }


    let task: Task = { id: this.id, description: newDescription!, date: newDate, taskStatusId: parseInt(newStatusId!), TaskStatus: null };
    
    this.onSave.emit(task);



    this.dialogRef.close();
  }
}
