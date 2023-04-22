using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using Expense_Manager.Areas.Logbooks.Models;
using Expense_Manager.Areas.Expenditure.Models;
using Expense_Manager.BAL;
using Expense_Manager.Areas.Payments.Models;
using Expense_Manager.Areas.Income.Models;

namespace Expense_Manager.DAL
{
    public class Money_DALBase
    {
        #region PR_CurrencySelectAll
        public DataTable PR_CurrencySelectAll(string conn)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CurrencySelectAll");

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


        #region PR_ExpenditureSelectAll
        public DataTable PR_ExpenditureSelectAll(string conn)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_ExpenditureSelectAll");
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

        #region PR_PaymentModeSelectAll
        public DataTable PR_PaymentModeSelectAll(string conn)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_PaymentModeSelectAll");
                sqlDB.AddInParameter(dbCMD, "User_id", SqlDbType.Int, CV.User_Id());

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

        #region PR_SourcesSelectAll
        public DataTable PR_SourcesSelectAll(string conn)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_SourcesSelectAll");
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


        #region PR_ExpenditureDeleteByPk
        public void PR_ExpenditureDeleteByPk(string conn, int id)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_ExpenditureDeleteByPk");
                sqlDB.AddInParameter(dbCMD, "Expenditure_Id", SqlDbType.Int, id);
                sqlDB.ExecuteNonQuery(dbCMD);
            }
            catch (Exception ex)
            {
                return;
            }
        }
        #endregion

        #region PR_Payment_Mode_DeleteByPk
        public void PR_Payment_Mode_DeleteByPk(string conn, int id)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_Payment_Mode_DeleteByPk");
                sqlDB.AddInParameter(dbCMD, "Payment_Id", SqlDbType.Int, id);
                sqlDB.ExecuteNonQuery(dbCMD);
            }
            catch (Exception ex)
            {
                return;
            }
        }
        #endregion

        #region PR_SourcesDeleteByPk
        public void PR_SourcesDeleteByPk(string conn, int id)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_SourcesDeleteByPk");
                sqlDB.AddInParameter(dbCMD, "Source_Id", SqlDbType.Int, id);
                sqlDB.ExecuteNonQuery(dbCMD);
            }
            catch (Exception ex)
            {
                return;
            }
        }
        #endregion


        #region PR_ExpenditureSelectByPk
        public ExpenditureModel PR_ExpenditureSelectByPk(string conn, int Id)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_ExpenditureSelectByPk");
                sqlDB.AddInParameter(dbCMD, "Expenditure_Id", SqlDbType.Int, Id);

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }

                ExpenditureModel modelExpenditure = new ExpenditureModel();
                foreach (DataRow drow in dt.Rows)
                {
                    modelExpenditure.Expenditure_Id = Convert.ToInt32(drow["Expenditure_Id "]);
                    modelExpenditure.Expenditure_Name = drow["Expenditure_Name"].ToString();
                }
                return modelExpenditure;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region PR_Payment_Mode_SelectByPK
        public PaymentModel PR_Payment_Mode_SelectByPK(string conn, int Id)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_Payment_Mode_SelectByPK");
                sqlDB.AddInParameter(dbCMD, "Payment_Id", SqlDbType.Int, Id);

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }

                PaymentModel modelPayment = new PaymentModel();
                foreach (DataRow drow in dt.Rows)
                {
                    modelPayment.Payment_Id = Convert.ToInt32(drow["Payment_Id "]);
                    modelPayment.Payment_Mode = drow["Payment_Mode"].ToString();
                }
                return modelPayment;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region PR_SourcesSelectByPk
        public SourceModel PR_SourcesSelectByPk(string conn, int Id)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_SourcesSelectByPk");
                sqlDB.AddInParameter(dbCMD, "Source_id", SqlDbType.Int, Id);

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }

                SourceModel modelSource = new SourceModel();
                foreach (DataRow drow in dt.Rows)
                {
                    modelSource.Source_Id = Convert.ToInt32(drow["Source_Id"]);
                    modelSource.Source_Name = drow["Source_Name"].ToString();
                }
                return modelSource;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion


        #region PR_ExpenditureInsert
        public int PR_ExpenditureInsert(string conn, ExpenditureModel modelExpenditure)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_ExpenditureInsert");
                sqlDB.AddInParameter(dbCMD, "User_Id", SqlDbType.Int, modelExpenditure.User_Id);
                sqlDB.AddInParameter(dbCMD, "Expenditure_Name", SqlDbType.NVarChar, modelExpenditure.Expenditure_Name);

                int status = sqlDB.ExecuteNonQuery(dbCMD);
                return status;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        #endregion

        #region PR_Payment_Type_Insert
        public int PR_Payment_Type_Insert(string conn, PaymentModel modelPayment)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("[PR_Payment_Type_Insert");
                sqlDB.AddInParameter(dbCMD, "User_Id", SqlDbType.Int, modelPayment.User_Id);
                sqlDB.AddInParameter(dbCMD, "Payment_Mode", SqlDbType.NVarChar, modelPayment.Payment_Mode);

                int status = sqlDB.ExecuteNonQuery(dbCMD);
                return status;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        #endregion

        #region PR_SourcesInsert
        public int PR_SourcesInsert(string conn, SourceModel modelSource)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("[PR_SourcesInsert");
                sqlDB.AddInParameter(dbCMD, "User_Id", SqlDbType.Int, modelSource.User_Id);
                sqlDB.AddInParameter(dbCMD, "Source_Name", SqlDbType.NVarChar, modelSource.Source_Name);

                int status = sqlDB.ExecuteNonQuery(dbCMD);
                return status;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        #endregion


        #region PR_ExpenditureUpdateByPk
        public int PR_ExpenditureUpdateByPk(string conn, ExpenditureModel modelExpenditure)
        {
            SqlDatabase sqlDB = new SqlDatabase(conn);
            DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_ExpenditureUpdateByPk");
            sqlDB.AddInParameter(dbCMD, "Expenditure_Id", SqlDbType.Int, modelExpenditure.Expenditure_Id);
            sqlDB.AddInParameter(dbCMD, "Expenditure_Name", SqlDbType.NVarChar, modelExpenditure.Expenditure_Name);

            int status = sqlDB.ExecuteNonQuery(dbCMD);
            return status;

        }
        #endregion

        #region PR_Payment_Mode_UpdateByPK
        public int PR_Payment_Mode_UpdateByPK(string conn, PaymentModel modelPayment)
        {
            SqlDatabase sqlDB = new SqlDatabase(conn);
            DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_Payment_Mode_UpdateByPK");
            sqlDB.AddInParameter(dbCMD, "Payment_Id", SqlDbType.Int, modelPayment.Payment_Id);
            sqlDB.AddInParameter(dbCMD, "Payment_Mode", SqlDbType.NVarChar, modelPayment.Payment_Mode);

            int status = sqlDB.ExecuteNonQuery(dbCMD);
            return status;

        }
        #endregion

        #region PR_SourcesUpdateByPk
        public int PR_SourcesUpdateByPk(string conn, SourceModel modelSource)
        {
            SqlDatabase sqlDB = new SqlDatabase(conn);
            DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_SourcesUpdateByPk");
            sqlDB.AddInParameter(dbCMD, "Source_id", SqlDbType.Int, modelSource.Source_Id);
            sqlDB.AddInParameter(dbCMD, "Source_Name", SqlDbType.NVarChar, modelSource.Source_Name);

            int status = sqlDB.ExecuteNonQuery(dbCMD);
            return status;

        }
        #endregion

    }
}
