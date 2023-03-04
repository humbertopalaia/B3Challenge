import { Component, ViewChild } from '@angular/core';
import { PageHeaderComponent } from 'src/app/shared/page-header/page-header.component';
import { TaskFilter } from './entities/task-filter';
import { TaskListComponent } from './task-list/task-list.component';

@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.scss']
})
export class TaskComponent {
  @ViewChild('taskList') taskList!: TaskListComponent;


  
  clear()
  {
    this.taskList.filter(null);
  }
  

  filter(filter:TaskFilter) {
    this.taskList.filter(filter);
  }
}
