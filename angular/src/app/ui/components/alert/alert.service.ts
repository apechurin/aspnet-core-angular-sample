import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Alert } from './alert';

/**
 * Service used to show and hide error alerts.
 */
@Injectable({
  providedIn: 'root',
})
export class AlertService {
  private readonly alertsSubject = new BehaviorSubject<Alert[]>([]);

  private nextId = 1;

  /**
   * The visible alerts collection Observable.
   */
  readonly alerts$ = this.alertsSubject.asObservable();

  /**
   * Adds a new error alert. If there is a created AlertComponent then it shows this alert.
   * @param message The alert message.
   */
  error(message: string): Alert {
    const alert = { message, id: this.nextId++ };
    this.alertsSubject.next([...this.alertsSubject.getValue(), alert]);
    return alert;
  }

  /**
   * Removes (hides) the alert.
   */
  remove(alert: Alert): void {
    const alerts = this.alertsSubject.getValue().filter((a) => a !== alert);
    this.alertsSubject.next(alerts);
  }

  /**
   * Removes (hides) all alerts.
   */
  clear(): void {
    this.alertsSubject.next([]);
  }
}
