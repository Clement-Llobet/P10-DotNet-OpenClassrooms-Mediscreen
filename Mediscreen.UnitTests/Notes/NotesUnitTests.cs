using Mediscreen.Domain.Note;
using Mediscreen.Domain.Note.Contracts;
using Mediscreen.Domain.Note.Dto;
using Moq;
using Xunit;

namespace Mediscreen.UnitTests
{
    public class NotesUnitTests
    {
        [Fact]
        public async Task ListNotesFromPatientAsync_ReturnsListOfNotes()
        {
            // Arrange
            int patientId = 1;

            //var noteOutput1 = new NotesOutput { NoteId = 1, PatientId = patientId, Comment = "Note 1" };
            //var noteOutput2 = new NotesOutput { NoteId = 2, PatientId = patientId, Comment = "Note 2" };
            //var noteOutput3 = new NotesOutput { NoteId = 3, PatientId = patientId, Comment = "Note 3" };

            var note1 = new Mock<INotes>();
            note1.Setup(x => x.NoteId).Returns(1);
            note1.Setup(x => x.PatientId).Returns(patientId);
            note1.Setup(x => x.Comment).Returns("Note 1");
            note1.Setup(x => x.Triggers).Returns(new List<ITriggers> { });
            note1.Setup(x => x.DoctorId).Returns("Doctor Who");

            var note2 = new Mock<INotes>();
            note2.Setup(x => x.NoteId).Returns(2);
            note2.Setup(x => x.PatientId).Returns(patientId);
            note2.Setup(x => x.Comment).Returns("Note 2");

            var note3 = new Mock<INotes>();
            note3.Setup(x => x.NoteId).Returns(3);
            note3.Setup(x => x.PatientId).Returns(patientId);
            note3.Setup(x => x.Comment).Returns("Note 3");

            var notesList = new List<INotes> { note1.Object, note2.Object, note3.Object };

            var noteRepositoryMock = new Mock<INotesRepository>();
            noteRepositoryMock.Setup(repo => repo.GetNotesAsync(patientId)).ReturnsAsync(notesList);

            // Act
            var result = await NoteManager.ListNotesFromPatientAsync(noteRepositoryMock.Object, patientId);

            // Assert
            Assert.Equal(notesList.Count, result.Count());
            Assert.Equal(notesList.Select(n => n.Comment), result.Select(n => n.Comment));
        }

        [Fact]
        public async Task GetNoteAsync_ReturnsNote()
        {
            // Arrange
            int noteId = 1;
            var expectedNote = new NotesOutput { NoteId = noteId, PatientId = 1, Comment = "Note 1" };

            var noteRepositoryMock = new Mock<INotesRepository>();

            // Act
            var result = await NoteManager.GetNoteAsync(noteRepositoryMock.Object, noteId);

            // Assert
            Assert.Equal(expectedNote.Comment, result.Comment);
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
                Triggers = new List<string> { "Trigger 1", "Trigger 2" },
                CreatedDate = DateTime.Now
            };

            var noteRepositoryMock = new Mock<INotesRepository>();

            // Act
            await NoteManager.CreateNoteAsync(noteRepositoryMock.Object, note);

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
                Triggers = new List<string> { "Trigger 1", "Trigger 2" },
                Practitioner = "Doctor Who"
            };

            var noteRepositoryMock = new Mock<INotesRepository>();

            // Act
            await NoteManager.UpdateNoteAsync(noteRepositoryMock.Object, noteInput, noteId);

            // Assert
            noteRepositoryMock.Verify(repo => repo.UpdateNoteAsync(noteInput, noteId), Times.Once);
        }
    }
}