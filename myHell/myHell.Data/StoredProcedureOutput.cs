using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myHell.Data
{
    public class StoredProcedureOutput
    {
        /// <summary>
        /// Gets or set the dataSet
        /// </summary>
        public DataSet DataSet { get; set; }

        /// <summary>
        /// Gets or set the Error
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// Gets or set the Message
        /// </summary>
        public string Message { get; set; }
       
    }
}
