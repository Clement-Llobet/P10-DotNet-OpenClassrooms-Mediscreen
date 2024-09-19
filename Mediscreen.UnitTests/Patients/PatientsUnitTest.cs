using Mediscreen.Domain.Patient;
using Mediscreen.Domain.Patient.Contracts;
using Mediscreen.Domain.Patient.Dto;
using Moq;

namespace Mediscreen.UnitTests.Patients;

public class PatientsUnitTest
{
    [Fact]
    public void ListPatients_ReturnsListOfPatientOutputs()
    {
        // Arrange
        var patientRepository = new Mock<IPatientRepository>();
        var dataRetrievalMethod = new Mock<Func<List<IPatient>>>();

        var patient1 = new Mock<IPatient>();
        patient1.Setup(x => x.Id).Returns(1);
        patient1.Setup(x => x.FirstName).Returns("John");
        patient1.Setup(x => x.LastName).Returns("Doe");
        patient1.Setup(x => x.BirthDate).Returns(new DateTime(1980, 1, 1));
        patient1.Setup(x => x.Gender).Returns("M");
        patient1.Setup(x => x.HomeAddress).Returns("123 Main St");
        patient1.Setup(x => x.PhoneNumber).Returns("123-456-7890");

        var patient2 = new Mock<IPatient>();
        patient2.Setup(x => x.Id).Returns(2);
        patient2.Setup(x => x.FirstName).Returns("Jane");
        patient2.Setup(x => x.LastName).Returns("Doe");
        patient2.Setup(x => x.BirthDate).Returns(new DateTime(1980, 1, 1));
        patient2.Setup(x => x.Gender).Returns("F");
        patient2.Setup(x => x.HomeAddress).Returns("123 Main St");
        patient2.Setup(x => x.PhoneNumber).Returns("123-456-7890");

        var patient3 = new Mock<IPatient>();
        patient3.Setup(x => x.Id).Returns(2);
        patient3.Setup(x => x.FirstName).Returns("Josh");
        patient3.Setup(x => x.LastName).Returns("Doe");
        patient3.Setup(x => x.BirthDate).Returns(new DateTime(1980, 1, 1));
        patient3.Setup(x => x.Gender).Returns("M");
        patient3.Setup(x => x.HomeAddress).Returns("123 Main St");
        patient3.Setup(x => x.PhoneNumber).Returns("123-456-7890");

        var patients = new List<IPatient> { patient1.Object, patient2.Object, patient3.Object };
        dataRetrievalMethod.Setup(x => x()).Returns(patients);

        // Act
        var result = PatientManager.ListPatients(patientRepository.Object, dataRetrievalMethod.Object);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(patients.Count, result.Count);
        foreach (var patientOutput in result)
        {
            Assert.IsType<PatientOutput>(patientOutput);
        }
    }

    [Fact]
    public void GetPatient_WithExistingId_ReturnsPatientOutput()
    {
        // Arrange
        var patientRepositoryMock = new Mock<IPatientRepository>();
        var patientId = 1;

        var patient = new Mock<IPatient>();
        patient.Setup(x => x.Id).Returns(patientId);
        patient.Setup(x => x.FirstName).Returns("John");
        patient.Setup(x => x.LastName).Returns("Doe");
        patient.Setup(x => x.BirthDate).Returns(new DateTime(1980, 1, 1));
        patient.Setup(x => x.Gender).Returns("M");
        patient.Setup(x => x.HomeAddress).Returns("123 Main St");
        patient.Setup(x => x.PhoneNumber).Returns("123-456-7890");

        var patients = new List<IPatient> { patient.Object }.AsQueryable();
        patientRepositoryMock.As<IQueryable<IPatient>>().Setup(m => m.Provider).Returns(patients.Provider);
        patientRepositoryMock.As<IQueryable<IPatient>>().Setup(m => m.Expression).Returns(patients.Expression);
        patientRepositoryMock.As<IQueryable<IPatient>>().Setup(m => m.ElementType).Returns(patients.ElementType);
        patientRepositoryMock.As<IQueryable<IPatient>>().Setup(m => m.GetEnumerator()).Returns(patients.GetEnumerator());

        // Act
        var result = PatientManager.GetPatient(patientRepositoryMock.Object, patientId);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<PatientOutput>(result);
    }

    [Fact]
    public void GetPatient_WithNonExistingId_ThrowsException()
    {
        // Arrange
        var patientRepositoryMock = new Mock<IPatientRepository>();
        var patientId = 1;

        var patients = new List<IPatient> { }.AsQueryable();
        patientRepositoryMock.As<IQueryable<IPatient>>().Setup(m => m.Provider).Returns(patients.Provider);
        patientRepositoryMock.As<IQueryable<IPatient>>().Setup(m => m.Expression).Returns(patients.Expression);
        patientRepositoryMock.As<IQueryable<IPatient>>().Setup(m => m.ElementType).Returns(patients.ElementType);
        patientRepositoryMock.As<IQueryable<IPatient>>().Setup(m => m.GetEnumerator()).Returns(patients.GetEnumerator());

        // Act & Assert
        Assert.Throws<Exception>(() => PatientManager.GetPatient(patientRepositoryMock.Object, patientId));
    }

    [Fact]
    public void UpdatePatient_ReturnsTaskOfInt()
    {
        // Arrange
        var patientRepositoryMock = new Mock<IPatientRepository>();
        var patientInput = new PatientInput() { Id = 1 };

        // Act
        var result = PatientManager.UpdatePatient(patientRepositoryMock.Object, patientInput);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Task<int>>(result);
    }

    [Fact]
    public void CreatePatient_ReturnsTaskOfInt()
    {
        // Arrange
        var patientRepositoryMock = new Mock<IPatientRepository>();
        var patientInputCreate = new PatientInputCreate()
        {
            FirstName = "John",
            LastName = "Doe",
            BirthDate = new DateTime(1980, 1, 1),
            Gender = "M",
        };

        // Act
        var result = PatientManager.CreatePatient(patientRepositoryMock.Object, patientInputCreate);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Task<int>>(result);
    }
}