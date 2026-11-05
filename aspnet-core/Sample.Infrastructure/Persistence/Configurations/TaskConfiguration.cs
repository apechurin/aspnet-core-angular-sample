using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Domain.Tasks;

namespace Sample.Infrastructure.Persistence.Configurations;

public sealed class TaskConfiguration : IEntityTypeConfiguration<TaskItem>
{
    public void Configure(EntityTypeBuilder<TaskItem> builder)
    {
        builder.ToTable("tasks");

        builder.HasKey(p => p.Id);

        builder
            .Property(p => p.Id)
            .HasConversion(id => id.Value, value => new TaskId(value))
            .HasColumnName("id");

        builder
            .Property(p => p.Title)
            .HasColumnName("title")
            .HasMaxLength(TaskItem.MaxTitleLength)
            .IsRequired();

        builder.Property(p => p.Description).HasColumnName("description").HasColumnType("text");

        builder.Property(p => p.IsCompleted).HasColumnName("is_completed");
        builder.HasIndex(p => p.IsCompleted).HasDatabaseName("ix_tasks_is_completed");

        builder.Property(p => p.CreatedAt).HasColumnName("created_at");

        builder.Property(p => p.Version).IsRowVersion();
    }
}
