using Facillita.API.Models.Enum;

namespace Facillita.API.Models
{
    public class Extract
    {
        public string Name { get; set; }

        public double Amount { get; set; }

        public DateTime Date { get; set; }

        public int UserId { get; set; }
        public ExtractTypeEnum TypeId { get; set; }
    }
}
