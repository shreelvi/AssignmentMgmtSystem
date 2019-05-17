using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentMgmt.Models
{
    public abstract class DatabaseObject
    {

        #region Private Variable
        private int _ID;
        #endregion

        #region Public Class
        //Primary Key for database
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        #endregion
    }
}
