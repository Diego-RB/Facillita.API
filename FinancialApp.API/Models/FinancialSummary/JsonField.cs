using Newtonsoft.Json;

namespace FinancialApp.API.Models.FinancialSummary
{
    public class JsonField
    {
        [JsonProperty("TotalIncome")]
        public double TotalIncome { get; set; }
        [JsonProperty("TotalExpense")]
        public double TotalExpense { get; set; }
        [JsonProperty("Balance")]
        public double Balance { get; set; }
        [JsonProperty("ExpensesByCategory")]
        public List<ExpenseByCategory> List { get; set; }
    }
}
