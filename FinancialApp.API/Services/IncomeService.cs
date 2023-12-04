using AutoMapper;
using Facillita.API.Data;
using Facillita.API.Data.Dtos.Income;
using Facillita.API.Interfaces.Repositories;
using Facillita.API.Interfaces.Services;
using Facillita.API.Models;
using FluentResults;
using System.Globalization;

namespace Facillita.API.Services
{
    public class IncomeService : IIncomeService
    {
        private FinancialContext _context;
        private IMapper _mapper;
        private IIncomeRepository _repository;
        private readonly IUserRepository _userRepository;

        public IncomeService(
            FinancialContext context,
            IMapper mapper,
            IIncomeRepository repository,
            IUserRepository userRepository)
        {
            _context = context;
            _mapper = mapper;
            _repository = repository;
            _userRepository = userRepository;
        }

        public ReadIncomeDto AddIncome(CreateIncomeDto incomeDto)
        {
            Income income = _mapper.Map<Income>(incomeDto);
            var user = _userRepository.GetUserByUID(incomeDto.UserUID);

            if (user == null)
                return null;

            income.User = user;
            //Verifies if an income with same name exists in the same month 
            var searchSameName = _repository.SearchSameName(incomeDto);

            ////If there isn't an income with same in the same month, it'll be allowed to be add
            if (searchSameName.Count() == 0)
            {
                _context.Incomes.Add(income);
                _context.SaveChanges();
                return _mapper.Map<ReadIncomeDto>(income);
            }
            return null;
        }

        public List<ReadIncomeDto> ListIncomes(string userUId)
        {
            List<Income> incomes = _context.Incomes.Where(x => x.User.UID == userUId).ToList();
            if (incomes != null)
            {
                List<ReadIncomeDto> readDto = _mapper.Map<List<ReadIncomeDto>>(incomes);
                return readDto;
            }
            return null;
        }

        public ReadIncomeDto ListIncomeById(int id)
        {
            Income income = _repository.GetIncomeById(id);
            if (income != null)
            {
                ReadIncomeDto readDto = _mapper.Map<ReadIncomeDto>(income);
                return readDto;
            }
            return null;
        }

        public List<ReadIncomeDto> ListIncomeByDescription(string userUId, string description)
        {
            List<ReadIncomeDto> incomesByDescription = new List<ReadIncomeDto>();
            var queryDescription = _repository.SearchSameDescription(userUId, description);
            if (queryDescription != null)
            {
                foreach (var income in queryDescription)
                {
                    ReadIncomeDto incomeDto = _mapper.Map<ReadIncomeDto>(income);
                    incomesByDescription.Add(incomeDto);
                }
                return incomesByDescription;
            }
            return null;
        }

        public List<ReadIncomeDto> ListIncomeByMonthOfYear(string userUId, int year, int month)
        {
            List<ReadIncomeDto> incomesByMonthOfYear = new List<ReadIncomeDto>();
            var queryYearAndMonth = _repository.SearchMonthOfYear(userUId, year, month);
            if (queryYearAndMonth != null)
            {
                foreach (var income in queryYearAndMonth)
                {
                    ReadIncomeDto incomeDto = _mapper.Map<ReadIncomeDto>(income);
                    incomesByMonthOfYear.Add(incomeDto);
                }
                return incomesByMonthOfYear;
            }
            return null;
        }

        public Result UpdateIncome(int id, UpdateIncomeDto updatedIncomeDto)
        {
            Income income = _repository.GetIncomeById(id);

            //Verifies if an income with same name exists in the same month 

            var searchSameName = _repository.SearchSameName(updatedIncomeDto);

            if (income != null)
            {
                //If there isn't other income with same name except for the one being changed, it'll be allowed to be updated
                if (searchSameName.Count() == 0 || searchSameName.Select(inc => inc.IncomeName).Contains(income.IncomeName))
                {
                    _mapper.Map<ReadIncomeDto>(income);
                    _mapper.Map(updatedIncomeDto, income);
                    _context.SaveChanges();
                    return Result.Ok();
                }
                return Result.Fail($"Income with same name already exists in {CultureInfo.GetCultureInfo("en-Us").DateTimeFormat.GetMonthName(updatedIncomeDto.IncomeDate.Month)}");
            }
            return Result.Fail("Income not found");
        }

        public Result DeleteIncome(int id)
        {
            Income income = _repository.GetIncomeById(id);
            if (income != null)
            {
                _context.Remove(income);
                _context.SaveChanges();
                return Result.Ok();
            }
            return Result.Fail("Income not found");
        }


    }
}
