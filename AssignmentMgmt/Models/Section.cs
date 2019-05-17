using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MySql.Data.MySqlClient;

namespace AssignmentMgmt.Models
{
    public class Section : DatabaseRecord
    {
        #region Constructors
        public Section()
        {
        }
        internal Section(MySql.Data.MySqlClient.MySqlDataReader dr)
        {
            Fill(dr);
        }

        #endregion

        #region Private Variables
        private int _SectionNumber;
        //private int _CRN;
        //private int _ClassID;


        #endregion


        #region Public Properties

        public int SectionNumber
        {
            get
            {
                return _SectionNumber;
            }

            set
            {
                _SectionNumber = value;
            }
        }

        


        #endregion

        #region Database String
        internal const string db_ID = "SectionID";
        internal const string db_Number = "SectionNumber";
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
        public override void Fill(MySqlDataReader dr)
        {
            _ID = dr.GetInt32(db_ID);
            _SectionNumber = dr.GetInt32(db_Number);
        }
        #endregion


        public override string ToString()
        {
            return this.GetType().ToString();
        }


    }
}
