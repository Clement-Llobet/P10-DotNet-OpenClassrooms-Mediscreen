using Mediscreen.Domain.Note;
using Mediscreen.Domain.Note.Contracts;
using Mediscreen.Domain.Note.Dto;
using Mediscreen.Domain.Patient.Contracts;
using Mediscreen.Domain.Triggers.Contracts;
using Mediscreen.Domain.Triggers.Dto;
using Mediscreen.Infrastructure.SqlServerDatabase.Entities;
using Moq;
using Xunit;

namespace Mediscreen.UnitTests
{
    public class NotesUnitTests
    {
        [Fact]
        public async Task ListNotesFromPatientAsync_ShouldReturnListOfNotes()
        {
            // Arrange
            int patientId = 1;
            var expectedNotes = new List<NotesOutput>
                {
                    new() { NoteId = 1, PatientId = 1, Comment = "Note 1" },
                    new() { NoteId = 2, PatientId = 1, Comment = "Note 2" },
                    new() { NoteId = 3, PatientId = 1, Comment = "Note 3" }
                };

            var noteRepositoryMock = new Mock<INotesRepository>();
            noteRepositoryMock.Setup(repo => repo.GetAllNotesAsync(patientId))
                .ReturnsAsync(expectedNotes);

            // Act
            var result = await NoteManager.ListNotesFromPatientAsync(noteRepositoryMock.Object, patientId);

            // Assert
            Assert.Equal(expectedNotes, result);
        }

        [Fact]
        public async Task GetNoteAsync_ShouldReturnCorrectData()
        {
            // Arrange
            int noteId = 1;
            var mockNotesRepository = new Mock<INotesRepository>();

            var mockPatient = new Mock<IPatient>();
            var mockNote = new Mock<INotes>();
            var mockTriggers = new List<ITriggers> { new Mock<ITriggers>().Object, new Mock<ITriggers>().Object };

            mockNotesRepository.Setup(repo => repo.GetNoteAsync(noteId))
                .ReturnsAsync((mockPatient.Object, mockNote.Object, mockTriggers));

            // Act
            var result = await GetNoteAsync(mockNotesRepository.Object, noteId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NotesOutput>(result);

            mockNotesRepository.Verify(repo => repo.GetNoteAsync(noteId), Times.Once);
        }

        private static async Task<NotesOutput> GetNoteAsync(INotesRepository noteRepository, int noteId)
        {
            var notesRepositoryDatas = await noteRepository.GetNoteAsync(noteId);
            var patient = notesRepositoryDatas.Item1;
            var note = notesRepositoryDatas.Item2;
            var triggers = notesRepositoryDatas.Item3.ToList();
            return NotesOutput.Render(patient!, note, triggers);
        }

        [Fact]
        public async Task CreateNoteAsync_CallsCreateNoteAsync()
        {
            // Arrange
            var note = new NotesCreateInput
            {
                NoteId = 1,
                PatientId = 1,
                Comment = "New Note",
                Triggers = new List<int> { 1, 2 },
                CreatedDate = DateTime.Now
            };

            var noteRepositoryMock = new Mock<INotesRepository>();
            var triggerRepositoryMock = new Mock<ITriggersRepository>();

            // Act
            await NoteManager.CreateNoteAsync(noteRepositoryMock.Object, triggerRepositoryMock.Object, note);

            // Assert
            noteRepositoryMock.Verify(repo => repo.CreateNoteAsync(note), Times.Once);
        }

        [Fact]
        public async Task UpdateNoteAsync_CallsUpdateNoteAsync()
        {
            // Arrange
            int noteId = 1;
            var noteInput = new NotesUpdateInput
            {
                PatientId = 1,
                Comment = "Updated Note",
                CurrentDateTime = DateTime.Now,
                Triggers = new List<int> { 1, 2 },
                Practitioner = "Doctor Who"
            };

            var noteRepositoryMock = new Mock<INotesRepository>();
            var triggerRepositoryMock = new Mock<ITriggersRepository>();

            // Act
            await NoteManager.UpdateNoteAsync(noteRepositoryMock.Object, triggerRepositoryMock.Object, noteInput, noteId);

            // Assert
            noteRepositoryMock.Verify(repo => repo.UpdateNoteAsync(noteInput, noteId), Times.Once);
        }
    }
}