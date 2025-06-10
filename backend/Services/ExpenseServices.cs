using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Dto;
using backend.Models;
using backend.Repositories.IRepositories;
using backend.Services.IServices;

namespace backend.Services
{
    public class ExpenseServices : IExpenseServices
    {
        private readonly IExpenseRepository _repository;
        private readonly IExpenseShareServices _expenseShareService;


        public ExpenseServices(IExpenseRepository repository, IExpenseShareServices expenseShareService)
        {
            _repository = repository;
            _expenseShareService = expenseShareService;
        }

        public async Task<List<ExpenseDTO>> GetAllAsync()
        {
            var expenses = await _repository.GetAllAsync();
            var dtoList = new List<ExpenseDTO>();

            foreach (var exp in expenses)
            {
                dtoList.Add(new ExpenseDTO
                {
                    Id = exp.Id,
                    Titolo = exp.Titolo,
                    ImportoTotale = exp.ImportoTotale,
                    Data = exp.Data,
                    CreatoreId = exp.CreatoreId,
                    GruppoId = exp.GruppoId
                   
                });
            }

            return dtoList;
        }

        public async Task<ExpenseDTO> GetByIdAsync(int id)
        {
            var exp = await _repository.GetByIdAsync(id);
            if (exp == null) return null;

            var shares = await _expenseShareService.GetByExpenseIdAsync(exp.Id);

            return new ExpenseDTO
            {
                Id = exp.Id,
                Titolo = exp.Titolo,
                ImportoTotale = exp.ImportoTotale,
                Data = exp.Data,
                CreatoreId = exp.CreatoreId,
                GruppoId = exp.GruppoId,
                Quote = shares.Select(s => new ExpenseShareDTO
                {
                    
                    UserId = s.UserId,
                    Importo = s.Importo
                }).ToList()
            };
        }


        public async Task<ExpenseDTO> CreateAsync(ExpenseDTO dto)
        {
            var expense = new Expense
            {
                Titolo = dto.Titolo,
                ImportoTotale = dto.ImportoTotale,
                Data = dto.Data == default ? DateTime.UtcNow : dto.Data,
                CreatoreId = dto.CreatoreId,
                GruppoId = dto.GruppoId
            };

            await _repository.AddAsync(expense);
            await _repository.SaveChangesAsync();

            dto.Id = expense.Id;

            if (dto.Quote != null && dto.Quote.Any())
            {
                var shares = dto.Quote.Select(q => new ExpenseShare
                {
                    ExpenseId = expense.Id,
                    UserId = q.UserId,
                    Importo = q.Importo
                }).ToList();

                await _expenseShareService.AddOrUpdateSharesAsync(expense.Id, shares);
            }

            return dto;
        }


        public async Task<ExpenseDTO> UpdateAsync(ExpenseDTO dto)
        {
            var expense = await _repository.GetByIdAsync(dto.Id);
            if (expense == null)
                throw new Exception($"Nessuna spesa trovata con ID {dto.Id}");

            expense.Titolo = dto.Titolo;
            expense.ImportoTotale = dto.ImportoTotale;
            expense.Data = dto.Data;
            expense.CreatoreId = dto.CreatoreId;
            expense.GruppoId = dto.GruppoId;

            await _repository.UpdateAsync(expense);
            await _repository.SaveChangesAsync();

            if (dto.Quote != null && dto.Quote.Any())
            {
                var updatedShares = dto.Quote.Select(q => new ExpenseShare
                {
                    ExpenseId = dto.Id,
                    UserId = q.UserId,
                    Importo = q.Importo
                }).ToList();

                await _expenseShareService.AddOrUpdateSharesAsync(dto.Id, updatedShares);
            }

            return dto;
        }


        public async Task DeleteAsync(int id)
        {
            var expense = await _repository.GetByIdAsync(id);
            if (expense == null)
                throw new Exception($"Nessuna spesa trovata con ID {id}");

            await _repository.DeleteAsync(id);
            await _repository.SaveChangesAsync();
        }

        public async Task SaveChangeAsync()
        {
            await _repository.SaveChangesAsync();
        }

        public async Task AddAsync(ExpenseDTO dto)
        {
            var expense = new Expense
            {
                Titolo = dto.Titolo,
                ImportoTotale = dto.ImportoTotale,
                Data = dto.Data,
                CreatoreId = dto.CreatoreId,
                GruppoId = dto.GruppoId
            };

            await _repository.AddAsync(expense);
        }
        public async Task<List<ExpenseDTO>> GetByGroupIdAsync(int groupId)
        {
            var expenses = await _repository.GetByGroupIdAsync(groupId);
            var dtoList = new List<ExpenseDTO>();

            foreach (var exp in expenses)
            {
                var shares = await _expenseShareService.GetByExpenseIdAsync(exp.Id);

                dtoList.Add(new ExpenseDTO
                {
                    Id = exp.Id,
                    Titolo = exp.Titolo,
                    ImportoTotale = exp.ImportoTotale,
                    Data = exp.Data,
                    CreatoreId = exp.CreatoreId,
                    GruppoId = exp.GruppoId,
                    Quote = shares.Select(s => new ExpenseShareDTO
                    {
                        UserId = s.UserId,
                        Importo = s.Importo
                    }).ToList()
                });
            }

            return dtoList;
        }

    }
}
