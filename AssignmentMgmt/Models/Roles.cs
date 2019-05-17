using AssignmentMgmt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentMgmt.Models
{
    /// <summary>
    /// Created by: Elvis
    /// Created on: 04/03/2019
    /// Static class that is used to get and manage user roles
    /// Reference: Code taken from prof.holmes peerval project
    /// </summary>
    public static class Roles
    {
        private static List<Role> _List;

        public static List<Role> List
        {
            get
            {
                if (_List == null)
                    _List = DAL.GetRoles();
                return _List;
            }
            set { _List = value; }
        }

        public static void ResetList()
        {
            _List = null;
        }

        internal static Role Get(int roleID)
        {
            return List.FirstOrDefault(r => r.ID == roleID);
        }
    }
}
