using Mediscreen.Domain.Patient.Contracts;

namespace Mediscreen.Domain.Common;

public static class DiabetesRiskCalculator
{
    public static RiskLevel CalculateRiskLevel(IPatient patient, int triggersListCount)
    {
        int patientAge = AgeCalculation.CalculateAge(patient.BirthDate);

        switch (patientAge)
        {
            case < 30:
                return CheckRiskLevelUnderAgeThirty(triggersListCount);
            case >= 30:
                return CheckRiskLevelOverAgeThirty(triggersListCount);
            default:
        }

        RiskLevel CheckRiskLevelUnderAgeThirty(int triggersListCount)
        {
            if (patient.Gender == "M")
            {
                if (triggersListCount == 0)
                    return RiskLevel.None;
                if (triggersListCount >= 3 && triggersListCount <= 4)
                    return RiskLevel.InDanger;
                if (triggersListCount >= 5)
                    return RiskLevel.EarlyOnset;
            }
            else
            {
                if (triggersListCount >= 4 && triggersListCount <= 6)
                    return RiskLevel.InDanger;
                if (triggersListCount >= 7)
                    return RiskLevel.EarlyOnset;
            }
            return RiskLevel.None;
        }

        RiskLevel CheckRiskLevelOverAgeThirty(int triggersListCount)
        {
            switch (triggersListCount)
            {
                case 0:
                    return RiskLevel.None;
                case >= 2 and <= 5:
                    return RiskLevel.Borderline;
                case >= 6 and <= 7:
                    return RiskLevel.InDanger;
                case >= 8:
                    return RiskLevel.EarlyOnset;
                default:
                    return RiskLevel.None;
            }
        }
    }
}
