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

        public ExpenseServices(IExpenseRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ExpenseDTO>> GetAllAsync()
        {
            var expenses = await _repository.GetAllAsync();
            var dtoList = new List<ExpenseDTO>();

            foreach (var e in expenses)
            {
                dtoList.Add(new ExpenseDTO
                {
                    Id = e.Id,
                    Titolo = e.Titolo,
                    ImportoTotale = e.ImportoTotale,
                    Data = e.Data,
                    CreatoreId = e.CreatoreId,
                    GruppoId = e.GruppoId
                    // Aggiungi anche Quote se serve
                });
            }

            return dtoList;
        }

        public async Task<ExpenseDTO> GetByIdAsync(int id)
        {
            var e = await _repository.GetByIdAsync(id);
            if (e == null) return null;

            return new ExpenseDTO
            {
                Id = e.Id,
                Titolo = e.Titolo,
                ImportoTotale = e.ImportoTotale,
                Data = e.Data,
                CreatoreId = e.CreatoreId,
                GruppoId = e.GruppoId
                // Aggiungi anche Quote se serve
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
                GruppoId = dto.GruppoId,
                Quote = new List<ExpenseShare>() // puoi mappare dto.Quote se presente
            };

            await _repository.AddAsync(expense);
            await _repository.SaveChangesAsync();

            dto.Id = expense.Id;
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
    }
}
