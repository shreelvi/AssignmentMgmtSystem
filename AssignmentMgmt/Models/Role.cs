using AssignmentMgmt.Model;
using AssignmentMgmt.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentMgmt.Models
{
    /// <summary>
    /// Role is a designated position for each user.
    /// Each role can be assigned to zero to many users.
    /// Each role user can have one to multiple permissions. 
    /// </summary>

    public class Role : DatabaseRecord
    {
        #region Constructors
        public Role()
        {
        }
        internal Role(MySql.Data.MySqlClient.MySqlDataReader dr)
        {
            Fill(dr);
        }

        #endregion

        #region Database String
        internal const string db_ID = "ID";
        internal const string db_Name = "Name";
        internal const string db_IsAdmin = "IsAdmin";
        internal const string db_Users = "Users";
        internal const string db_Role = "Role";
        internal const string db_Assignment = "Assignment";
        #endregion

        #region Private Variables
        private string _Name;
        private bool _IsAdmin;
        private PermissionSet _Users;
        private PermissionSet _Role;
        private PermissionSet _Assignment;
        #endregion

        #region public Properites
        /// <summary>
        /// Gets or sets the Name for this PeerVal.Role object.
        /// </summary>
        /// <remarks></remarks>
        /// 
        [Required]
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value.Trim();
            }
        }

        /// <summary>
        /// Gets or sets the IsAdmin for this PeerVal.Role object.
        /// </summary>
        /// <remarks></remarks>

        [Required]
        public bool IsAdmin
        {
            get
            {
                return _IsAdmin;
            }
            set
            {
                _IsAdmin = value;
            }
        }

        /// <summary>
        /// Gets or sets the Users for this PeerVal.Role object.
        /// </summary>
        /// <remarks></remarks>

        [Required]
        [Display(Name = "Users Permissionset")]
        public PermissionSet Users
        {
            get
            {
                return _Users;
            }
            set
            {
                _Users = value;
            }
        }

        /// <summary>
        /// Gets or sets the Role for this Classweb.Role object.
        /// </summary>
        /// <remarks></remarks>
        [Required]
        [Display(Name = "Roles Permissionset")]
        public PermissionSet Roles
        {
            get
            {
                return _Role;
            }
            set
            {
                _Role = value;
            }
        }

        /// <summary>
        /// Gets or sets the Assignment for this Classweb.Role object.
        /// </summary>
        [Required]
        [Display(Name = "Assignment Permissionset")]
        public PermissionSet Assignment
        {
            get
            {
                return _Assignment;
            }
            set
            {
                _Assignment = value;
            }
        }
        #endregion



        #region Public Functions

        public override string ToString()
        {
            return this.GetType().ToString();
        }

        public override int dbSave()
        {
            throw new NotImplementedException();
        }

        protected override int dbAdd()
        {
            _ID = DAL.AddRole(this);
            return ID;
        }

        protected override int dbUpdate()
        {
            return DAL.UpdateRole(this);
        }
        /// <summary>
        /// Calls DAL function to remove Role from the database.
        /// </summary>
        /// <remarks></remarks>
        #endregion

        #region Public Subs
        /// <summary>
        /// Fills object from a MySqlClient Data Reader
        /// </summary>
        /// <remarks></remarks>
        public override void Fill(MySql.Data.MySqlClient.MySqlDataReader dr)
        {
            _ID = dr.GetInt32(db_ID);
            _Name = dr.GetString(db_Name);
            _IsAdmin = dr.GetBoolean(db_IsAdmin);
            _Users = new PermissionSet((byte)dr.GetUInt64(db_Users));
            _Role = new PermissionSet((byte)dr.GetUInt64(db_Role));
            _Assignment = new PermissionSet((byte)dr.GetUInt64(db_Assignment));
        }
        #endregion
    }
}

