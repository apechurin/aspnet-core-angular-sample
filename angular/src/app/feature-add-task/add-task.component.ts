import { Component, DestroyRef, inject, signal } from '@angular/core';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { defer, finalize } from 'rxjs';
import { TaskFacade } from '../domain/application/task.facade';
import { AlertService } from '../ui/components/alert/alert.service';
import { AlertComponent } from '../ui/components/alert/alert.component';
import { BusyIndicatorComponent } from '../ui/components/busy-indicator/busy-indicator.component';

/**
 * The main component of the Add Task feature. It allows the user to create and save a new task.
 */
@Component({
  selector: 'app-add-task',
  standalone: true,
  imports: [ReactiveFormsModule, AlertComponent, BusyIndicatorComponent],
  templateUrl: './add-task.component.html',
  styleUrl: './add-task.component.scss',
})
export class AddTaskComponent {
  private readonly router = inject(Router);
  private readonly taskFacade = inject(TaskFacade);
  private readonly alertService = inject(AlertService);
  private readonly destroyRef = inject(DestroyRef);

  readonly form = new FormGroup({
    title: new FormControl('', {
      nonNullable: true,
      validators: [Validators.required],
    }),
    description: new FormControl('', {}),
  });

  busy = signal(false);

  onCancelClicked(): void {
    this.router.navigate(['tasks']);
  }

  onSubmit(): void {
    if (this.form.valid) {
      defer(() => {
        this.busy.set(true);
        this.alertService.clear();

        return this.taskFacade.createTask(
          this.form.value.title!,
          this.form.value.description!,
        );
      })
        .pipe(
          takeUntilDestroyed(this.destroyRef),
          finalize(() => this.busy.set(false)),
        )
        .subscribe(
          () => {
            this.router.navigate(['tasks']);
          },
          (error: Error) => this.alertService.error(error.message),
        );
    }
  }
}
