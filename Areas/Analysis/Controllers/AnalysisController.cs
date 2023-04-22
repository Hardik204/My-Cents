using Expense_Manager.Areas.Analysis.Models;
using Expense_Manager.BAL;
using Expense_Manager.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Expense_Manager.Areas.Analysis.Controllers
{
    [CheckAccess]
    [Area("Analysis")]
    [Route("Analysis/[Controller]/[action]")]
    public class AnalysisController : Controller
    {

        #region Config
        private IConfiguration Configuration;

        public AnalysisController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        #endregion

        #region Daily_Expense
        public IActionResult Index(int Logbook_Id)
        {
            string str = Configuration.GetConnectionString("myConnectionString");
            Log_DAL dalLog = new Log_DAL();
            DataTable dt = dalLog.PR_SelectAllExpenses(str , Logbook_Id);
            return View("DailyExpense", dt);
        }
        #endregion

        #region Monthly Expense
        public IActionResult MonthlyExpensePage(int Logbook_Id)
        {
            string str = Configuration.GetConnectionString("myConnectionString");
            Log_DAL dalLog = new Log_DAL();
            DataTable dt = dalLog.PR_MonthlyExpense(str, Logbook_Id);
            return View("MonthlyExpense",dt);
        }
        #endregion

        #region Yearly Expense
        public IActionResult YearlyExpensePage( int Logbook_Id)
        {
            string str = Configuration.GetConnectionString("myConnectionString");
            Log_DAL dalLog = new Log_DAL();
            DataTable dt = dalLog.PR_YearlyExpenseIncome(str, Logbook_Id);
            return View("YearlyExpense",dt);
        }
        #endregion

    }
}
 