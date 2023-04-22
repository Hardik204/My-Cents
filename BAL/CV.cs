namespace Expense_Manager.BAL
{
    public class CV
    {
        private static IHttpContextAccessor _httpContextAccessor;

        static CV()
        {
            _httpContextAccessor = new HttpContextAccessor();
        }

        public static string? Username()
        {
            string? Username = null;

            if (_httpContextAccessor.HttpContext.Session.GetString("Username") != null)
            {
                Username = _httpContextAccessor.HttpContext.Session.GetString("Username").ToString();
            }
            return Username;
        }

        public static int? User_Id()
        {
            int? User_Id = null;

            if (_httpContextAccessor.HttpContext.Session.GetString("User_Id") != null)
            {
                User_Id = Convert.ToInt32(_httpContextAccessor.HttpContext.Session.GetString("User_Id"));
            }
            return User_Id;
        }

		public static string? First_Name()
		{
			string? First_Name = null;

			if (_httpContextAccessor.HttpContext.Session.GetString("First_Name") != null)
			{
				First_Name = _httpContextAccessor.HttpContext.Session.GetString("First_Name").ToString();
			}
			return First_Name;
		}

        public static string? AvatarLocation()
        {
            string? AvatarLocation = null;

            if (_httpContextAccessor.HttpContext.Session.GetString("AvatarLocation") != null)
            {
                AvatarLocation = _httpContextAccessor.HttpContext.Session.GetString("AvatarLocation").ToString();
            }
            return AvatarLocation;
        }
    }
}
