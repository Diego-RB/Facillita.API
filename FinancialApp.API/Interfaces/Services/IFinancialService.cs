﻿using Facillita.API.Models.FinancialSummary;

namespace Facillita.API.Interfaces.Services
{
    public interface IFinancialService
    {
        public JsonField MonthSummary(string userUId, int year, int month);
    }
}
