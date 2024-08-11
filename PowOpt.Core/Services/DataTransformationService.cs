using System.Collections.Generic;
using System.Linq;
using PowOpt.Core.Models;

namespace PowOpt.Core.Services
{
    public class DataTransformationService
    {
        public ProjectDataViewModel TransformToViewModel(ProjectDataDbo projectData)
        {
            var displayGroups = projectData.Groups.Select(group => new GroupViewModel
            {
                Id = group.Id,
                GroupName = group.GroupName,
                Items = projectData.Parameters
                    .Where(param => param.GroupId == group.Id)
                    .Select(param => new ParameterViewModel
                    {
                        Id = param.Id,
                        ParameterName = param.ParameterName,
                        GroupId = param.GroupId,
                        TypeId = param.TypeId,  // Устанавливаем TypeId
                        Value = param.Value     // Устанавливаем Value
                    })
                    .ToList()
            }).ToList();

            return new ProjectDataViewModel
            {
                Groups = displayGroups
            };
        }
    }
}
