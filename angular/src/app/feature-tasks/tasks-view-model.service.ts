import { Injectable, signal } from '@angular/core';

/**
 * The item in the Completed filter dropdown.
 */
export interface CompletedFilterItem {
  name: string;
  value?: boolean;
}

/**
 * The ViewModel of TaskComponent. It persists the component state.
 */
@Injectable({
  providedIn: 'root',
})
export class TasksViewModelService {
  /**
   * The Completed filter dropdown items.
   */
  readonly completedFilterItems: CompletedFilterItem[] = [
    { name: 'Все' },
    { name: 'Выполненные', value: true },
    { name: 'Невыполненные', value: false },
  ];

  /**
   * The currently selected item of the Completed filter dropdown.
   */
  selectedCompletedFilterItem = signal<CompletedFilterItem>(
    this.completedFilterItems[0],
  );
}
