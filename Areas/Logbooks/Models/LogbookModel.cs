namespace Expense_Manager.Areas.Logbooks.Models
{
    public class LogbookModel
    {
        public int? Logbook_Id { get; set; }

        public string? Logbook_Name { get; set; }

        public int User_Id { get; set; }

        public Int64 Current_Balance { get; set; }

        public DateTime? Creation_Date { get; set; }

        public DateTime? Modification_Date { get; set; }
    }
}
