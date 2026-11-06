/**
 * The task entity.
 */
export class Task {
  #title: string;
  #description: string | null;
  #isCompleted: boolean;
  #createdAt: Date;

  get title(): string {
    return this.#title;
  }

  get description(): string | null {
    return this.#description;
  }

  get isCompleted(): boolean {
    return this.#isCompleted;
  }

  get createdAt(): Date {
    return this.#createdAt;
  }

  /**
   * Constructs the new entity with specific properties.
   */
  constructor(
    public readonly id: string,

    title: string,
    description: string | null,
    isCompleted: boolean,
    createdAt: Date,

    public readonly version: number,
  ) {
    this.#title = title;
    this.#description = description;
    this.#isCompleted = isCompleted;
    this.#createdAt = createdAt;
  }

  /**
   * Changes the entity properties.
   */
  change(
    title: string,
    description: string | null,
    isCompleted: boolean,
  ): void {
    this.#title = title;
    this.#description = description;
    this.#isCompleted = isCompleted;
  }
}
