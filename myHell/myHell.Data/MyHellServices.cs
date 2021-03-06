﻿using System;
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
        private const string _NO_MESSAGE = "NoMessage";

        #endregion

        #region Methods

        #region User

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
        public StoredProcedureOutput GetUserById(int id)
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

            
            storedProcedureOutput.DataSet = _dbEntities.Execute("myHell_GET_USER", sqlParams, out outputParameter);
            if (outputParameter.Keys.Count > 0 && outputParameter.ContainsKey("@Error"))
            {

                if (outputParameter["@Error"] == _NO_ERROR)
                {
                    if (outputParameter.ContainsKey("@Message"))
                    {
                        if (outputParameter["@Message"] != _NO_MESSAGE)
                        {
                            storedProcedureOutput.Message = outputParameter["@Message"];
                        }
                    }
                }
                else
                {
                    storedProcedureOutput.Error = outputParameter["@Error"];
                }

            }

            return storedProcedureOutput;
        }


        /// <summary>
        /// Get All Users
        /// </summary>
        /// <returns>User Dataset</returns>
        public StoredProcedureOutput GetAllUser(int pageSize,int pageIndex)
        {
            StoredProcedureOutput storedProcedureOutput = new StoredProcedureOutput();
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            Dictionary<string, string> outputParameter = new Dictionary<string, string>();

            sqlParams.Add(new SqlParameter()
            {
                ParameterName = "@Id",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                Value = 0
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

           
            storedProcedureOutput.DataSet = _dbEntities.Execute("myHell_GET_USER", sqlParams, out outputParameter);
            if (outputParameter.Keys.Count > 0 && outputParameter.ContainsKey("@Error"))
            {

                if (outputParameter["@Error"] == _NO_ERROR)
                {
                    if (outputParameter.ContainsKey("@Message"))
                    {
                        if (outputParameter["@Message"] != _NO_MESSAGE)
                        {
                            storedProcedureOutput.Message = outputParameter["@Message"];
                        }
                    }
                }
                else
                {
                    storedProcedureOutput.Error = outputParameter["@Error"];
                }

            }

            return storedProcedureOutput;
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

            
            storedProcedureOutput.DataSet = _dbEntities.Execute("myHell_SET_USER", sqlParams, out outputParameter);
            if (outputParameter.Keys.Count > 0 && outputParameter.ContainsKey("@Error"))
            {

                if (outputParameter["@Error"] == _NO_ERROR)
                {
                    if (outputParameter.ContainsKey("@Message"))
                    {
                        if (outputParameter["@Message"] != _NO_MESSAGE)
                        {
                            storedProcedureOutput.Message = outputParameter["@Message"];
                        }
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

        #region Category

        /// <summary>
        /// Get Category By Name
        /// </summary>
        /// <param categoryName="categoryName">name</param>
        /// <returns>User Dataset</returns>
        public DataSet GetCategoryByName(string categoryName)
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
        /// Get Category By Id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>User Dataset</returns>
        public StoredProcedureOutput GetCategoryById(int id)
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

            
            storedProcedureOutput.DataSet = _dbEntities.Execute("myHell_GET_Category", sqlParams, out outputParameter);
            if (outputParameter.Keys.Count > 0 && outputParameter.ContainsKey("@Error"))
            {

                if (outputParameter["@Error"] == _NO_ERROR)
                {
                    if (outputParameter.ContainsKey("@Message"))
                    {
                        if (outputParameter["@Message"] != _NO_MESSAGE)
                        {
                            storedProcedureOutput.Message = outputParameter["@Message"];
                        }
                    }
                }
                else
                {
                    storedProcedureOutput.Error = outputParameter["@Error"];
                }

            }

            return storedProcedureOutput;
        }


        /// <summary>
        /// Get All Categories
        /// </summary>
        /// <returns>Category Dataset</returns>
        public StoredProcedureOutput GetAllCategories(int pageSize, int pageIndex)
        {
            StoredProcedureOutput storedProcedureOutput = new StoredProcedureOutput();
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            Dictionary<string, string> outputParameter = new Dictionary<string, string>();

            sqlParams.Add(new SqlParameter()
            {
                ParameterName = "@Id",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                Value = 0
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

            
            storedProcedureOutput.DataSet = _dbEntities.Execute("myHell_GET_Category", sqlParams, out outputParameter);
            if (outputParameter.Keys.Count > 0 && outputParameter.ContainsKey("@Error"))
            {

                if (outputParameter["@Error"] == _NO_ERROR)
                {
                    if (outputParameter.ContainsKey("@Message"))
                    {
                        if (outputParameter["@Message"] != _NO_MESSAGE)
                        {
                            storedProcedureOutput.Message = outputParameter["@Message"];
                        }
                    }
                }
                else
                {
                    storedProcedureOutput.Error = outputParameter["@Error"];
                }

            }

            return storedProcedureOutput;
        }


        

        /// <summary>
        /// Insert Category
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="categoryName">categoryName</param>
        /// <param name="cateogryType">categoryType</param>
        /// <param name="active">active</param>
        /// <returns>storedProcedureOutput</returns>
        public StoredProcedureOutput InsertCategory(int id, string categoryName, string categoryType,  bool active)
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
                ParameterName = "@Category_Name",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = categoryName
            });


            sqlParams.Add(new SqlParameter()
            {
                ParameterName = "@Category_Type",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = categoryType
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

            
            storedProcedureOutput.DataSet = _dbEntities.Execute("myHell_SET_Category", sqlParams, out outputParameter);
            if (outputParameter.Keys.Count > 0 && outputParameter.ContainsKey("@Error"))
            {

                if (outputParameter["@Error"] == _NO_ERROR)
                {
                    if (outputParameter.ContainsKey("@Message"))
                    {
                        if (outputParameter["@Message"] != _NO_MESSAGE)
                        {
                            storedProcedureOutput.Message = outputParameter["@Message"];
                        }
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

        #region Transanction

        /// <summary>
        /// Get Transaction By Id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>User Dataset</returns>
        public StoredProcedureOutput GetTransactionById(int id)
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


            storedProcedureOutput.DataSet = _dbEntities.Execute("myHell_GET_Transaction", sqlParams, out outputParameter);
            if (outputParameter.Keys.Count > 0 && outputParameter.ContainsKey("@Error"))
            {

                if (outputParameter["@Error"] == _NO_ERROR)
                {
                    if (outputParameter.ContainsKey("@Message"))
                    {
                        if (outputParameter["@Message"] != _NO_MESSAGE)
                        {
                            storedProcedureOutput.Message = outputParameter["@Message"];
                        }
                    }
                }
                else
                {
                    storedProcedureOutput.Error = outputParameter["@Error"];
                }

            }

            return storedProcedureOutput;
        }


        /// <summary>
        /// Get All Transactions
        /// </summary>
        /// <returns>User Dataset</returns>
        public StoredProcedureOutput GetAllTransactions(int pageSize, int pageIndex)
        {
            StoredProcedureOutput storedProcedureOutput = new StoredProcedureOutput();
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            Dictionary<string, string> outputParameter = new Dictionary<string, string>();

            sqlParams.Add(new SqlParameter()
            {
                ParameterName = "@Id",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                Value = 0
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


            storedProcedureOutput.DataSet = _dbEntities.Execute("myHell_GET_Transaction", sqlParams, out outputParameter);
            if (outputParameter.Keys.Count > 0 && outputParameter.ContainsKey("@Error"))
            {

                if (outputParameter["@Error"] == _NO_ERROR)
                {
                    if (outputParameter.ContainsKey("@Message"))
                    {
                        if (outputParameter["@Message"] != _NO_MESSAGE)
                        {
                            storedProcedureOutput.Message = outputParameter["@Message"];
                        }
                    }
                }
                else
                {
                    storedProcedureOutput.Error = outputParameter["@Error"];
                }

            }

            return storedProcedureOutput;
        }

        
        /// <summary>
        /// Insert Transaction
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="amount">amount</param>
        /// <param name="categoryId">categoryId</param>
        /// <param name="userId">userId</param>
        /// <param name="active">active</param>
        /// <returns>storedProcedureOutput</returns>
        public StoredProcedureOutput InsertTransaction(int id, decimal amount,int categoryId,int userId, bool active)
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
                ParameterName = "@Amount",
                SqlDbType = SqlDbType.Decimal,
                Direction = ParameterDirection.Input,
                Value = amount
            });

            sqlParams.Add(new SqlParameter()
            {
                ParameterName = "@CategoryId",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                Value = categoryId
            });


            sqlParams.Add(new SqlParameter()
            {
                ParameterName = "@UserId",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                Value = userId
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


            storedProcedureOutput.DataSet = _dbEntities.Execute("myHell_SET_Transaction", sqlParams, out outputParameter);
            if (outputParameter.Keys.Count > 0 && outputParameter.ContainsKey("@Error"))
            {

                if (outputParameter["@Error"] == _NO_ERROR)
                {
                    if (outputParameter.ContainsKey("@Message"))
                    {
                        if (outputParameter["@Message"] != _NO_MESSAGE)
                        {
                            storedProcedureOutput.Message = outputParameter["@Message"];
                        }
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



        #endregion
    }
}
