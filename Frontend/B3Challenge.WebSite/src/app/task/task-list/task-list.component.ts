import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTable } from '@angular/material/table';
import { Task } from '../entities/task';
import { TaskService } from '../service/task.service';
import { formatDate } from '@angular/common'
import { TaskFilter } from '../entities/task-filter';

const mockTasks: Task[] = [
  { id: 10, description: 'teste', date: '03/03/2023', taskStatusId: 1, TaskStatus: { id: 1, name: 'Pendente' } },
  { id: 20, description: 'teste2', date: '04/03/2023', taskStatusId: 1, TaskStatus: { id: 1, name: 'Pendente' } },
  { id: 30, description: 'teste3', date: '05/03/2023', taskStatusId: 1, TaskStatus: { id: 1, name: 'Pendente' } },
  { id: 40, description: 'teste4', date: '06/03/2023', taskStatusId: 1, TaskStatus: { id: 1, name: 'Pendente' } },
  { id: 50, description: 'teste5', date: '07/03/2023', taskStatusId: 1, TaskStatus: { id: 1, name: 'Pendente' } },
  { id: 60, description: 'teste6', date: '08/03/2023', taskStatusId: 1, TaskStatus: { id: 1, name: 'Pendente' } },
];


@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.scss']
})

export class TaskListComponent implements OnInit {
  @ViewChild(MatTable)
  table!: MatTable<any>;
  displayedColumns: string[] = ['id', 'description', 'date', 'status', 'action'];
  tasks: Task[] = [];
  constructor(public taskService: TaskService) {

    this.filter(null);
   
  }

  ngOnInit(): void {

  }

  public filter(filter:TaskFilter|null)
  {
     this.taskService.listar(filter).subscribe(x => {
       this.tasks = x;
     })
  }


}
