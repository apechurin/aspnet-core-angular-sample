namespace Sample.Api.Contracts;

public record UpdateTaskRequest(string Title, string? Description, bool IsCompleted, uint Version);
