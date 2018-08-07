using DbEntities.Interfaces;

namespace DbEntities.Models
{
    public class RequestType : IBase
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
