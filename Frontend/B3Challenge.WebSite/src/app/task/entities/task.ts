import { TaskStatus } from "./task-status";

export interface Task {
    id: number;
    description: string;
    date : Date|null;
    taskStatusId:number;
    TaskStatus: TaskStatus|null;
  }
  