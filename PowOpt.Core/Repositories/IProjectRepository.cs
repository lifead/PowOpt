﻿using PowOpt.Core.Models;

namespace PowOpt.Core.Repositories
{
    public interface IProjectRepository
    {
        ProjectDataDbo LoadProject(string filePath);
        void SaveProject(string filePath, ProjectDataDbo projectData); // Добавляем метод для сохранения проекта
    }
}