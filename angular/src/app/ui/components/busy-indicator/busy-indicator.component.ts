import { Component, input } from '@angular/core';

/**
 * Helper UI component used to block the entire screen and show a spinner
 * while an async operation is in progress.
 */
@Component({
  selector: 'app-busy-indicator',
  standalone: true,
  imports: [],
  templateUrl: './busy-indicator.component.html',
  styleUrl: './busy-indicator.component.scss',
})
export class BusyIndicatorComponent {
  /**
   * Set it to true to block UI.
   */
  busy = input(false);
}
