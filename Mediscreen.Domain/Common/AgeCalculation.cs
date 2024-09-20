namespace Mediscreen.Domain.Common;

public static class AgeCalculation
{
    public static int CalculateAge(DateTime birthDate)
    {
        DateTime today = DateTime.Today;
        int patientAge = today.Year - birthDate.Year;

        if (birthDate > today.AddYears(-patientAge))
            patientAge--;

        if (birthDate > today)
            throw new ArgumentException("Birth date cannot be in the future");

        return patientAge;
    }
}
