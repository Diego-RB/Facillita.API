using Facillita.API.Models.Enum;
using Newtonsoft.Json;

namespace Facillita.API.Data.Dtos.Financial
{
    public class ExtractDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("amount")]
        public double Amount { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("typeId")]
        public ExtractTypeEnum TypeId { get; set; }
    }
}
