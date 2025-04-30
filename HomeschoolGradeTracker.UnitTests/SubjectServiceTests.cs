using HomeschoolGradeTracker.Application.Interfaces;
using HomeschoolGradeTracker.Application.Services;
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

        [Fact]
        public async Task UpdateSubjectAsync_UpdatesSubject()
        {
            // Arrange
            var existingSubject = new Subject { Id = 1, Name = "Math" };
            var updatedSubject = new Subject { Id = 1, Name = "Advanced Math" };
            var mockSubjectRepository = new Mock<IRepository>();
            mockSubjectRepository.Setup(repo => repo.GetByIdAsync(existingSubject.Id)).ReturnsAsync(existingSubject);
            mockSubjectRepository.Setup(repo => repo.UpdateAsync(existingSubject)).Returns(Task.CompletedTask);
            var service = new SubjectService(mockSubjectRepository.Object); // Use the mocked ISubjectRepository

            // Act
            await service.UpdateSubjectAsync(updatedSubject);

            // Assert
            Assert.Equal("Advanced Math", existingSubject.Name);
        }

        [Fact]
        public async Task DeleteSubjectAsync_DeletesSubject()
        {
            // Arrange
            var subject = new Subject { Id = 1, Name = "Math" };
            var mockSubjectRepository = new Mock<IRepository>();
            mockSubjectRepository.Setup(repo => repo.GetByIdAsync(subject.Id)).ReturnsAsync(subject);
            mockSubjectRepository.Setup(repo => repo.DeleteAsync(subject)).Returns(Task.CompletedTask);
            var service = new SubjectService(mockSubjectRepository.Object); // Use the mocked ISubjectRepository

            // Act
            await service.DeleteSubjectAsync(subject.Id);

            // Assert
            mockSubjectRepository.Verify(repo => repo.DeleteAsync(subject), Times.Once);
        }
    }

}
