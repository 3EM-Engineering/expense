namespace backend.Dto
{
    public class ExpenseShareDTO
    {
        public int UserId { get; set; }
        public decimal Importo { get; set; }
        public int ExpenseId { get; set; }
        public List<ExpenseShareDTO> Quote { get; set; }
    }
}
