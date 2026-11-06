import { Routes } from '@angular/router';
import { TasksComponent } from './feature-tasks/tasks.component';
import { AddTaskComponent } from './feature-add-task/add-task.component';

export const routes: Routes = [
  {
    path: 'tasks',
    component: TasksComponent,
  },
  {
    path: 'addtask',
    component: AddTaskComponent,
  },
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'tasks',
  },
  {
    path: '**',
    redirectTo: 'tasks',
  },
];
