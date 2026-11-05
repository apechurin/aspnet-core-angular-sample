namespace Sample.Shared.Kernel.Domain;

public abstract class AggregateRoot
{
    public uint Version { get; private set; }
}
