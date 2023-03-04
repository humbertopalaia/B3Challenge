import { Component, ViewChild } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { TaskNewEditComponent } from '../task-new-edit/task-new-edit.component';
import { TaskFilter } from './entities/task-filter';
import { TaskListComponent } from './task-list/task-list.component';
import { Task } from './entities/task';

@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.scss']
})
export class TaskComponent {
  @ViewChild('taskList') taskList!: TaskListComponent;

  dialogRef!:MatDialogRef<TaskNewEditComponent>;

  constructor(private dialog: MatDialog){}
  
  openNew() {
     this.dialogRef = this.dialog.open(TaskNewEditComponent, { disableClose: true });
     this.dialogRef.componentInstance.fillFields({id:0, date: '', description :'', taskStatusId:1, TaskStatus:null  });
  }

  clear()
  {
    this.taskList.filter(null);
  }
  

  filter(filter:TaskFilter) {
    this.taskList.filter(filter);
  }
}
