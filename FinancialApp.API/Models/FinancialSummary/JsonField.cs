using Newtonsoft.Json;

namespace Facillita.API.Models.FinancialSummary
{
    public class JsonField
    {
        [JsonProperty("totalIncome")]
        public double TotalIncome { get; set; }
        [JsonProperty("totalExpense")]
        public double TotalExpense { get; set; }
        [JsonProperty("balance")]
        public double Balance { get; set; }
        [JsonProperty("expensesByCategory")]
        public List<ExpenseByCategory> List { get; set; }
    }
}
