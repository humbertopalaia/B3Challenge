import { Component } from '@angular/core';
import { TaskService } from '../service/task.service';

@Component({
  selector: 'app-task-filter',
  templateUrl: './task-filter.component.html',
  styleUrls: ['./task-filter.component.scss']
})
export class TaskFilterComponent {

  constructor(private taskService:TaskService)
  {
    console.log('123');
     taskService.listar('a').subscribe(x =>
      {
        console.log(x);
      })
  }

}
