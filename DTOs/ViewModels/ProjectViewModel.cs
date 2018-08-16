using System;
using System.Collections.Generic;

namespace DTOs.ViewModels
{
    public class ProjectViewModel
    {
        public string Name { get; set; }

        public IEnumerable<BaseViewModel> Employees { get; set; }

        public string Status { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
