import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';

/**
 * The domain-specific service used to extract a user-friendly message from
 * an error response or exception, to be shown to the user.
 */
@Injectable({
  providedIn: 'root',
})
export class ErrorExtractorService {
  private readonly errorCodes = new Map([
    [
      'task.not_found',
      'Задача больше недоступна. Возможно, она была удалена. Обновите страницу, чтобы продолжить с актуальными данными.',
    ],
    [
      'task.update_conflict',
      'Задача была изменена в другом месте. Обновите страницу, чтобы продолжить с актуальными данными.',
    ],
    ['task.title.required', 'Требуется заголовок.'],
    ['task.title.too_long', 'Заголовок слишком длинный.'],
  ]);

  /**
   * Extracts the error message.
   */
  extractError(error: Error | HttpErrorResponse): string {
    if (error instanceof HttpErrorResponse) {
      if (error.status === 0) {
        return 'Ошибка соединения. Проверьте подключение к интернету.';
      }
      if (error.status === 503) {
        return 'Сервис временно недоступен. Пожалуйста, попробуйте позже.';
      }
      return this.decodeErrorCode(
        error?.error?.title,
        'Что-то пошло не так. Пожалуйста, попробуйте позже.',
      );
    }
    return 'Что-то пошло не так. Пожалуйста, попробуйте позже.';
  }

  private decodeErrorCode(code: string, defaultMessage: string): string {
    return this.errorCodes.get(code) || defaultMessage;
  }
}
