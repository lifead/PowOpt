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
                GroupName = group.GroupName,
                Items = projectData.Parameters
                    .Where(param => param.GroupId == group.Id)
                    .Select(param => new ParameterViewModel
                    {
                        Id = param.Id,
                        ParameterName = param.ParameterName
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
