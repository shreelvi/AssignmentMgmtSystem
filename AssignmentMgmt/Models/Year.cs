using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentMgmt.Models
{
    public class Year : DatabaseRecord
    {

        #region Constructors
        public Year()
        {
        }
        internal Year(MySql.Data.MySqlClient.MySqlDataReader dr)
        {
            Fill(dr);
        }

        #endregion

        private int _Year;



        public int Year1
        {
            get { return _Year; }
            set { _Year = value; }
        }

        #region Database String
        internal const string db_ID = "YearID";
        internal const string db_Year = "Year";
        #endregion

        #region Public Functions
        public override int dbSave()
        {
            throw new NotImplementedException();
        }

        protected override int dbAdd()
        {
            throw new NotImplementedException();
        }

        protected override int dbUpdate()
        {
            throw new NotImplementedException();
        }

        public int dbRemove()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Public Subs
        public override void Fill(MySql.Data.MySqlClient.MySqlDataReader dr)
        {
            _ID = dr.GetInt32(db_ID);
            _Year = dr.GetInt32(db_Year);
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
