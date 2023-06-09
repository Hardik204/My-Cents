﻿using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using Expense_Manager.Areas.SEC_User.Models;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Expense_Manager.BAL;
using Expense_Manager.Areas.Logbooks.Models;

namespace Expense_Manager.DAL
{
	public class SEC_DALBase
	{
		#region Method: dbo_PR_SEC_User_SelectByUsernamePassword
		public DataTable dbo_PR_SEC_User_SelectByUserNamePassword(string conn, string Username, string Password)
		{
			try
			{
				SqlDatabase sqlDB = new SqlDatabase(conn);
				DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_SEC_UserSelectByUsernamePassword");
				sqlDB.AddInParameter(dbCMD, "Username", SqlDbType.VarChar, Username);
				sqlDB.AddInParameter(dbCMD, "Password", SqlDbType.VarChar, Password);

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

		#region Method: dbo_PR_SEC_User_Insert
		public decimal? dbo_PR_SEC_User_Insert(string conn, SEC_UserModel modelSEC_User)
		{
			try
			{
				SqlDatabase sqlDB = new SqlDatabase(conn);
				DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_SEC_UserInsert");
				sqlDB.AddInParameter(dbCMD, "First_Name", SqlDbType.VarChar, modelSEC_User.First_Name);
				sqlDB.AddInParameter(dbCMD, "Last_Name", SqlDbType.VarChar, modelSEC_User.Last_Name);
				sqlDB.AddInParameter(dbCMD, "Email", SqlDbType.VarChar, modelSEC_User.Email);
				sqlDB.AddInParameter(dbCMD, "PhoneNumber", SqlDbType.BigInt, modelSEC_User.ContactNumber);
				sqlDB.AddInParameter(dbCMD, "Username", SqlDbType.VarChar, modelSEC_User.Username);
				sqlDB.AddInParameter(dbCMD, "Password", SqlDbType.VarChar, modelSEC_User.Password);

				var vResult = sqlDB.ExecuteNonQuery(dbCMD);
				return vResult;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
        #endregion


        #region SEC_UserSelectByPk
		public SEC_UserModel PR_SEC_UserSelectByPk(string conn)
		{
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_SEC_UserSelectByPk");
                sqlDB.AddInParameter(dbCMD, "User_Id", SqlDbType.Int, CV.User_Id());

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }

                SEC_UserModel modelSEC_User = new SEC_UserModel();
                foreach (DataRow drow in dt.Rows)
                {
                    modelSEC_User.User_Id = Convert.ToInt32(drow["User_Id"]);
                    modelSEC_User.First_Name = drow["First_Name"].ToString();
                    modelSEC_User.Last_Name = drow["Last_Name"].ToString();
                    modelSEC_User.Email = drow["Email"].ToString();
                    modelSEC_User.ContactNumber = Convert.ToInt64(drow["ContactNumber"]);
                    modelSEC_User.Username = drow["Username"].ToString();
                    modelSEC_User.AvatarLocation = drow["Location"].ToString();
                }
                return modelSEC_User;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion


        #region SEC_UserSelectByPkdt
        public DataTable PR_SEC_UserSelectByPkdt(string conn)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_SEC_UserSelectByPk");
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



        #region SEC_User_UpdateByPk
        public int PR_SEC_User_UpdateByPk(string conn , SEC_UserModel modelSEC_User)
		{
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_SEC_User_UpdateByPk");
                sqlDB.AddInParameter(dbCMD, "User_Id", SqlDbType.Int, CV.User_Id());
                sqlDB.AddInParameter(dbCMD, "Username", SqlDbType.NVarChar, modelSEC_User.Username);
                sqlDB.AddInParameter(dbCMD, "First_Name", SqlDbType.NVarChar, modelSEC_User.First_Name);
                sqlDB.AddInParameter(dbCMD, "Last_Name", SqlDbType.NVarChar, modelSEC_User.Last_Name);
                sqlDB.AddInParameter(dbCMD, "PhoneNumber", SqlDbType.BigInt, modelSEC_User.ContactNumber);
                sqlDB.AddInParameter(dbCMD, "Email", SqlDbType.NVarChar, modelSEC_User.Email);

                int status = sqlDB.ExecuteNonQuery(dbCMD);
                return status;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        #endregion


        #region SelectAllAvatars
        public DataTable PR_SelectAllAvatars(string conn)
		{
			try
			{
				SqlDatabase sqlDB = new SqlDatabase(conn);
				DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_SelectAllAvatars");

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

		#region SetAvatar
		public int PR_SetAvatar(string conn, int Photo_Id)
		{
			try
			{
				SqlDatabase sqlDB = new SqlDatabase(conn);
				DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_SetAvatar");
				sqlDB.AddInParameter(dbCMD, "Photo_Id", SqlDbType.Int, Photo_Id);
				sqlDB.AddInParameter(dbCMD, "User_Id", SqlDbType.Int, CV.User_Id());

				int status = sqlDB.ExecuteNonQuery(dbCMD);
				return status;
			}
			catch (Exception ex)
			{
				return -1;
			}
		}
		#endregion

		#region PR_SelectAvatarByPK
		public String PR_SelectAvatarByPK(string conn, int Photo_Id)
		{
			try
			{
				SqlDatabase sqlDB = new SqlDatabase(conn);
				DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_SelectAvatarByPK");
				sqlDB.AddInParameter(dbCMD, "Avatar_Id", SqlDbType.Int, Photo_Id);

				DataTable dt = new DataTable();
				using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
				{
					dt.Load(dr);
				}
				foreach (DataRow dr in dt.Rows)
				{
					String loc = dr["Location"].ToString();
					return loc;
				}
				return "";
			}
			catch (Exception ex)
			{
				return "";
			}
			#endregion
		}
	}
}

