import { Component, inject } from '@angular/core';
import { AlertService } from './alert.service';
import { AsyncPipe } from '@angular/common';
import { Alert } from './alert';

/**
 * Helper UI component used to show error alerts.
 */
@Component({
  selector: 'app-alert',
  standalone: true,
  imports: [AsyncPipe],
  templateUrl: './alert.component.html',
  styleUrl: './alert.component.scss',
})
export class AlertComponent {
  private readonly alertService = inject(AlertService);

  readonly alerts$ = this.alertService.alerts$;

  onCloseButtonClicked(alert: Alert): void {
    this.alertService.remove(alert);
  }
}
