using AssignmentMgmt.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AssignmentMgmt.Models
{
    /// <summary>

    /// Everyone is a user when they sign up except Admin.
    /// Special permission will be provided based on the roles assigned to them on the system.
    /// Every user can login to the system unless deleted.
    /// </summary>

    public class User : DatabaseRecord
    {

        #region Constructors
        /// <summary>
        /// Code By Elvis
        /// Constructor to map results of sql query to the class
        /// Reference: GitHub PeerVal Project
        /// </summary>
        public User()
        {
        }
        internal User(MySql.Data.MySqlClient.MySqlDataReader dr)
        {
            Fill(dr);
        }

        #endregion

        #region private variable
        private string _FirstName;
        private string _MiddleName;
        private string _LastName;
        private string _EmailAddress;
        private string _ResetCode;
        private string _UserName;
        private string _Password;
        private string _Salt;
        private int _RoleID;
        private Role _Role;
        private string _DirectoryPath;
        private List<Assignment> _Assignments;
        private DateTime _DateCreated;
        private DateTime _DateModified;
        private DateTime _DateDeleted;
        private int _Enabled;
        private int _Archived;
        private string _VerificationCode;
        #endregion

        #region Database String
        internal const string db_ID = "UserID";
        internal const string db_FirstName = "FirstName";
        internal const string db_MiddleName = "MiddleName";
        internal const string db_LastName = "LastName";
        internal const string db_EmailAddress = "EmailAddress";
        internal const string db_UserName = "UserName";
        internal const string db_Salt = "Salt";
        internal const string db_Role = "RoleID";
        internal const string db_Password = "Password";
        internal const string db_ResetCode = "ResetCode";
        internal const string db_DateCreated = "DateCreated";
        internal const string db_DateModified = "DateModified";
        internal const string db_DateDeleted = "DateDeleted";
        internal const string db_Archived = "Archived";
        internal const string db_Enabled = "Enabled";
        internal const string db_VerificationCode = "VerificationCode";
        #endregion

        #region public Properites

        public int Enabled
        {
            get { return _Enabled; }
            set { _Enabled = value; }
        }
        public string VerificationCode
        {
            get { return _VerificationCode; }
            set { _VerificationCode = value; }
        }
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        public string ResetCode
        {
            get { return _ResetCode; }
            set { _ResetCode = value; }
        }
        public string MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        public string EmailAddress
        {
            get { return _EmailAddress; }
            set { _EmailAddress = value; }
        }
        //public string Address
        //{
        //    get { return _Address; }
        //    set { _Address = value; }
        //}
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        /// <summary>
        /// Gets or sets the Salt for this PeerVal.User object
        /// </summary>
        public string Salt
        {
            get { return _Salt; }
            set { _Salt = value; }
        }

        /// <summary>
        /// Gets or sets the RoleID for this PeerVal.User object.
        /// </summary>
        /// <remarks></remarks>
        public int RoleID
        {
            get
            {
                return _RoleID;
            }
            set
            {
                _RoleID = value;
            }
        }

        public DateTime DateCreated
        {
            get { return _DateCreated; }
            set { _DateCreated = value; }
        }
        public DateTime DateModified
        {
            get { return _DateModified; }
            set { _DateModified = value; }
        }
        public DateTime DateDeleted
        {
            get { return _DateDeleted; }
            set { _DateDeleted = value; }
        }


        /// <summary>
        /// Gets or sets the Role for this User object.
        /// Reference: Taken code from prof. Holmes Peerval Project
        /// </summary>
        /// <remarks></remarks>

        [XmlIgnore]
        public Role Role
        {
            get
            {
                if (_Role == null)
                {
                    _Role = Roles.Get(_RoleID);//DAL.GetRole(_RoleID);
                }
                return _Role;
            }
            set
            {
                _Role = value;
                if (value == null)
                {
                    _RoleID = -1;
                }
                else
                {
                    _RoleID = value.ID;
                }
            }
        }

        public string DirectoryPath
        {
            get { return _DirectoryPath; }
            set { _DirectoryPath = value; }
        }

        public List<Assignment> Assignments
        {
            get { return _Assignments; }
            set { _Assignments = value; }
        }

        //public long PhoneNumber
        //{
        //    get { return _PhoneNumber; }
        //    set { _PhoneNumber = value; }
        //}

        //public bool AccountExpired
        //{
        //    get { return _AccountExpired; }
        //    set { _AccountExpired = value; }
        //}

        public int Archived
        {
            get { return _Archived; }
            set { _Archived = value; }
        }

        //public bool PasswordExpired
        //{
        //    get { return _PasswordExpired; }
        //    set { _PasswordExpired = value; }
        //}

        //public bool Enabled
        //{
        //    get { return _Enabled; }
        //    set { _Enabled = value; }
        //}
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
        #endregion

        #region Public Subs
        /// <summary>
        /// Fills object from a MySqlClient Data Reader
        /// </summary>
        /// <remarks></remarks>
        public override void Fill(MySql.Data.MySqlClient.MySqlDataReader dr)
        {
            _ID = dr.GetInt32(db_ID);
            _FirstName = dr.GetString(db_FirstName);
            _ResetCode = dr.GetString(db_ResetCode);
            _LastName = dr.GetString(db_LastName);
            _EmailAddress = dr.GetString(db_EmailAddress);
            _Password = dr.GetString(db_Password);
            DateTime DateCreated = dr.GetDateTime(db_DateCreated);
            _DateModified = dr.GetDateTime(db_DateModified);
            // _DateModified = DateTime.Parse(DateModified.ToString());
            _DateDeleted = dr.GetDateTime(db_DateDeleted);
            _Salt = dr.GetString(db_Salt);
            _RoleID = dr.GetInt32(db_Role);
            _UserName = dr.GetString(db_UserName);
            _Enabled = dr.GetInt32(db_Enabled);
            _Archived = dr.GetInt32(db_Archived);
            _VerificationCode = dr.GetString(db_VerificationCode);
        }
        #endregion

        public override string ToString()
        {
            return this.GetType().ToString();
        }
    }
}