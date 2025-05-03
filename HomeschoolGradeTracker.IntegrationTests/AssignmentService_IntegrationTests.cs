using HomeschoolGradeTracker.Application.Services;
using HomeschoolGradeTracker.Domain.Entities;
using HomeschoolGradeTracker.Infrastructure.Persistence;
using HomeschoolGradeTracker.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class AssignmentService_IntegrationTests
{
    private ApplicationDbContext CreateInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new ApplicationDbContext(options);

        // Seed a subject since assignments require a valid subject
        context.Subjects.Add(new Subject { Id = 1, Name = "Math" });
        context.SaveChanges();

        return context;
    }

    [Fact]
    public async Task AddAssignment_AddsToDb()
    {
        var dbContext = CreateInMemoryDbContext();
        var repo = new AssignmentRepository(dbContext);
        var service = new AssignmentService(repo);

        var assignment = new Assignment
        {
            AssignmentName = "Worksheet 1",
            Description = "Basic Addition",
            SubjectId = 1,
            Grade = 95,
            DateCompleted = DateTime.Today
        };

        await service.AddAssignmentAsync(assignment);

        var fromDb = await dbContext.Assignments.FirstOrDefaultAsync();
        Assert.NotNull(fromDb);
        Assert.Equal("Worksheet 1", fromDb!.AssignmentName);
        Assert.Equal(95, fromDb.Grade);
        Assert.Equal(1, fromDb.SubjectId);
    }

    [Fact]
    public async Task GetAssignmentsBySubjectId_ReturnsCorrectAssignments()
    {
        var dbContext = CreateInMemoryDbContext();
        var repo = new AssignmentRepository(dbContext);
        var service = new AssignmentService(repo);

        dbContext.Assignments.AddRange(
            new Assignment { AssignmentName = "Test 1", SubjectId = 1 },
            new Assignment { AssignmentName = "Test 2", SubjectId = 1 },
            new Assignment { AssignmentName = "Test 3", SubjectId = 99 } // wrong subject
        );
        await dbContext.SaveChangesAsync();

        var assignments = await service.GetAssignmentsBySubjectIdAsync(1);

        Assert.Equal(2, assignments.Count);
        Assert.All(assignments, a => Assert.Equal(1, a.SubjectId));
    }

    [Fact]
    public async Task UpdateAssignment_UpdatesInDb()
    {
        var dbContext = CreateInMemoryDbContext();
        var repo = new AssignmentRepository(dbContext);
        var service = new AssignmentService(repo);

        var assignment = new Assignment
        {
            AssignmentName = "Original",
            SubjectId = 1,
            Grade = 70
        };
        dbContext.Assignments.Add(assignment);
        await dbContext.SaveChangesAsync();

        assignment.AssignmentName = "Updated";
        assignment.Grade = 100;

        await service.UpdateAssignmentAsync(assignment);

        var updated = await dbContext.Assignments.FindAsync(assignment.Id);
        Assert.Equal("Updated", updated!.AssignmentName);
        Assert.Equal(100, updated.Grade);
    }

    [Fact]
    public async Task DeleteAssignment_RemovesFromDb()
    {
        var dbContext = CreateInMemoryDbContext();
        var repo = new AssignmentRepository(dbContext);
        var service = new AssignmentService(repo);

        var assignment = new Assignment { AssignmentName = "To Delete", SubjectId = 1 };
        dbContext.Assignments.Add(assignment);
        await dbContext.SaveChangesAsync();

        await service.DeleteAssignmentAsync(assignment.Id);

        var deleted = await dbContext.Assignments.FindAsync(assignment.Id);
        Assert.Null(deleted);
    }
}
