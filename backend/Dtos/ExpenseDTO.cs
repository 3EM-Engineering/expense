using System;
using backend.Models;

namespace backend.Dto
{
    public class ExpenseDTO
    {
        public int Id { get; set; }
        public string Titolo { get; set; }
        public decimal ImportoTotale { get; set; }
        public DateTime Data { get; set; }
        public int CreatoreId { get; set; }
        public int GruppoId { get; set; }

        public List<ExpenseShare>? Quote { get; set; } = new List<ExpenseShare>();
    }
}
