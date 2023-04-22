using Expense_Manager.Areas.Logbooks.Models;
using Expense_Manager.Areas.Logs.Models;
using Expense_Manager.BAL;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using NuGet.Protocol;
using System.Data;
using System.Data.Common;
using System.Xml.Resolvers;

namespace Expense_Manager.DAL
{
    public class Log_DALBase
    {
        #region PR_LogbooksSelectAllByUserId
        public DataTable PR_LogbooksSelectAllByUserId(string conn)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LogbooksSelectAllByUserId");
                sqlDB.AddInParameter(dbCMD, "User_Id", SqlDbType.Int, CV.User_Id());

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region PR_LogsSelectAll
        public DataTable PR_LogsSelectAll(string conn , int Logbook_Id)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LogsSelectAll");
                sqlDB.AddInParameter(dbCMD, "Logbook_Id", SqlDbType.Int, Logbook_Id);

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion


        #region PR_LogbookDeleteByPk
        public void PR_LogbookDeleteByPk(string conn, int id)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LogbookDeleteByPk");
                sqlDB.AddInParameter(dbCMD, "Logbook_Id", SqlDbType.Int, id);
                sqlDB.ExecuteNonQuery(dbCMD);
            }
            catch (Exception ex)
            {
                return;
            }
        }
        #endregion

        #region PR_LogsDeleteByPk
        public void PR_LogsDeleteByPk(string conn, int Log_Id)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LogsDeleteByPk");
                sqlDB.AddInParameter(dbCMD, "@Log_Id", SqlDbType.Int, Log_Id);
                sqlDB.ExecuteNonQuery(dbCMD);
            }
            catch (Exception ex)
            {
                return;
            }
        }
		#endregion

		#region PR_DeleteLogsByLogbook
		public void PR_DeleteLogsByLogbook(string conn, int Logbook_Id)
		{
			try
			{
				SqlDatabase sqlDB = new SqlDatabase(conn);
				DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_DeleteLogsByLogbook");
				sqlDB.AddInParameter(dbCMD, "Logbook_Id", SqlDbType.Int, Logbook_Id);
				sqlDB.ExecuteNonQuery(dbCMD);
			}
			catch (Exception ex)
			{
				return;
			}
		}
		#endregion


		#region PR_LogbookSelectByPk
		public LogbookModel PR_LogbookSelectByPk(string conn, int Logbook_Id)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LogbookSelectByPk");
                sqlDB.AddInParameter(dbCMD, "Logbook_Id", SqlDbType.Int, Logbook_Id);

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }

                LogbookModel modelLogbook = new LogbookModel();
                foreach (DataRow drow in dt.Rows)
                {
                    modelLogbook.Logbook_Id = Convert.ToInt32(drow["Logbook_Id"]);
                    modelLogbook.Logbook_Name = drow["Logbook_Name"].ToString();
                    modelLogbook.Current_Balance = Convert.ToInt64(drow["Current_Balance"]);
                    modelLogbook.Creation_Date = Convert.ToDateTime(drow["Creation_Date"]);
                }
                return modelLogbook;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region PR_LogsSelectByPk
        public LogsModel PR_LogsSelectByPk(string conn, int Log_Id)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LogsSelectByPk");
                sqlDB.AddInParameter(dbCMD, "Log_Id", SqlDbType.Int, Log_Id);

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }

                LogsModel modelLog = new LogsModel();
                foreach (DataRow drow in dt.Rows)
                {
                    modelLog.Log_Id = Convert.ToInt32(drow["Log_Id"]);
                    modelLog.Logbook_Id = Convert.ToInt32(drow["Logbook_Id"]);
                    modelLog.Log_Name = drow["Log_Name"].ToString();
                    modelLog.Amount = Convert.ToInt64(drow["Amount"]);
                    modelLog.Creation_Date = Convert.ToDateTime(drow["Creation_Date"]);
                    modelLog.Description= drow["Description"].ToString();
                    modelLog.Expense_Date = Convert.ToDateTime(drow["Expense_Date"]);
                    modelLog.Log_Type = drow["Log_Type"].ToString();
                    modelLog.Log_TypeData = drow["Log_TypeData"].ToString();
                    if (modelLog.Modification_Date is not null)
                    {
                        modelLog.Modification_Date = Convert.ToDateTime(drow["Modification_Date"]);
                    }
                }
                return modelLog;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion


        #region PR_LogbookInsert
        public int PR_LogbookInsert(string conn, LogbookModel modelLogbook)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LogbookInsert");
                sqlDB.AddInParameter(dbCMD, "User_Id", SqlDbType.Int, CV.User_Id());
                sqlDB.AddInParameter(dbCMD, "Logbook_Name", SqlDbType.NVarChar, modelLogbook.Logbook_Name);

                int status = sqlDB.ExecuteNonQuery(dbCMD);
                return status;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        #endregion

        #region PR_LogsInsert
        public int PR_LogsInsert(string conn, LogsModel modelLog)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LogsInsert");
                sqlDB.AddInParameter(dbCMD, "Logbook_Id", SqlDbType.Int, modelLog.Logbook_Id);
                sqlDB.AddInParameter(dbCMD, "Log_Name", SqlDbType.NVarChar, modelLog.Log_Name);
                sqlDB.AddInParameter(dbCMD, "Log_Type", SqlDbType.NVarChar, modelLog.Log_Type);
                sqlDB.AddInParameter(dbCMD, "Log_TypeData", SqlDbType.NVarChar, modelLog.Log_TypeData);
                if (modelLog.Log_Type == "Expenditure")
                {
                    modelLog.Amount = Convert.ToInt64(modelLog.Amount) * (-1);
                }
                sqlDB.AddInParameter(dbCMD, "Amount", SqlDbType.BigInt, modelLog.Amount);
                sqlDB.AddInParameter(dbCMD, "Expense_Date", SqlDbType.Date, modelLog.Expense_Date);
                sqlDB.AddInParameter(dbCMD, "Description", SqlDbType.NVarChar, modelLog.Description);

                int status = sqlDB.ExecuteNonQuery(dbCMD);
                return status;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        #endregion


        #region PR_LogbookUpdateByPK
        public int PR_LogbookUpdateByPK(string conn, LogbookModel modelLogbook)
        {
            SqlDatabase sqlDB = new SqlDatabase(conn);
            DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LogbookUpdateByPK");
            sqlDB.AddInParameter(dbCMD, "Logbook_Id", SqlDbType.Int, modelLogbook.Logbook_Id);
            sqlDB.AddInParameter(dbCMD, "Logbook_Name", SqlDbType.NVarChar, modelLogbook.Logbook_Name);
            sqlDB.AddInParameter(dbCMD, "Current_Balance", SqlDbType.NVarChar, modelLogbook.Current_Balance);

            int status = sqlDB.ExecuteNonQuery(dbCMD);
            return status;

        }
        #endregion

        #region PR_LogsUpdateByPk
        public int PR_LogsUpdateByPk(string conn, LogsModel modelLog)
        {
            SqlDatabase sqlDB = new SqlDatabase(conn);
            DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LogsUpdatebyPk");
            sqlDB.AddInParameter(dbCMD, "Log_Id", SqlDbType.Int, modelLog.Log_Id);
            sqlDB.AddInParameter(dbCMD, "Log_Name", SqlDbType.NVarChar, modelLog.Log_Name);
            sqlDB.AddInParameter(dbCMD, "Log_Type", SqlDbType.NVarChar, modelLog.Log_Type);
            sqlDB.AddInParameter(dbCMD, "Log_TypeData", SqlDbType.NVarChar, modelLog.Log_TypeData);
            sqlDB.AddInParameter(dbCMD, "Amount", SqlDbType.BigInt, modelLog.Amount);
            sqlDB.AddInParameter(dbCMD, "Expense_Date", SqlDbType.Date, modelLog.Expense_Date);
            sqlDB.AddInParameter(dbCMD, "Description", SqlDbType.NVarChar, modelLog.Description);

            int status = sqlDB.ExecuteNonQuery(dbCMD);
            return status;

        }
        #endregion


        #region PR_SetCurrentBalance
        public int PR_SetCurrentBalance(string conn,int Logbook_Id ,Int64 Amount )
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_SetCurrentBalance");
                sqlDB.AddInParameter(dbCMD, "Logbook_Id", SqlDbType.Int, Logbook_Id);
                sqlDB.AddInParameter(dbCMD, "Amount", SqlDbType.BigInt, Amount);

                int status = sqlDB.ExecuteNonQuery(dbCMD);
                return status;
            }
            catch(Exception e)
            {
                return -1;
            }

        }
        #endregion

        //Doubt here 
        #region PR_UpdateCurrentBalance
        public int PR_UpdateCurrentBalance(string conn, int Logbook_Id, Int64 old,Int64 neww)
        {
            SqlDatabase sqlDB = new SqlDatabase(conn);
            DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_UpdateCurrentBalance");
            sqlDB.AddInParameter(dbCMD, "Logbook_Id", SqlDbType.Int, Logbook_Id);
            sqlDB.AddInParameter(dbCMD, "old", SqlDbType.BigInt, old);
            sqlDB.AddInParameter(dbCMD, "new", SqlDbType.BigInt, neww);

            int status = sqlDB.ExecuteNonQuery(dbCMD);
            return status;

        }
        #endregion


        #region PR_SelectAllExpenses
        public DataTable PR_SelectAllExpenses(string conn, int Logbook_Id)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_SelectAllExpenses");
                sqlDB.AddInParameter(dbCMD, "Logbook_Id", SqlDbType.Int, Logbook_Id);

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Monthly_Expense
        public DataTable PR_MonthlyExpense(string conn , int Logbook_Id)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MonthlyExpense");
                sqlDB.AddInParameter(dbCMD, "Logbook_Id", SqlDbType.Int, Logbook_Id);

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Yearly_Expense
        public DataTable PR_YearlyExpenseIncome (string conn, int Logbook_Id)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_YearlyExpenseIncome");
                sqlDB.AddInParameter(dbCMD, "Logbook_Id", SqlDbType.Int, Logbook_Id);

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}
