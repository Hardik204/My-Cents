using Expense_Manager.Areas.Expenditure.Models;
using Expense_Manager.Areas.Income.Models;
using Expense_Manager.BAL;
using Expense_Manager.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Expense_Manager.Areas.Income.Controllers
{
    [CheckAccess]
    [Area("Income")]
    [Route("Income/[Controller]/[action]")]
    public class SourcesController : Controller
    {
        #region Config
        private IConfiguration Configuration;

        public SourcesController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        #endregion

        #region SelectAll
        public IActionResult Index()
        {
            string str = Configuration.GetConnectionString("myConnectionString");
            Money_DAL dalMoney = new Money_DAL();
            DataTable dt = dalMoney.PR_SourcesSelectAll(str);
            return View("LogbooksList", dt);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int Source_Id)
        {
            string str = Configuration.GetConnectionString("myConnectionString");
            Money_DAL dalMoney = new Money_DAL();
            dalMoney.PR_SourcesDeleteByPk(str, Source_Id);
            return RedirectToAction("Add", "Logs", new { area = "Logs" });
        }
        #endregion

        #region Add
        public IActionResult Add(int? Source_Id)
        {

            #region Record Select by Pk
            if (Source_Id != null)
            {
                string str = Configuration.GetConnectionString("myConnectionString");
                Money_DAL dalMoney = new Money_DAL();
                SourceModel modelSource = dalMoney.PR_SourcesSelectByPk(str, (int)Source_Id);
                return View("SourceAddEdit", modelSource);
            }
            #endregion

            return View("SourceAddEdit");
        }
        #endregion

        #region Save
        [HttpPost]
        public IActionResult Save(SourceModel modelSource)
        {
            string str = Configuration.GetConnectionString("myConnectionString");
            Money_DAL dalMoney = new Money_DAL();

            if (modelSource.Source_Id == null)
            {
                if (Convert.ToBoolean(dalMoney.PR_SourcesInsert(str, modelSource)))
                {
                    TempData["Msg"] = "Record Inserted Successfully!";
                }
            }
            else
            {
                if (Convert.ToBoolean(dalMoney.PR_SourcesUpdateByPk(str, modelSource)))
                {
                    TempData["Msg"] = "Record Updated Successfully!";
                }

            }
            return RedirectToAction("Add", "Logs", new { area = "Logs" });
        }
        #endregion
    }
}
