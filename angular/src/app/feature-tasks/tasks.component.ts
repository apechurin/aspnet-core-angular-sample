import { AsyncPipe, DatePipe } from '@angular/common';
import { Component, DestroyRef, OnInit, inject, signal } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { Router } from '@angular/router';
import { defer, finalize } from 'rxjs';
import { TaskFacade } from '../domain/application/task.facade';
import { BusyIndicatorComponent } from '../ui/components/busy-indicator/busy-indicator.component';
import { AlertService } from '../ui/components/alert/alert.service';
import { AlertComponent } from '../ui/components/alert/alert.component';
import {
  CompletedFilterItem,
  TasksViewModelService,
} from './tasks-view-model.service';
import { Task } from '../domain/entities/task';

/**
 * The main component of the Task List feature. Displays the list of tasks and
 * filters them by the Completed property when filtering is requested.
 * It also allows the user to create a new task, delete a task, and mark a task
 * as completed.
 */
@Component({
  selector: 'app-tasks',
  standalone: true,
  imports: [AsyncPipe, DatePipe, AlertComponent, BusyIndicatorComponent],
  templateUrl: './tasks.component.html',
  styleUrl: './tasks.component.scss',
})
export class TasksComponent implements OnInit {
  private readonly taskFacade = inject(TaskFacade);
  private readonly alertService = inject(AlertService);
  private readonly destroyRef = inject(DestroyRef);
  private readonly router = inject(Router);

  readonly viewModel = inject(TasksViewModelService);

  readonly tasks$ = this.taskFacade.tasks$;

  busy = signal(false);
  selectedTask = signal<Task | undefined>(undefined);

  constructor() {
    this.destroyRef.onDestroy(() => {
      this.alertService.clear();
    });
  }

  ngOnInit(): void {
    this.loadTasks();
  }

  onTaskRowClicked(task: Task): void {
    this.selectedTask.set(this.selectedTask() === task ? undefined : task);
  }

  onCompletedFilterItemClicked(item: CompletedFilterItem): void {
    this.viewModel.selectedCompletedFilterItem.set(item);

    this.loadTasks();
  }

  onCreateButtonClicked(): void {
    this.router.navigate(['addtask']);
  }

  onCompleteButtonClicked(): void {
    if (this.selectedTask()) {
      defer(() => {
        this.busy.set(true);
        this.alertService.clear();

        return this.taskFacade.completeTask(this.selectedTask()!);
      })
        .pipe(
          takeUntilDestroyed(this.destroyRef),
          finalize(() => this.busy.set(false)),
        )
        .subscribe(
          () => {},
          (error: Error) => this.alertService.error(error.message),
        );
    }
  }

  onDeleteButtonClicked(): void {
    if (this.selectedTask()) {
      defer(() => {
        this.busy.set(true);
        this.alertService.clear();

        return this.taskFacade.deleteTask(this.selectedTask()!.id);
      })
        .pipe(
          takeUntilDestroyed(this.destroyRef),
          finalize(() => this.busy.set(false)),
        )
        .subscribe(
          () => {},
          (error: Error) => this.alertService.error(error.message),
        );
    }
  }

  private loadTasks(): void {
    defer(() => {
      this.busy.set(true);
      this.alertService.clear();

      return this.taskFacade.loadTasks(
        this.viewModel.selectedCompletedFilterItem().value,
      );
    })
      .pipe(
        takeUntilDestroyed(this.destroyRef),
        finalize(() => this.busy.set(false)),
      )
      .subscribe(
        () => {
          this.restoreSelectedTaskAfterLoading();
        },
        (error: Error) => this.alertService.error(error.message),
      );
  }

  private restoreSelectedTaskAfterLoading(): void {
    if (this.selectedTask()) {
      const newSelectedTask = this.taskFacade.getTaskById(
        this.selectedTask()!.id,
      );
      this.selectedTask.set(newSelectedTask);
    }
  }
}
