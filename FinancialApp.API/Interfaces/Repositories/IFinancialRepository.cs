﻿using Facillita.API.Models;
using Facillita.API.Models.FinancialSummary;

namespace Facillita.API.Interfaces.Repositories
{
    public interface IFinancialRepository
    {
        public double TotalExpense(string userUId, int year, int month);
        public double TotalIncome(string userUId, int year, int month);
        public List<ExpenseByCategory> CalculateExpensesByCategory(string userUId, int year, int month);
        public List<Extract> GetExtrac(string userUID);

    }
}
