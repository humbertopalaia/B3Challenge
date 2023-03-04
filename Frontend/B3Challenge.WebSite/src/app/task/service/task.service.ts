import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Task } from '../entities/task';
import { TaskFilter } from '../entities/task-filter';


// const API = environment.apiUrl;
const API_URL = 'https://localhost:11594/api'

@Injectable({ providedIn: 'root' })
export class TaskService {


    constructor(private http: HttpClient) {
    
    }

    // salvar(dado:Funcionario) {
    //     return this.http.post(API + '/funcionario/Salvar', dado );
    // }

    // excluir(id:number)
    // {
    //     return this.http.post(API + '/funcionario/Apagar', {id:id}  );
    // }


    listar(filter: TaskFilter|null) {

        let url = `${API_URL}/task/filter`;
        if(filter)
        {

            const filterDate = (filter.date ? filter.date.getFullYear() + "-" + ("0" + (filter.date.getMonth())).slice(-2) + "-" +  ("0" + (filter.date.getDate())).slice(-2): null);
            url = `${url}?description=${filter.description}&taskStatusId=${filter.taskStatusId}&date=${filterDate}`
        }

        return this.http.get<Task[]>(url);
    }

}



