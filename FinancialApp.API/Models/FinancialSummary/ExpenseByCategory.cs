using Newtonsoft.Json;

namespace FinancialApp.API.Models.FinancialSummary
{
    public class ExpenseByCategory
    {
        [JsonProperty("CategoryId")]
        public int CategoryId { get; set; }
        [JsonProperty("TotalCategoryIdExpense")]
        public double Total { get; set; }
    }
}
