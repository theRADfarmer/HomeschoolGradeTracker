using HomeschoolGradeTracker.Application.Interfaces;
using HomeschoolGradeTracker.Application.Services;
using HomeschoolGradeTracker.Domain.Entities;
using Moq;

namespace HomeschoolGradeTracker.UnitTests
{
    public class AssignmentServiceTests
    {
        [Fact]
        public async Task GetAssignmentByIdAsync_ReturnsAssignment()
        {
            // Arrange
            var assignments = new List<Assignment>
            {
                new Assignment { Id = 1, 
                                 AssignmentName = "Lesson 1", 
                                 SubjectId = 1, 
                                 Description ="", 
                                 DateCompleted = new DateTime(2025, 1, 1), 
                                 Grade = 95 },
                new Assignment { Id = 2,
                                 AssignmentName = "Lesson 2",
                                 SubjectId = 1,
                                 Description ="Something",
                                 DateCompleted = new DateTime(2025, 1, 2),
                                 Grade = 90 }
            };
            var mockAssignmentRepository = new Mock<IAssignmentRepository>();

            mockAssignmentRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(assignments[0]);
            mockAssignmentRepository.Setup(repo => repo.GetByIdAsync(2)).ReturnsAsync(assignments[1]);

            var service = new AssignmentService(mockAssignmentRepository.Object); // Use the mocked IAssignmentRepository

            // Act
            var result1 = await service.GetAssignmentByIdAsync(1);
            var result2 = await service.GetAssignmentByIdAsync(2);

            // Assert
            Assert.NotNull(result1);
            Assert.Equal(1, result1.Id);
            Assert.Equal("Lesson 1", result1.AssignmentName);
            Assert.Equal(1, result1.SubjectId);
            Assert.Equal(new DateTime(2025, 1, 1), result1.DateCompleted);
            Assert.Equal(95, result1.Grade);
            Assert.Equal("", result1.Description);

            Assert.NotNull(result2);
            Assert.Equal(2, result2.Id);
            Assert.Equal("Lesson 2", result2.AssignmentName);
            Assert.Equal(1, result2.SubjectId);
            Assert.Equal(new DateTime(2025, 1, 2), result2.DateCompleted);
            Assert.Equal(90, result2.Grade);
            Assert.Equal("Something", result2.Description);
        }

        [Fact]
        public async Task GetAssignmentsBySubjectIdAsync_ReturnsAssignments()
        {
            // Arrange
            var assignments = new List<Assignment>
            {
                new Assignment { Id = 1, AssignmentName = "Lesson 1", SubjectId = 1 },
                new Assignment { Id = 2, AssignmentName = "Lesson 2", SubjectId = 1 },
                new Assignment { Id = 3, AssignmentName = "Lesson 3", SubjectId = 1 },
                new Assignment { Id = 1, AssignmentName = "Lesson 1", SubjectId = 2 },
                new Assignment { Id = 2, AssignmentName = "Lesson 2", SubjectId = 2 },
                new Assignment { Id = 3, AssignmentName = "Lesson 3", SubjectId = 2 }
            };
            var mockAssignmentRepository = new Mock<IAssignmentRepository>();

            mockAssignmentRepository.Setup(repo => repo.GetBySubjectIdAsync(1)).ReturnsAsync(assignments.Where(a => a.SubjectId == 1).ToList());

            var service = new AssignmentService(mockAssignmentRepository.Object); // Use the mocked IAssignmentRepository

            // Act
            var result = await service.GetAssignmentsBySubjectIdAsync(1);

            // Assert
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task AddAssignmentAsync_AddsAssignment()
        {
            // Arrange
            var assignment = new Assignment { Id = 1, AssignmentName = "Lesson 1", SubjectId = 1 };

            var mockAssignmentRepository = new Mock<IAssignmentRepository>();

            mockAssignmentRepository.Setup(repo => repo.AddAsync(assignment)).Returns(Task.CompletedTask);

            var service = new AssignmentService(mockAssignmentRepository.Object); // Use the mocked IAssignmentRepository

            // Act
            await service.AddAssignmentAsync(assignment);

            // Assert
            mockAssignmentRepository.Verify(repo => repo.AddAsync(assignment), Times.Once);
        }

        [Fact]
        public async Task UpdateAssignmentAsync_UpdatesAssignment()
        {
            // Arrange
            var existingAssignment = new Assignment { Id = 1, 
                                                      AssignmentName = "Lesson 1", 
                                                      Description = "",
                                                      DateCompleted = new DateTime(2025, 1, 1),
                                                      Grade = 95,
                                                      SubjectId = 1 };
            var updatedAssignment = new Assignment { Id = 1, 
                                                     AssignmentName = "Lesson 2", 
                                                     Description = "Project",
                                                     DateCompleted = new DateTime(2025, 1, 2),
                                                     Grade = 90,
                                                     SubjectId = 1 };

            var mockAssignmentRepository = new Mock<IAssignmentRepository>();

            mockAssignmentRepository.Setup(repo => repo.GetByIdAsync(existingAssignment.Id)).ReturnsAsync(existingAssignment);
            mockAssignmentRepository.Setup(repo => repo.UpdateAsync(existingAssignment)).Returns(Task.CompletedTask);

            var service = new AssignmentService(mockAssignmentRepository.Object); // Use the mocked IAssignmentRepository

            // Act
            await service.UpdateAssignmentAsync(updatedAssignment);

            // Assert
            Assert.Equal("Lesson 2", existingAssignment.AssignmentName);
            Assert.Equal("Project", existingAssignment.Description);
            Assert.Equal(new DateTime(2025, 1, 2), existingAssignment.DateCompleted);
            Assert.Equal(90, existingAssignment.Grade);
        }

        [Fact]
        public async Task DeleteAssignmentAsync_DeletesAssignment()
        {
            // Arrange
            var assignment = new Assignment { Id = 1, AssignmentName = "Lesson 1", SubjectId = 1 };

            var mockAssignmentRepository = new Mock<IAssignmentRepository>();

            mockAssignmentRepository.Setup(repo => repo.DeleteAsync(assignment.Id)).Returns(Task.CompletedTask);

            var service = new AssignmentService(mockAssignmentRepository.Object); // Use the mocked IAssignmentRepository

            // Act
            await service.DeleteAssignmentAsync(assignment.Id);

            // Assert
            mockAssignmentRepository.Verify(repo => repo.DeleteAsync(assignment.Id), Times.Once);
        }
    }
}
