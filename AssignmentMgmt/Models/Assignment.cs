using Microsoft.AspNetCore.Antiforgery.Internal;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentMgmt.Models
{
    /// <summary>
    /// Assignment class is used to add, update, and save objects
    /// It also contains ID 
    /// Reference: GitHub Prof. Holmes PeerVal Project
    /// </summary>
    public class Assignment : DatabaseRecord
    {
        #region Database String
        internal const string db_ID = "ID";
        internal const string db_Location = "FileLocation";
        internal const string db_FileName = "FileName";
        internal const string db_DateStarted = "DateStarted";
        internal const string db_DateDue = "DateDue";
        internal const string db_DateSubmited = "DateSubmited";
        internal const string db_Grade = "Grade";
        internal const string db_Feedback = "Feedback";
        internal const string db_FileSize = "FileSize";
        internal const string db_IsEditable = "IsEditable";
        internal const string db_DateModified = "DateModified";
        internal const string db_UserName = "UserName";
        #endregion
        public Assignment(MySqlDataReader dr)
        {
            Fill(dr);
        }
        public Assignment()
        {
        }
        public override void Fill(MySqlDataReader dr)
        {
            _ID = dr.GetInt32(db_ID);
            _FileName = dr.GetString(db_FileName);
            _FileLocation = dr.GetString(db_Location);
            _DateModified = dr.GetDateTime(db_DateModified);
            _DateSubmited = dr.GetDateTime(db_DateSubmited);
            _Feedback = dr.GetString(db_Feedback);
            _FileSize = dr.GetInt64(db_FileSize);
            _Grade = dr.GetInt32(db_Grade);
            _IsEditable = dr.GetBoolean(db_IsEditable);
            _UserName = dr.GetString(db_UserName);
        }

        public override int dbSave()
        {
            throw new NotImplementedException();
        }

        protected override int dbUpdate()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        protected override int dbAdd()
        {
            throw new NotImplementedException();
        }
        #region Private Variable
        protected DateTime _DateStarted;
        protected DateTime _DateDue;
        protected DateTime _DateSubmited;
        protected int _Grade;
        protected string _Feedback;
        protected double _FileSize;
        protected bool _IsEditable;
        protected DateTime _DateModified;
        protected string _FileName;
        protected string _FileLocation;
        protected string _UserName;
        #endregion

        #region Public Properties
        public string FileLocation
        {
            get
            {
                return _FileLocation;
            }
            set
            {
                _FileLocation = value;
            }
        }
        public string UserName
        {
            get
            {
                return _UserName;
            }
            set
            {
                _UserName = value;
            }
        }
        public string FileName
        {
            get
            {
                return _FileName;
            }
            set
            {
                _FileName = value;
            }
        }
        public bool IsEditable
        {
            get
            {
                return _IsEditable;
            }
            set
            {
                _IsEditable = value;
            }
        }
        public double FileSize
        {
            get
            {
                return _FileSize;
            }
            set
            {
                _FileSize = value;
            }
        }
        public DateTime DateStarted
        {
            get { return _DateStarted; }
            set { _DateStarted = value; }
        }
        public DateTime DateModified
        {
            get { return _DateModified; }
            set { _DateModified = value; }
        }

        [Display(Name = "Date Due")]
        public DateTime DateDue
        {
            get { return _DateDue; }
            set { _DateDue = value; }
        }
        [Display(Name = "Date Submited")]
        public DateTime DateSubmited
        {
            get { return _DateSubmited; }
            set { _DateSubmited = value; }
        }

        public int Grade
        {
            get { return _Grade; }
            set
            {
                if (value > 100)
                {
                    _Grade = 100;
                }
                if (value < 0)
                {
                    _Grade = 0;
                }
                _Grade = value;
            }
        }

        public string Feedback
        {
            get { return _Feedback; }
            set { _Feedback = value; }
        }
        #endregion
    }
}