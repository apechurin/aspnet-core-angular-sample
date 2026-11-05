namespace Sample.Shared.Kernel;

public class AppException : Exception
{
    public string Code { get; }

    public AppException(string code, string message)
        : base(message)
    {
        Code = code;
    }

    public AppException(string code, string message, Exception inner)
        : base(message, inner)
    {
        Code = code;
    }
}

public sealed class ValidationException : AppException
{
    public ValidationException(string code, string message)
        : base(code, message) { }
}

public sealed class NotFoundException : AppException
{
    public NotFoundException(string code, string message)
        : base(code, message) { }
}

public sealed class ConflictException : AppException
{
    public ConflictException(string code, string message)
        : base(code, message) { }

    public ConflictException(string code, string message, Exception inner)
        : base(code, message, inner) { }
}
