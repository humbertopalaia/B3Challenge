import { Component, EventEmitter, Output, OnInit, ViewChild, ElementRef } from '@angular/core';
import { FormGroup, FormControl, Validators, AbstractControl } from '@angular/forms';

import { MatSelect } from '@angular/material/select';
import { Task } from '../task/entities/task';

@Component({
  selector: 'app-task-new-edit',
  templateUrl: './task-new-edit.component.html',
  styleUrls: ['./task-new-edit.component.scss']
})

export class TaskNewEditComponent  implements OnInit{
 
  @Output() onSave = new EventEmitter<Task>();
  @Output() onCancel = new EventEmitter();
  
  public taskForm = new FormGroup({
      description: new FormControl(''),
      date: new FormControl(new Date()),
      status: new FormControl('')
  });


  ngOnInit(): void {
    


  }

  fillFields(task:Task)
  {
     let dateSplited = task.date.split('/');
     let newDate = new Date();
     console.log(dateSplited);

     if(dateSplited.length == 3)
        newDate = new Date(parseInt(dateSplited[2]), parseInt(dateSplited[1]), parseInt(dateSplited[0]));

    console.log(newDate);

     this.taskForm.patchValue({ description: task.description });
     this.taskForm.patchValue({ date: newDate });
     this.taskForm.patchValue({ status: task.taskStatusId.toString() });
  }

  cancel()
  {

  }

  save()
  {

  }
}
