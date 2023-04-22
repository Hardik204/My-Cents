using Expense_Manager.Areas.Expenditure.Models;
using Expense_Manager.Areas.Logbooks.Models;
using Expense_Manager.BAL;
using Expense_Manager.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Expense_Manager.Areas.Expenditure.Controllers
{
    [CheckAccess]
    [Area("Expenditure")]
    [Route("Expenditure/[Controller]/[action]")]
    public class ExpenditureController : Controller
    {

        #region Config
        private IConfiguration Configuration;

        public ExpenditureController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        #endregion

        #region SelectAll
        public IActionResult Index()
        {
            string str = Configuration.GetConnectionString("myConnectionString");
            Money_DAL dalMoney = new Money_DAL();
            DataTable dt = dalMoney.PR_ExpenditureSelectAll(str);
            return View("LogbooksList", dt);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int Expenditure_Id)
        {
            string str = Configuration.GetConnectionString("myConnectionString");
            Money_DAL dalMoney = new Money_DAL();
            dalMoney.PR_ExpenditureDeleteByPk(str, Expenditure_Id);
            return RedirectToAction("Add", "Logs", new { area = "Logs" });
        }
        #endregion

        #region Add
        public IActionResult Add(int? Expenditure_Id)
        {

            #region Record Select by Pk
            if (Expenditure_Id != null)
            {
                string str = Configuration.GetConnectionString("myConnectionString");
                Money_DAL dalMoney = new Money_DAL();
                ExpenditureModel modelExpenditure = dalMoney.PR_ExpenditureSelectByPk(str, (int)Expenditure_Id);
                return View("ExpenditureAddEdit", modelExpenditure);
            }
            #endregion

            return View("ExpenditureAddEdit");
        }
        #endregion

        #region Save
        [HttpPost]
        public IActionResult Save(ExpenditureModel modelExpenditure)
        {
            string str = Configuration.GetConnectionString("myConnectionString");
            Money_DAL dalMoney = new Money_DAL();

            if (modelExpenditure.Expenditure_Id == null)
            {
                if (Convert.ToBoolean(dalMoney.PR_ExpenditureInsert(str, modelExpenditure)))
                {
                    TempData["Msg"] = "Record Inserted Successfully!";
                }
            }
            else
            {
                if (Convert.ToBoolean(dalMoney.PR_ExpenditureUpdateByPk(str, modelExpenditure)))
                {
                    TempData["Msg"] = "Record Updated Successfully!";
                }

            }
            return RedirectToAction("Add","Logs", new { area = "Logs" });
        }
        #endregion
    }
}
