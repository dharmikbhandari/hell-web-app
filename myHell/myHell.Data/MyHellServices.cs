using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myHell.Data
{
    public class MyHellServices
    {
        #region Fields

        DbEntities _dbEntities = new DbEntities();

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

        #endregion
    }
}
