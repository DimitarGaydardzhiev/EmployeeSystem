﻿using DTOs.ViewModels;
using System.Collections.Generic;

namespace ServiceLayer.Interfaces
{
    public interface IProjectService
    {
        IEnumerable<ProjectViewModel> GetCompanyProjects();
    }
}
