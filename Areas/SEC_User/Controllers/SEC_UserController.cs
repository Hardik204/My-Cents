using Expense_Manager.Areas.Logbooks.Models;
using Expense_Manager.Areas.SEC_User.Models;
using Expense_Manager.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Expense_Manager.Areas.SEC_User.Controllers
{
	[Area("SEC_User")]
	[Route("SEC_User/[Controller]/[action]")]
	public class SEC_UserController : Controller
    {

		#region Config
		private IConfiguration Configuration;

		public SEC_UserController(IConfiguration _configuration)
		{
			Configuration = _configuration;
		}
		#endregion

		public IActionResult Index()
        {
            return View();
        }

		[HttpPost]
		#region Login
		public IActionResult Login(SEC_UserModel modelSEC_User)
		{
			string conn = this.Configuration.GetConnectionString("myConnectionString");
			string error = null;
			if (modelSEC_User.Username == null)
			{
				error += "Username is required.";
			}
			if (modelSEC_User.Password == null)
			{
				error += "<br>Password is required.";
			}
			if (error != null)
			{
				ViewData["Error"] = error;
				return RedirectToAction("Index");
			}
			else
			{
				SEC_DAL dalSEC = new SEC_DAL();
				DataTable dt = dalSEC.dbo_PR_SEC_User_SelectByUserNamePassword(conn, modelSEC_User.Username, modelSEC_User.Password);
				if (dt.Rows.Count > 0)
				{
					foreach (DataRow dr in dt.Rows)
					{
						HttpContext.Session.SetString("User_Id", dr["User_Id"].ToString());
						HttpContext.Session.SetString("First_Name", dr["First_Name"].ToString());
						HttpContext.Session.SetString("Last_Name", dr["Last_Name"].ToString());
						HttpContext.Session.SetString("Email", dr["Email"].ToString());
                        HttpContext.Session.SetString("AvatarLocation", dr["Location"].ToString());
                        HttpContext.Session.SetString("Username", dr["Username"].ToString());
						HttpContext.Session.SetString("Password", dr["Password"].ToString());
						break;
					}
				}
				else
				{
					ViewData["Error"] = "UserName or Password is invalid!";
					return RedirectToAction("Index");
				}
				if (HttpContext.Session.GetString("Username") != null && HttpContext.Session.GetString("Password") != null)
				{
					return RedirectToAction("Index", "Logbooks", new { area = "Logbooks" });
				}
			}
			return RedirectToAction("Index");
		}
		#endregion

		[HttpPost]
		#region User SignUp
		public IActionResult SignUp(SEC_UserModel modelSEC_User)
		{
			string conn = this.Configuration.GetConnectionString("myConnectionString");
			string error = null;
			if (modelSEC_User.Username == null)
			{
				error += "Username is required.";
			}
			if (modelSEC_User.Password == null)
			{
				error += "<br>Password is required.";
			}
			if (error != null)
			{
				ViewData["Error"] = error;
				return RedirectToAction("Index");
			}
			else
			{
				SEC_DAL dalSEC = new SEC_DAL();
                if (@Convert.ToBoolean(dalSEC.dbo_PR_SEC_User_Insert(conn, modelSEC_User)))
				{
                    DataTable dt = dalSEC.dbo_PR_SEC_User_SelectByUserNamePassword(conn, modelSEC_User.Username, modelSEC_User.Password);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            HttpContext.Session.SetString("User_Id", dr["User_Id"].ToString());
                            HttpContext.Session.SetString("First_Name", dr["First_Name"].ToString());
                            HttpContext.Session.SetString("Last_Name", dr["Last_Name"].ToString());
                            HttpContext.Session.SetString("Email", dr["Email"].ToString());
                            HttpContext.Session.SetString("AvatarLocation", dr["Location"].ToString());
                            HttpContext.Session.SetString("Username", dr["Username"].ToString());
                            HttpContext.Session.SetString("Password", dr["Password"].ToString());
                            break;
                        }
                    }
					return RedirectToAction("AvatarSelect");
				}
				else
				{
					ViewData["Error"] = "Insertion Failed !";
					return RedirectToAction("OpenSignUp");
				}
			}
			return RedirectToAction("Index");

		}
		#endregion

		#region Logout
		public IActionResult Logout()
		{
			HttpContext.Session.Clear();
			return RedirectToAction("Index", "SEC_User");
		}
		#endregion

		public IActionResult OpenSignUp()
		{
			return View("SignUpPage");
		}

        #region ViewProfile
        public IActionResult ViewProfilePage()
		{
            string str = Configuration.GetConnectionString("myConnectionString");
            SEC_DAL dalSEC = new SEC_DAL();
            DataTable dt = dalSEC.PR_SEC_UserSelectByPkdt(str);
            return View("ViewProfile",dt);
		}
        #endregion

        #region AvatarSelectAll
        public IActionResult AvatarSelect()
		{
            string str = Configuration.GetConnectionString("myConnectionString");
            SEC_DAL dalSEC = new SEC_DAL();
            DataTable dt = dalSEC.PR_SelectAllAvatars(str);
            return View("EmojiSelect", dt);
		}
        #endregion

        #region SetAvatar
		public IActionResult SetAvatar(int Photo_Id)
		{
            string str = Configuration.GetConnectionString("myConnectionString");
            SEC_DAL dalSEC = new SEC_DAL();
            if(Convert.ToBoolean(dalSEC.PR_SetAvatar(str,Photo_Id)))
			{
				string loc = dalSEC.PR_SelectAvatarByPK(str, Photo_Id);
				HttpContext.Session.SetString("AvatarLocation", loc);
                return RedirectToAction("Index","Logbooks",new { area = "Logbooks"});
            }
			return RedirectToAction("AvatarSelect");
        }
        #endregion

        #region Add
		public IActionResult Add()
		{
            string str = Configuration.GetConnectionString("myConnectionString");
            SEC_DAL dalSEC = new SEC_DAL();
            SEC_UserModel modelSEC_User = dalSEC.PR_SEC_UserSelectByPk(str);
            return View("SEC_UserAddEdit",modelSEC_User);
        }
        #endregion

        #region Save
		public IActionResult Save(SEC_UserModel modelSEC_User)
		{
            string str = Configuration.GetConnectionString("myConnectionString");
            SEC_DAL dalSEC = new SEC_DAL();
            if (Convert.ToBoolean(dalSEC.PR_SEC_User_UpdateByPk(str, modelSEC_User)))
            {
                HttpContext.Session.SetString("First_Name", modelSEC_User.First_Name);
                HttpContext.Session.SetString("Username", modelSEC_User.Username);
                ViewData["Msg"] = "Record Updated Successfully!";
            }
            return RedirectToAction("ViewProfilePage");
        }
        #endregion

    }
}
