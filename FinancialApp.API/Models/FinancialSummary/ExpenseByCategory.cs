using Newtonsoft.Json;

namespace Facillita.API.Models.FinancialSummary
{
    public class ExpenseByCategory
    {
        [JsonProperty("categoryId")]
        public int CategoryId { get; set; }
        [JsonProperty("totalCategoryIdExpense")]
        public double Total { get; set; }
    }
}
