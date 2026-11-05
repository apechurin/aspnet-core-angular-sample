using Sample.Shared.Kernel;
using Sample.Shared.Kernel.Domain;

namespace Sample.Domain.Tasks;

public class TaskItem : AggregateRoot
{
    public const int MaxTitleLength = 200;

    #region Properties

    public TaskId Id { get; private set; }

    public string Title { get; private set; } = null!;

    public string? Description { get; private set; }

    public bool IsCompleted { get; private set; }

    public DateTime CreatedAt { get; private set; }

    #endregion

    private TaskItem() { }

    public static TaskItem Create(string title, string? description)
    {
        ValidateTitle(title);

        var taskItem = new TaskItem
        {
            Id = TaskId.New(),
            Title = title,
            Description = description,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
        };
        return taskItem;
    }

    public void Change(string title, string? description, bool isCompleted)
    {
        ValidateTitle(title);

        Title = title;
        Description = description;
        IsCompleted = isCompleted;
    }

    private static void ValidateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ValidationException(DomainErrors.TaskTitleRequired, "Title is required");
        if (title.Length > MaxTitleLength)
            throw new ValidationException(DomainErrors.TaskTitleTooLong, "Title is too long");
    }
}
