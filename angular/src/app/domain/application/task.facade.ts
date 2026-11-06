import { Injectable, inject } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import {
  BehaviorSubject,
  Observable,
  catchError,
  concatMap,
  map,
  tap,
} from 'rxjs';
import { Task } from '../entities/task';
import { TaskService } from '../infrastructure/data-access/api/task.service';
import { GetTasksResponse } from '../infrastructure/data-access/model/getTasksResponse';
import {
  CreateTaskRequest,
  CreateTaskResponse,
  TaskItemDto,
  UpdateTaskRequest,
} from '../infrastructure/data-access';
import { ErrorExtractorService } from '../infrastructure/error-extractor.service';

/**
 * The facade for the Tasks domain logic.
 * It provides access to a collection of tasks and use case methods.
 */
@Injectable({ providedIn: 'root' })
export class TaskFacade {
  private readonly taskService = inject(TaskService);
  private readonly errorExtractor = inject(ErrorExtractorService);

  private readonly tasksSubject = new BehaviorSubject<Task[]>([]);

  /**
   * The Observable of the tasks collection.
   */
  readonly tasks$ = this.tasksSubject.asObservable();

  /**
   * Loads tasks. After loading, updates the tasks collection and emits the tasks$ event.
   * @param isCompleted If defined, used to filter tasks by this value of the isCompleted property.
   * @return An Observable that completes when loading is complete.
   */
  loadTasks(isCompleted?: boolean): Observable<void> {
    return this.taskService.getTasks(isCompleted).pipe(
      tap((response: GetTasksResponse) => {
        if (response.items) {
          this.tasksSubject.next(
            response.items.map(
              (dto: TaskItemDto) =>
                new Task(
                  dto.id!,
                  dto.title!,
                  dto.description!,
                  dto.isCompleted!,
                  new Date(dto.createdAt!),
                  dto.version!,
                ),
            ),
          );
        }
      }),
      map(() => {}),
      catchError((error: Error | HttpErrorResponse) => {
        console.error('fail', error);

        throw new Error(this.errorExtractor.extractError(error));
      }),
    );
  }

  /**
   * Gets a task with a specific id in the tasks collection.
   * @param id The task id.
   * @return The found Task or undefined.
   */
  getTaskById(id: string | null): Task | undefined {
    return id
      ? this.tasksSubject.getValue().find((task) => task.id === id)
      : undefined;
  }

  /**
   * Creates a new task. The method adds the task on the server, then loads it and
   * adds it into the collection.
   * @param title The task title.
   * @param description The task description.
   * @return An Observable that completes when the task has been updated
   *         and the collection refreshed. Returns the new task id.
   */
  createTask(title: string, description: string | null): Observable<string> {
    const createTaskRequest: CreateTaskRequest = {
      title,
      description,
    };
    return this.taskService.createTask(createTaskRequest).pipe(
      concatMap((response: CreateTaskResponse) => {
        return this.taskService.getTask(response.id!);
      }),
      tap((dto: TaskItemDto) => {
        const task = new Task(
          dto.id!,
          dto.title!,
          dto.description!,
          dto.isCompleted!,
          new Date(dto.createdAt!),
          dto.version!,
        );
        this.tasksSubject.next([...this.tasksSubject.getValue(), task]);
      }),
      map((dto: TaskItemDto) => dto.id!),
      catchError((error) => {
        console.error('fail', error);

        throw new Error(this.errorExtractor.extractError(error));
      }),
    );
  }

  /**
   * Completes a task. The method updates the task on the server, then loads
   * the updated task entity and mutates it in place in the collection
   * (the entity is not replaced).
   * @param task The task to complete.
   * @return An Observable that completes when the task has been updated
   *         and the collection refreshed.
   */
  completeTask(task: Task): Observable<void> {
    const updateTaskRequest: UpdateTaskRequest = {
      title: task.title,
      description: task.description,
      isCompleted: true,
      version: task.version,
    };
    return this.taskService.updateTask(task.id, updateTaskRequest).pipe(
      concatMap(() => {
        return this.taskService.getTask(task.id);
      }),
      tap((task: TaskItemDto) => {
        const collectionTask = this.getTaskById(task.id!);
        if (collectionTask) {
          collectionTask.change(
            task.title!,
            task.description!,
            task.isCompleted!,
          );
          this.tasksSubject.next([...this.tasksSubject.getValue()]);
        }
      }),
      map(() => {}),
      catchError((error) => {
        console.error('fail', error);

        throw new Error(this.errorExtractor.extractError(error));
      }),
    );
  }

  /**
   * Deletes a task. The method deletes a task on the server and then removes it
   * from the collection.
   * @param id The id of the task to delete.
   * @return An Observable that completes when the task has been deleted
   *         and the collection refreshed.
   */
  deleteTask(id: string): Observable<void> {
    return this.taskService.deleteTask(id).pipe(
      tap(() => {
        const tasks = this.tasksSubject
          .getValue()
          .filter((task) => task.id !== id);
        this.tasksSubject.next(tasks);
      }),
      catchError((error) => {
        console.error('fail', error);

        throw new Error(this.errorExtractor.extractError(error));
      }),
    );
  }
}
