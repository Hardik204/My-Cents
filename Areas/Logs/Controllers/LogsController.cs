using Expense_Manager.Areas.Expenditure.Models;
using Expense_Manager.Areas.Income.Models;
using Expense_Manager.Areas.Logbooks.Models;
using Expense_Manager.Areas.Logs.Models;
using Expense_Manager.Areas.Payments.Models;
using Expense_Manager.BAL;
using Expense_Manager.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using NuGet.Protocol;
using System.Data;
using System.Data.Common;

namespace Expense_Manager.Areas.Logs.Controllers
{
    [CheckAccess]
    [Area("Logs")]
    [Route("Logs/[Controller]/[action]")]
    public class LogsController : Controller
    {

        #region Config
        private IConfiguration Configuration;

        public LogsController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        #endregion

        #region SelectAll
        public IActionResult Index(int Logbook_Id)
        {
            string str = Configuration.GetConnectionString("myConnectionString");
            Log_DAL dalLog = new Log_DAL();
            ViewData["Logbook_Id"] = Logbook_Id;
            if(Logbook_Id == null)
            {
                Logbook_Id = Convert.ToInt32(TempData["Logbook_Id"]);
            }
            else
            {
                TempData["Logbook_Id"] = Logbook_Id;
                RedirectToAction("Save");
                RedirectToAction("Delete");
            }
            DataTable dt = dalLog.PR_LogsSelectAll(str, Logbook_Id);
            return View("LogList", dt);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int Log_Id)
        {
            string str = Configuration.GetConnectionString("myConnectionString");
            Log_DAL dalLog = new Log_DAL();
            dalLog.PR_LogsDeleteByPk(str, Log_Id);
            return RedirectToAction("Index", new { Logbook_Id = Convert.ToInt32(TempData["Logbook_Id"])});
        }
        #endregion

        #region Add
        public IActionResult Add(int? Log_Id)
        {
			string str = Configuration.GetConnectionString("myConnectionString");
			Money_DAL dalMoney = new Money_DAL();

            #region Dropdown for Sources
            //Dropdown for Selecting Sources of Income
            DataTable dt = dalMoney.PR_SourcesSelectAll(str);
            List<SourceDropdown> list = new List<SourceDropdown>();
            foreach(DataRow dr in dt.Rows)
            {
                SourceDropdown sd = new SourceDropdown();
                sd.Source_id = Convert.ToInt32(dr["Source_id"]);
                sd.Source_Name = dr["Source_Name"].ToString();
                list.Add(sd);
            }
            ViewBag.SourceData = list;
            #endregion

            #region Dropdown for Expenditure
            dt = dalMoney.PR_ExpenditureSelectAll(str);
            List<ExpenditureDropdown> list1 = new List<ExpenditureDropdown>();
            foreach(DataRow dr in dt.Rows)
            {
                ExpenditureDropdown ed = new ExpenditureDropdown();
                ed.Expenditure_Id = Convert.ToInt32(dr["Expenditure_Id"]);
                ed.Expenditure_Name = dr["Expenditure_Name"].ToString() ;
                list1.Add(ed);
            }
            ViewBag.ExpenditureData = list1;
            #endregion

            #region Dropdown for Payment Mode
            dt = dalMoney.PR_PaymentModeSelectAll(str);
            List<PaymentDropdown> list2 = new List<PaymentDropdown>();
            foreach(DataRow dr in dt.Rows)
            {
                PaymentDropdown pd = new PaymentDropdown();
                pd.Payment_Id = Convert.ToInt32(dr["Payment_Id"]);
                pd.Payment_Mode = dr["Payment_Mode"].ToString();
                list2.Add(pd);
            }
            ViewBag.PaymentData = list2;
            #endregion


            #region Record Select by Pk
            if (Log_Id != null)
            {
                
                Log_DAL dalLog = new Log_DAL();
                LogsModel modelLog = dalLog.PR_LogsSelectByPk(str, (int)Log_Id);
                return View("LogAddEdit", modelLog);
            }
            #endregion

            return View("LogAddEdit");
        }
        #endregion

        #region Save
        [HttpPost]
        public IActionResult Save(LogsModel modelLog)
        {
            modelLog.Logbook_Id = Convert.ToInt32(TempData["Logbook_Id"]);
            string str = Configuration.GetConnectionString("myConnectionString");
            Log_DAL dalLog = new Log_DAL();
            if (modelLog.Log_Id == null)
            {
                if (Convert.ToBoolean(dalLog.PR_LogsInsert(str, modelLog)))
                {
                    dalLog.PR_SetCurrentBalance(str, modelLog.Logbook_Id, modelLog.Amount);
                    ViewData["Msg"] = "Record Inserted Successfully!";
                }
            }
            else
            {
                if (Convert.ToBoolean(dalLog.PR_LogsUpdateByPk(str, modelLog)))
                {
                    LogsModel tempModel = dalLog.PR_LogsSelectByPk(str, (int)modelLog.Log_Id);
                    dalLog.PR_UpdateCurrentBalance(str, modelLog.Logbook_Id, tempModel.Amount, modelLog.Amount);
                    ViewData["Msg"] = "Record Updated Successfully!";
                }

            }
            return RedirectToAction("Index",new { Logbook_Id = Convert.ToInt32(TempData["Logbook_Id"])});
        }
        #endregion
    }
}
