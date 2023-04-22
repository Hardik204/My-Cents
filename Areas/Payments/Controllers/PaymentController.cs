using Expense_Manager.Areas.Income.Models;
using Expense_Manager.Areas.Payments.Models;
using Expense_Manager.BAL;
using Expense_Manager.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Expense_Manager.Areas.Payments.Controllers
{
    [CheckAccess]
    [Area("Payments")]
    [Route("Payments/[Controller]/[action]")]
    public class PaymentController : Controller
    {

        #region Config
        private IConfiguration Configuration;

        public PaymentController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        #endregion

        #region SelectAll
        public IActionResult Index()
        {
            string str = Configuration.GetConnectionString("myConnectionString");
            Money_DAL dalMoney = new Money_DAL();
            DataTable dt = dalMoney.PR_PaymentModeSelectAll(str);
            return View("PaymentsList", dt);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int Payment_Id)
        {
            string str = Configuration.GetConnectionString("myConnectionString");
            Money_DAL dalMoney = new Money_DAL();
            dalMoney.PR_Payment_Mode_DeleteByPk(str, Payment_Id);
            return RedirectToAction("Add", "Logs", new { area = "Logs" });
        }
        #endregion

        #region Add
        public IActionResult Add(int? Payment_Id)
        {

            #region Record Select by Pk
            if (Payment_Id != null)
            {
                string str = Configuration.GetConnectionString("myConnectionString");
                Money_DAL dalMoney = new Money_DAL();
                PaymentModel modelPayment = dalMoney.PR_Payment_Mode_SelectByPK(str, (int)Payment_Id);
                return View("PaymentAddEdit", modelPayment);
            }
            #endregion

            return View("PaymentAddEdit");
        }
        #endregion

        #region Save
        [HttpPost]
        public IActionResult Save(PaymentModel modelPayment)
        {
            string str = Configuration.GetConnectionString("myConnectionString");
            Money_DAL dalMoney = new Money_DAL();

            if (modelPayment.Payment_Id == null)
            {
                if (Convert.ToBoolean(dalMoney.PR_Payment_Type_Insert(str, modelPayment)))
                {
                    TempData["Msg"] = "Record Inserted Successfully!";
                }
            }
            else
            {
                if (Convert.ToBoolean(dalMoney.PR_Payment_Mode_UpdateByPK(str, modelPayment)))
                {
                    TempData["Msg"] = "Record Updated Successfully!";
                }

            }
            return RedirectToAction("Add", "Logs", new { area = "Logs" });
        }
        #endregion
    }
}
