using Xunit;

public class TaskDomainTests
{
    [Fact]
    public void CompleteTask_ShouldUpdateStatusAndUpdatedAt()
    {
        // Arrange
        var task = new Task
        {
            Title = "New Task",
            Description = "Task Description",
            DueDate = DateTime.Now.AddDays(1),
            Priority = 1,
            CategoryId = 1
        };

        // Act
        task.CompleteTask();

        // Assert
        Assert.Equal("completed", task.Status);
        Assert.True(task.UpdatedAt > task.CreatedAt);
    }
}
