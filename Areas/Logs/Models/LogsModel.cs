namespace Expense_Manager.Areas.Logs.Models
{
    public class LogsModel
    {
        public int? Log_Id { get; set; }

        public int Logbook_Id { get; set; }

        public string? Log_Name { get; set; }

        public string? Log_Type { get; set; }

        public string? Log_TypeData { get; set; }

        public Int64 Amount { get; set; }

        public DateTime Expense_Date { get; set; }

        public Int64 Balance { get; set; }

        public string? Description { get; set; }

        public DateTime? Creation_Date { get; set; }

        public DateTime? Modification_Date { get; set; }
    }
}
