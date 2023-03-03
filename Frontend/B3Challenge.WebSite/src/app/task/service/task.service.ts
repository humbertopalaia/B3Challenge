import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Task } from '../entities/task';


// const API = environment.apiUrl;
const API_URL = 'https://localhost:49159/api'

@Injectable({ providedIn: 'root' })
export class TaskService {

    
    constructor(private http: HttpClient) 
    {

    }    

    // salvar(dado:Funcionario) {
    //     return this.http.post(API + '/funcionario/Salvar', dado );
    // }

    // excluir(id:number)
    // {
    //     return this.http.post(API + '/funcionario/Apagar', {id:id}  );
    // }


    listar(sentence:string) {
       return this.http.get<Task[]>( `${API_URL}/task/filter?description=${sentence}`);
    }

}



