using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace myHell.Data
{

    /// <summary>
    /// Database connection class
    /// </summary>
    public class DbEntities
    {
        /// <summary>
        /// Get Connection
        /// </summary>
        /// <returns>string</returns>
        private string GetConnection()
        {
            return string.Empty;
        }

        /// <summary>
        /// Excecute stored procedure
        /// </summary>
        /// <param name="procedureName">pass procedure name</param>
        /// <param name="parameters">params</param>
        /// <param name="outputParams">ouputparam</param>
        /// <returns>dataset</returns>
        public DataSet Execute(string procedureName, List<SqlParameter> parameters, out Dictionary<string, string> outputParams)
        {
            var dataSet = new DataSet();
            outputParams = new Dictionary<string, string>();
            var storeProcedureName = "NOPROCEDURE";

            if (!String.IsNullOrWhiteSpace(procedureName))
            {
                storeProcedureName = procedureName;
            }

            using (SqlConnection sqlConn = new SqlConnection(GetConnection()))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConn;
                    sqlCommand.CommandTimeout = 300000;
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandText = storeProcedureName;
                    if (parameters != null)
                    {
                        sqlCommand.Parameters.AddRange(parameters.ToArray());
                    }
                    
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                    {
                        sqlDataAdapter.Fill(dataSet);
                    }
                    
                    //fetch output parameter values
                    foreach (var output in parameters)
                    {
                        if (output.Direction == ParameterDirection.Output || output.Direction == ParameterDirection.InputOutput)
                        {
                            if (!outputParams.ContainsKey(output.ParameterName.ToString()))
                            {
                                outputParams.Add(output.ParameterName.ToString(), output.Value.ToString());
                            }
                        }
                    }
                }
            }

            return dataSet;
        }


    }
}
