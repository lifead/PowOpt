using PowOpt.Core.Models;

namespace PowOpt.Core.Services
{
    public class DataTransformationService
    {
        public ProjectDataViewData TransformToViewModel(ProjectDataDbo projectData)
        {
            var displayGroups = projectData.Groups.Select(group => new GroupViewData
            {
                Id = group.Id,
                GroupName = group.GroupName,
                Items = projectData.Parameters
                    .Where(param => param.GroupId == group.Id)
                    .Select(param => new ParameterViewData
                    {
                        Id = param.Id,
                        ParameterName = param.ParameterName,
                        GroupId = param.GroupId,
                        TypeId = param.TypeId,  // Устанавливаем TypeId
                        Value = param.Value,     // Устанавливаем Value
                        CalculatedValue = param.CalculatedValue  // Устанавливаем CalculatedValue
                    })
                    .ToList()
            }).ToList();

            return new ProjectDataViewData
            {
                Groups = displayGroups
            };
        }
    }
}
