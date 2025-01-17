﻿using Facillita.API.Data.Dtos.Financial;
using Facillita.API.Models.Enum;
using Facillita.API.Models.FinancialSummary;

namespace Facillita.API.Interfaces.Services
{
    public interface IFinancialService
    {
        public JsonField MonthSummary(string userUId, int year, int month);
        public List<ExtractDto> GetExtract(string userUID, DateTime startDate, DateTime endDate, ExtractTypeEnum typeEnum);
        public List<ExtractDto> GetExtracByMonth(string userUID, int year, int month, ExtractTypeEnum typeEnum);
    }
}
