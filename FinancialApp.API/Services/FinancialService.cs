using AutoMapper;
using Facillita.API.Data.Dtos.Financial;
using Facillita.API.Interfaces.Repositories;
using Facillita.API.Interfaces.Services;
using Facillita.API.Models.FinancialSummary;

namespace Facillita.API.Services
{
    public class FinancialService : IFinancialService
    {
        private readonly IFinancialRepository _repository;
        private readonly IMapper _mapper;

        public FinancialService(IFinancialRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public JsonField MonthSummary(string userUId, int year, int month)
        {
            var totalIncome = _repository.TotalIncome(userUId, year, month);
            var totalExpense = _repository.TotalExpense(userUId, year, month);
            var balance = totalIncome - totalExpense;
            var listExpenseByCategory = _repository.CalculateExpensesByCategory(userUId, year, month);

            return new JsonField
            {
                TotalIncome = totalIncome,
                TotalExpense = totalExpense,
                Balance = balance,
                List = listExpenseByCategory
            };
        }

        public List<ExtractDto> GetExtract(string userUID)
        {
            return _mapper.Map<List<ExtractDto>>(_repository.GetExtrac(userUID));
        }

    }




}

