namespace Expense_Manager.Areas.Payments.Models
{
    public class PaymentModel
    {
        public int Payment_Id { get; set; }

        public string Payment_Mode { get; set;}

        public int User_Id { get; set; }
    }

    public class PaymentDropdown
    {
        public int Payment_Id { get; set; }

        public string Payment_Mode { get; set; }
    }
}
