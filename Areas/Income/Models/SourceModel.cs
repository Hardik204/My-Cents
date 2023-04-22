namespace Expense_Manager.Areas.Income.Models
{
    public class SourceModel
    {
        public int Source_Id { get; set; }

        public string? Source_Name { get; set; }

        public int User_Id { get; set; }
    }

    public class SourceDropdown
    {
        public int Source_id { get; set; }

        public string? Source_Name { get; set; }
    }
}
