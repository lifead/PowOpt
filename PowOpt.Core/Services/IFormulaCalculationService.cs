using PowOpt.Core.Models;

namespace PowOpt.Core.Services;

public interface IFormulaCalculationService
{
    public ProjectDataDbo Calculate(ProjectDataDbo projectData);
}
