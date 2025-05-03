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
                    new Subject { Id = 1, Name = "Math", Description = "Math-U-See Level Alpha" },
                    new Subject { Id = 2, Name = "Science" }
                };

            var mockSubjectRepository = new Mock<ISubjectRepository>();
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
            var subjectWithDescription = new Subject { Id = 1, Name = "Math", Description = "Math-U-See Level Alpha" };
            var subjectWithoutDescription = new Subject { Id = 1, Name = "Math" };

            var mockSubjectRepository = new Mock<ISubjectRepository>();
            mockSubjectRepository.Setup(repo => repo.AddAsync(subjectWithDescription)).Returns(Task.CompletedTask);
            mockSubjectRepository.Setup(repo => repo.AddAsync(subjectWithoutDescription)).Returns(Task.CompletedTask);

            var service = new SubjectService(mockSubjectRepository.Object); // Use the mocked ISubjectRepository
            // Act
            await service.AddSubjectAsync(subjectWithDescription);
            await service.AddSubjectAsync(subjectWithoutDescription);
            // Assert
            mockSubjectRepository.Verify(repo => repo.AddAsync(subjectWithDescription), Times.Once);
            mockSubjectRepository.Verify(repo => repo.AddAsync(subjectWithoutDescription), Times.Once);
        }

        [Fact]
        public async Task UpdateSubjectAsync_UpdatesSubject()
        {
            // Arrange
            var existingSubject = new Subject { Id = 1, Name = "Math", Description = "Math-U-See Level Alpha" };
            var updatedSubject = new Subject { Id = 1, Name = "Advanced Math", Description = "Math-U-See Pre-Calculus" };

            var mockSubjectRepository = new Mock<ISubjectRepository>();

            mockSubjectRepository.Setup(repo => repo.GetByIdAsync(existingSubject.Id)).ReturnsAsync(existingSubject);
            mockSubjectRepository.Setup(repo => repo.UpdateAsync(existingSubject)).Returns(Task.CompletedTask);

            var service = new SubjectService(mockSubjectRepository.Object); // Use the mocked ISubjectRepository

            // Act
            await service.UpdateSubjectAsync(updatedSubject);

            // Assert
            Assert.Equal("Advanced Math", existingSubject.Name);
            Assert.Equal("Math-U-See Pre-Calculus", existingSubject.Description);
        }

        [Fact]
        public async Task DeleteSubjectAsync_DeletesSubject()
        {
            // Arrange
            var subject = new Subject { Id = 1, Name = "Math", Description = "Math-U-See Level Alpha" };
            var mockSubjectRepository = new Mock<ISubjectRepository>();
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
