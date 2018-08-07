using DbEntities.Interfaces;

namespace DbEntities.Models
{
    public class EmployeeUserProject : IBase
    {
        public int Id { get; set; }

        public int EmployeeUserId { get; set; }

        public int ProjectId { get; set; }

        public EmployeeUser EmployeeUser { get; set; }

        public Project Project { get; set; }
    }
}
