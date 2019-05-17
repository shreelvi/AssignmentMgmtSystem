using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentMgmt.Models
{
    public class Course : DatabaseNamedRecord
    {
        #region Constructors
        public Course()
        {
        }
        internal Course(MySql.Data.MySqlClient.MySqlDataReader dr)
        {
            Fill(dr);
        }

        #endregion

        #region Private Variables
        private string _Title;
        private string _Description;
        #endregion

        #region Public Variables
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        public string Description
        {
            get { return _Description; }
            private set { _Description = value; }
        }
        #endregion

        #region Database String
        internal const string db_ID = "CourseID";
        internal const string db_Title = "CourseTitle";
        internal const string db_Name = "CourseName";
        internal const string db_Description = "CourseDescription";
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
            _Title = dr.GetString(db_Title);
            _Name = dr.GetString(db_Name);
            //_Description = dr.GetString(db_Description);
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}