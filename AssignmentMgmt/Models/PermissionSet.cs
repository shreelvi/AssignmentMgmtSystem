using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssignmentMgmt.Models;
using MySql.Data.MySqlClient;

namespace AssignmentMgmt.Models
{
    /// <summary>
    /// Created by: Elvis
    /// Created on: 04/01/2019
    /// Reference: Prof.Holmes Peerval Project
    /// Taken code to have permission set for 
    /// user roles in classweb
    /// DAVE (DELETE,ADD,VIEW,EDIT) (1,1,1,1)
    /// </summary>
    public class PermissionSet : DatabaseRecord
    {
        private byte _DAVESet = 0;

        public enum Permissions
        {
            Edit = 1, View = 2, ViewAndEdit = 3, Add = 4, Delete = 8
        }

        public PermissionSet()
        {

        }
        public PermissionSet(byte perms)
        {
            // we will only use the lower four bits.
            _DAVESet = perms;
        }

        public bool Add
        {
            get
            {
                return IsolateBit(_DAVESet, Permissions.Add);
            }
        }
        public bool Edit
        {
            get
            {
                return IsolateBit(_DAVESet, Permissions.Edit);
            }
        }
        public bool View
        {
            get
            {
                return IsolateBit(_DAVESet, Permissions.View);
            }
        }
        public bool Delete
        {
            get
            {
                return IsolateBit(_DAVESet, Permissions.Delete);
            }
        }

        public bool ViewAndEdit
        {
            get
            {
                return IsolateBit(_DAVESet, Permissions.View)
                    && IsolateBit(_DAVESet, Permissions.Edit);
            }
        }



        public string AsBinary
        {
            get
            {
                //string retString = "";
                //retString += _DAVESet >= (int)Permissions.Delete ? 1 : 0;
                //retString += _DAVESet >= (int)Permissions.Add ? 1 : 0;
                //retString += _DAVESet >= (int)Permissions.View ? 1 : 0;
                //retString += _DAVESet >= (int)Permissions.Edit ? 1 : 0;
                //return retString;
                return Convert.ToString(_DAVESet, 2).PadLeft(4, '0');
            }
        }

        /// <summary>
        /// Had to expose this attribute so that JSON serialization would work
        /// </summary>
        public byte DAVESet { get => _DAVESet; set => _DAVESet = value; }


        // Possible DAVE Values
        // 0001 = 1  -                          Edit
        // 0010 = 2  -                  View
        // 0011 = 3  -                  View    Edit
        // 0100 = 4  -          Add
        // 0101 = 5  -          Add	            Edit
        // 0110 = 6  -          Add	    View
        // 0111 = 7  -          Add	    View	Edit
        // 1000 = 8  - Delete
        // 1001 = 9  - Delete	                Edit
        // 1010 = 10 - Delete	        View
        // 1011 = 11 - Delete	        View	Edit
        // 1100 = 12 - Delete	 Add 
        // 1101 = 13 - Delete	 Add	        Edit
        // 1110 = 14 - Delete	 Add	 View
        // 1111 = 15 - Delete	 Add	 View	Edit


        public bool IsolateBit(byte value, Permissions pm)
        {
            //if (pm == Permissions.Edit) {
            //    // 0001
            //    return value % 2 == 1;
            //} else if (pm == Permissions.View) {
            //    // 0010 => 0001
            //    return (value >> 1) % 2 == 1;
            //} else if (pm == Permissions.Add) {
            //    // 0100 => 0001
            //    return (value >> 2) % 2 == 1;
            //} else if (pm == Permissions.Delete) {
            //    // 1000 => 0001
            //    return (value >> 3) % 2 == 1;
            //} else {
            //    // should not reach here.
            //    // always give back no permission (for security)
            //    return false;
            //}
            return (value / (int)pm) % 2 == 1;
        }

        public override string ToString()
        {
            return _DAVESet.ToString();
        }

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

        public override void Fill(MySqlDataReader dr)
        {
            throw new NotImplementedException();
        }

        public static bool operator >=(PermissionSet ps, Permissions pm)
        {
            return ps.IsolateBit(ps._DAVESet, pm);
        }

        public static bool operator <=(PermissionSet ps, Permissions pm)
        {
            return !(ps >= pm);
        }
    }
}