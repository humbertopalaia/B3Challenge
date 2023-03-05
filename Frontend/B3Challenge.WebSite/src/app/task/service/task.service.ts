import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Task } from '../entities/task';
import { TaskFilter } from '../entities/task-filter';
import { TaskDto } from '../entities/task-dto';
import { DatePipe } from '@angular/common';
import { environment } from 'src/environments/environment';

const API_URL = environment.API_URL;

@Injectable({ providedIn: 'root' })
export class TaskService {


    constructor(private http: HttpClient, private datePipe: DatePipe) {

    }

    save(entity: Task)  {
        if (entity.id == 0) {

            let url = `${API_URL}/task/insert`;
            const dateFormated = this.datePipe.transform(entity.date, 'yyyy-MM-dd');
            url = `${url}?id=${entity.id}&description=${entity.description}&date=${dateFormated}&taskStatusId=${entity.taskStatusId}`
            return this.http.put<Task>(url, entity);
        }
        else
        {
            let url = `${API_URL}/task/update`;
            return this.http.post<Task>(url, entity);
        }

    }


    delete(id:number)
    {
        let url = `${API_URL}/task/delete/${id}`;

        return this.http.delete(url);
    }


    list(filter: TaskFilter | null) {

        let url = `${API_URL}/task/filter`;
        console.log(url);
        if (filter) {

            const filterDate = this.datePipe.transform(filter.date, 'yyyy-MM-dd');
            url = `${url}?description=${filter.description}&taskStatusId=${filter.taskStatusId}&date=${filterDate}`
        }

        return this.http.get<Task[]>(url);
    }

}



