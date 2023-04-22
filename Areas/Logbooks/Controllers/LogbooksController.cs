using Expense_Manager.Areas.Logbooks.Models;
using Expense_Manager.BAL;
using Expense_Manager.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Expense_Manager.Areas.Logbooks.Controllers
{
    [CheckAccess]
    [Area("Logbooks")]
    [Route("Logbooks/[Controller]/[action]")]
    public class LogbooksController : Controller
	{

        #region Config
        private IConfiguration Configuration;

        public LogbooksController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        #endregion

        #region SelectAll
        public IActionResult Index()
		{
            string str = Configuration.GetConnectionString("myConnectionString");
            Log_DAL dalLog = new Log_DAL();
            DataTable dt = dalLog.PR_LogbooksSelectAllByUserId(str);
            return View("LogbooksList", dt);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int Logbook_Id)
        {
            string str = Configuration.GetConnectionString("myConnectionString");
            Log_DAL dalLog = new Log_DAL();
            dalLog.PR_LogbookDeleteByPk(str, Logbook_Id);
            dalLog.PR_DeleteLogsByLogbook(str,Logbook_Id);
            return RedirectToAction("Index");
        }
        #endregion

        #region Add
        public IActionResult Add(int? Logbook_Id)
        {

            #region Record Select by Pk
            if (Logbook_Id != null)
            {
                string str = Configuration.GetConnectionString("myConnectionString");
                Log_DAL dalLog = new Log_DAL();
                LogbookModel modelLogbook = dalLog.PR_LogbookSelectByPk(str, (int)Logbook_Id);
                return View("LogbookAddEdit", modelLogbook);
}
            #endregion

            return View("LogbookAddEdit");
        }
        #endregion

        #region Save
        [HttpPost]
        public IActionResult Save(LogbookModel modelLogbook)
        {
            string str = Configuration.GetConnectionString("myConnectionString");
            Log_DAL dalLog = new Log_DAL();

            if (modelLogbook.Logbook_Id == null)
            {
                if (Convert.ToBoolean(dalLog.PR_LogbookInsert(str, modelLogbook)))
                {
                    TempData["Msg"] = "Record Inserted Successfully!";
                }
            }
            else
            {
                if (Convert.ToBoolean(dalLog.PR_LogbookUpdateByPK(str, modelLogbook)))
                {
                    TempData["Msg"] = "Record Updated Successfully!";
                }

            }
            return RedirectToAction("Index");
        }
        #endregion
    }
}
