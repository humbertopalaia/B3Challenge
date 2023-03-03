import { TaskStatus } from "./task-status";

export interface Task {
    id: number;
    description: string;  
    date : string;
    taskStatusId:number;
    Status: TaskStatus
  }
  