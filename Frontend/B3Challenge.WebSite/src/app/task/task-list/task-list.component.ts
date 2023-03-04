import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTable } from '@angular/material/table';
import { Task } from '../entities/task';
import { TaskService } from '../service/task.service';
import { formatDate } from '@angular/common'
import { TaskFilter } from '../entities/task-filter';
import Swal from 'sweetalert2';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { TaskNewEditComponent } from '../task-new-edit/task-new-edit.component';


@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.scss']
})

export class TaskListComponent implements OnInit {
  @ViewChild(MatTable)

  
  table!: MatTable<any>;
  displayedColumns: string[] = ['id', 'description', 'date', 'status', 'action'];
  dialogRef!: MatDialogRef<TaskNewEditComponent>;
  tasks: Task[] = [];

  public isLoading:boolean=false;



  constructor(public taskService: TaskService, private dialog: MatDialog) {


  }

  ngOnInit(): void {
     this.filter(null);

  }

  ngAfterViewInit() {
  
  }

  public edit(task: Task) {
    this.dialogRef = this.dialog.open(TaskNewEditComponent, { disableClose: true });
    this.dialogRef.componentInstance.fillFields(task);

    this.dialogRef.componentInstance.onSave.subscribe({
      next: (task: Task) => {
        this.isLoading = true;
        this.taskService.save(task).subscribe({
          next: (v) =>  this.isLoading = false,
          error: (e) => Swal.fire('Ocorreu um erro na operação!', 'Contacte o suporte', 'error'),
          complete: () => Swal.fire('Tarefa foi salva com sucesso!', '', 'success')

        });
      }
    });
  }

  public confirmDelete(id: number) {
    
    Swal.fire({
      title: 'Deseja excluir o registro?',
      text: 'Esse processo é irreversível.',
      icon: 'question',
      showCancelButton: true,
      confirmButtonText: 'Sim',
      cancelButtonText: 'Não',
    }).then((result) => {
      if (result.value) {
        this.delete(id);
      }
    });
  }

  private delete(id: number) {
    this.isLoading = true;
    this.taskService.delete(id).subscribe({
      next: (v) => this.filter(null),
      error: (e) => Swal.fire('Ocorreu um erro na operação!', 'Contacte o suporte', 'error'),
      complete: () => Swal.fire('Tarefa foi excluída com sucesso!', '', 'success')

    });

  }

  public filter(filter: TaskFilter | null) {
    
    this.isLoading = true;
    this.taskService.list(filter).subscribe(x => {
      this.tasks = x;
      this.isLoading = false;
    });
  }

}
