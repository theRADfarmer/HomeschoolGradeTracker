using HomeschoolGradeTracker.Application.Subjects;
using HomeschoolGradeTracker.Domain.Entities;
using HomeschoolGradeTracker.Infrastructure.Persistence;
using HomeschoolGradeTracker.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

public class SubjectService_IntegrationTests
{
    // Creates a fresh in-memory ApplicationDbContext seeded with test data.
    // This mimics a real database for integration testing without requiring a real SQL Server.
    private ApplicationDbContext CreateInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use unique DB instance per test
            .Options;

        var context = new ApplicationDbContext(options);

        // Seed with a subject to verify retrieval
        context.Subjects.Add(new Subject { Name = "History" });
        context.SaveChanges();

        return context;
    }

    [Fact]
    public async Task GetAllSubjects_ReturnsFromDb()
    {
        // Arrange: setup in-memory DB, repository, and service
        var dbContext = CreateInMemoryDbContext();

        // The real repository is wired up with the real EF Core context (but in-memory)
        var repository = new SubjectRepository(dbContext);

        // The service depends on the repository (not the DbContext directly anymore)
        var service = new SubjectService(repository);

        // Act: call the method under test
        var subjects = await service.GetAllSubjectsAsync();

        // Assert: validate the result
        Assert.Single(subjects);
        Assert.Equal("History", subjects.First().Name);
    }

    [Fact]
    public async Task AddSubject_AddsToDb()
    {
        // Arrange: setup in-memory DB, repository, and service
        var dbContext = CreateInMemoryDbContext();
        var repository = new SubjectRepository(dbContext);
        var service = new SubjectService(repository);

        // Explicitly clear the context to ensure no data is present before the test
        dbContext.Subjects.RemoveRange(dbContext.Subjects);
        await dbContext.SaveChangesAsync();

        // Act: call the method under test
        var newSubject = new Subject { Name = "Geography" };
        await service.AddSubjectAsync(newSubject);

        // Assert: validate the result
        var subjects = await dbContext.Subjects.ToListAsync();
        Assert.Single(subjects);
        Assert.Equal("Geography", subjects.First().Name);
    }
}

