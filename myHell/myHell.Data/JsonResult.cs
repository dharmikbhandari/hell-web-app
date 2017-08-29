using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myHell.Data
{
    public class JsonResult
    {
        /// <summary>
        /// Gets or set the Object
        /// </summary>
        public object Object { get; set; }

        /// <summary>
        /// Gets or set the Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or set the Error
        /// </summary>
        public string Error { get; set; }
    }
}
