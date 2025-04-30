using HomeschoolGradeTracker.Application.Interfaces;
using HomeschoolGradeTracker.Application.Subjects;
using HomeschoolGradeTracker.Domain.Entities;
using Moq;

namespace HomeschoolGradeTracker.UnitTests
{
    public class SubjectServiceTests
    {
        [Fact]
        public async Task GetSubjectsAsync_ReturnsSubjects()
        {
            // Arrange
            var subjects = new List<Subject>
                {
                    new Subject { Id = 1, Name = "Math" },
                    new Subject { Id = 2, Name = "Science" }
                };

            var mockSubjectRepository = new Mock<IRepository>();
            mockSubjectRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(subjects);

            var service = new SubjectService(mockSubjectRepository.Object); // Use the mocked ISubjectRepository

            // Act
            var result = await service.GetAllSubjectsAsync();

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task AddSubjectAsync_AddsSubject()
        {
            // Arrange
            var subject = new Subject { Id = 1, Name = "Math" };
            var mockSubjectRepository = new Mock<IRepository>();
            mockSubjectRepository.Setup(repo => repo.AddAsync(subject)).Returns(Task.CompletedTask);
            var service = new SubjectService(mockSubjectRepository.Object); // Use the mocked ISubjectRepository
            // Act
            await service.AddSubjectAsync(subject);
            // Assert
            mockSubjectRepository.Verify(repo => repo.AddAsync(subject), Times.Once);
        }
    }

}
