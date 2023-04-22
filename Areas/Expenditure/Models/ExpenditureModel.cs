namespace Expense_Manager.Areas.Expenditure.Models
{
    public class ExpenditureModel
    {
        public int Expenditure_Id { get; set; }

        public string? Expenditure_Name { get; set; }

        public int User_Id { get; set; }
    }

    public class ExpenditureDropdown
    {
        public int Expenditure_Id { get; set; }

        public string? Expenditure_Name { get; set; }
    }
}
