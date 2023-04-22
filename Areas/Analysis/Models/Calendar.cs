namespace Expense_Manager.Areas.Analysis.Models
{
    public class Calendar
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool allDay { get; set; } 
    }
}
