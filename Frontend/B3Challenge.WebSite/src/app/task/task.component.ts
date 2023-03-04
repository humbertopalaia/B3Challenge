import { Component, ViewChild } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { TaskNewEditComponent } from './task-new-edit/task-new-edit.component';
import { TaskFilter } from './entities/task-filter';
import { TaskListComponent } from './task-list/task-list.component';
import { Task } from './entities/task';
import { Subscription } from 'rxjs';
import { TaskService } from './service/task.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.scss']
})
export class TaskComponent {
  @ViewChild('taskList') taskList!: TaskListComponent;

  dialogRef!: MatDialogRef<TaskNewEditComponent>;

  constructor(private dialog: MatDialog, public taskService: TaskService) {



  }

  openNew() {
    this.dialogRef = this.dialog.open(TaskNewEditComponent, { disableClose: true });
    this.dialogRef.componentInstance.fillFields({ id: 0, date: new Date(), description: '', taskStatusId: 1, TaskStatus: null });

    this.dialogRef.componentInstance.onSave.subscribe({
      next: (task:Task) => {
        this.taskList.isLoading = true;
        this.taskService.save(task).subscribe({
          next: (v) => {this.taskList.filter(null)},
          error: (e) => Swal.fire('Ocorreu um erro na operação!', 'Contacte o suporte', 'error'),
          complete: () => Swal.fire('Tarefa foi salva com sucesso!', '', 'success')
    
        });
      }
    });

  }

  clear() {
    this.taskList.filter(null);
  }


  filter(filter: TaskFilter) {
    this.taskList.filter(filter);
  }
}
