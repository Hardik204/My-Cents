namespace Expense_Manager.Areas.SEC_User.Models
{
    public class SEC_UserModel
    {
        public int? User_Id { get; set; }

        public string Username { get; set;}

        public string? Email { get; set;}

        public Int64 ContactNumber { get; set; }

        public string First_Name { get; set;}

        public string?Last_Name { get;set;}

        public string Password { get; set;}

        public string? AvatarLocation { get; set;}

    }
}
