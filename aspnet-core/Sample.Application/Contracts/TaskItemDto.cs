using Sample.Domain.Tasks;

namespace Sample.Application.Contracts;

public class TaskItemDto
{
    public Guid Id { get; }

    public string Title { get; }

    public string? Description { get; }

    public bool IsCompleted { get; }

    public DateTime CreatedAt { get; }

    public uint Version { get; }

    public TaskItemDto(TaskItem taskItem)
    {
        Id = taskItem.Id.Value;
        Title = taskItem.Title;
        Description = taskItem.Description;
        IsCompleted = taskItem.IsCompleted;
        CreatedAt = taskItem.CreatedAt;
        Version = taskItem.Version;
    }
}
