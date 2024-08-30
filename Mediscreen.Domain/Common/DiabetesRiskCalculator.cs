using Mediscreen.Domain.Note.Contracts;
using Mediscreen.Domain.Patient.Contracts;

namespace Mediscreen.Domain.Common;

public static class DiabetesRiskCalculator
{
    public static RiskLevel CalculateRiskLevel(IPatient patient, List<Triggers> triggersList)
    {
        DateTime today = DateTime.Today;
        int patientAge = today.Year - patient.BirthDate.Year;

        if (patient.BirthDate > today.AddYears(-patientAge))
            patientAge--;

        if (patient.BirthDate > today)
            throw new ArgumentException("Birth date cannot be in the future");

        int triggersCount = triggersList.Count;

        switch (patientAge)
        {
            case < 30:
                return CheckRiskLevelUnderAgeThirty(triggersCount);
            case >= 30:
                return CheckRiskLevelOverThirty(triggersCount);
            default:
        }

        RiskLevel CheckRiskLevelUnderAgeThirty(int triggersCount)
        {
            if (patient.Gender == "M")
            {
                if (triggersCount == 0)
                    return RiskLevel.None;
                if (triggersCount >= 3 && triggersCount <= 4)
                    return RiskLevel.InDanger;
                if (triggersCount >= 5)
                    return RiskLevel.EarlyOnset;
            }
            else
            {
                if (triggersCount >= 4 && triggersCount <= 6)
                    return RiskLevel.InDanger;
                if (triggersCount >= 7)
                    return RiskLevel.EarlyOnset;
            }
            return RiskLevel.None;
        }

        RiskLevel CheckRiskLevelOverThirty(int triggersCount)
        {
            if (triggersCount == 0)
                return RiskLevel.None;
            if (triggersCount == 2)
                return RiskLevel.Borderline;
            if (triggersCount >= 6 && triggersCount <= 7)
                return RiskLevel.InDanger;
            if (triggersCount >= 8)
                return RiskLevel.EarlyOnset;

            return RiskLevel.None;
        }
    }
}
