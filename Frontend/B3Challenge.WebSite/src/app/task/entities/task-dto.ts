import { TaskStatus } from "./task-status";

export interface TaskDto {
    Id: number;
    Description: string;
    Date : string|null;
    TaskStatusId:number;    
  }
  