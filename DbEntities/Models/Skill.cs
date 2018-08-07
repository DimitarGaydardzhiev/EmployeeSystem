using DbEntities.Interfaces;

namespace DbEntities.Models
{
    public class Skill : IBase
    {
        public int Id { get; set; }

        public int Name { get; set; }
    }
}
