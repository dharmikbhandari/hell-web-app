using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myHell.Data
{
    public class MyHellServices
    {
        #region Fields

        DbEntities _dbEntities = new DbEntities();
        private const string _NO_ERROR = "NoError";
        
        #endregion

        #region Methods

        /// <summary>
        /// Get User By Name
        /// </summary>
        /// <param name="name">name</param>
        /// <returns>User Dataset</returns>
        public DataSet GetUserByName(string name)
        {
            DataSet dataSet = new DataSet();

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id", typeof(int));
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Email", typeof(string));
            dataTable.Columns.Add("Password", typeof(string));
            dataTable.Columns.Add("Active", typeof(bool));

            dataTable.Rows.Add(1, "Dharmik", "d@d.com", "12345", true);

            dataSet.Tables.Add(dataTable);

            return dataSet;
        }

        /// <summary>
        /// Get User By Id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>User Dataset</returns>
        public DataSet GetUserById(int id)
        {
            DataSet dataSet = new DataSet();

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id", typeof(int));
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Email", typeof(string));
            dataTable.Columns.Add("Password", typeof(string));
            dataTable.Columns.Add("Active", typeof(bool));

            dataTable.Rows.Add(1, "Dharmik", "d@d.com", "12345", true);

            dataSet.Tables.Add(dataTable);

            return dataSet;
        }


        /// <summary>
        /// Get All Users
        /// </summary>
        /// <returns>User Dataset</returns>
        public DataSet GetAllUser(int pageSize,int pageIndex)
        {
           

            DataSet dataSet = new DataSet();

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id", typeof(int));
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Email", typeof(string));
            dataTable.Columns.Add("Password", typeof(string));
            dataTable.Columns.Add("Active", typeof(bool));

            dataTable.Rows.Add(1, "Dharmik1", "d@d.com", "12345", true);
            dataTable.Rows.Add(2, "Dharmik2", "d@d.com", "12345", true);
            dataTable.Rows.Add(3, "Dharmik3", "d@d.com", "12345", true);
            dataTable.Rows.Add(4, "Dharmik4", "d@d.com", "12345", true);
            dataTable.Rows.Add(5, "Dharmik5", "d@d.com", "12345", true);
            dataTable.Rows.Add(6, "Dharmik6", "d@d.com", "12345", true);
            dataTable.Rows.Add(7, "Dharmik7", "d@d.com", "12345", false);
            dataTable.Rows.Add(8, "Dharmik8", "d@d.com", "12345", true);
            dataTable.Rows.Add(9, "Dharmik9", "d@d.com", "12345", true);
            dataTable.Rows.Add(10, "Dharmik10", "d@d.com", "12345", true);

            dataSet.Tables.Add(dataTable);

            return dataSet;
        }


        /// <summary>
        /// Is valid login
        /// </summary>
        /// <param name="email">email</param>
        /// <param name="password">password</param>
        /// <returns>true/false</returns>
        public bool IsValidLogin(string email,string password)
        {
            //if(email.Equals("dharmik_bhandari@hotmail.com") && password.Equals("As@12345"))
            if (email=="dharmik_bhandari@hotmail.com" && password=="12345")
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Insert User
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="name">name</param>
        /// <param name="email">email</param>
        /// <param name="password">password</param>
        /// <param name="active">active</param>
        /// <returns>storedProcedureOutput</returns>
        public StoredProcedureOutput InsertUser(int id,string name,string email,string password,bool active)
        {
            StoredProcedureOutput storedProcedureOutput = new StoredProcedureOutput();
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            Dictionary<string, string> outputParameter = new Dictionary<string, string>();
            
            sqlParams.Add(new SqlParameter()
            {
                ParameterName = "@Id",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                Value = id
            });


            sqlParams.Add(new SqlParameter()
            {
                ParameterName = "@Name",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = name
            });


            sqlParams.Add(new SqlParameter()
            {
                ParameterName = "@Email",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = email
            });

            sqlParams.Add(new SqlParameter()
            {
                ParameterName = "@Password",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = password
            });

            sqlParams.Add(new SqlParameter()
            {
                ParameterName = "@Active",
                SqlDbType = SqlDbType.Bit,
                Direction = ParameterDirection.Input,
                Value = active
            });

           
            sqlParams.Add(new SqlParameter()
            {
                ParameterName = "@Error",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Output,
                Size = 5000
            });

            sqlParams.Add(new SqlParameter()
            {
                ParameterName = "@Message",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Output,
                Size = 5000
            });

            //Execute Stored Procedure
            storedProcedureOutput.DataSet = _dbEntities.Execute("myHell_SET_USER", sqlParams, out outputParameter);
            if (outputParameter.Keys.Count > 0 && outputParameter.ContainsKey("@Error"))
            {

                if (outputParameter["@Error"] == _NO_ERROR)
                {
                    if (outputParameter.ContainsKey("@Message"))
                    {
                        storedProcedureOutput.Message = outputParameter["@Message"];
                    }
                }
                else
                {
                    storedProcedureOutput.Error = outputParameter["@Error"];
                }

            }

            return storedProcedureOutput;

        }


        #endregion
    }
}
