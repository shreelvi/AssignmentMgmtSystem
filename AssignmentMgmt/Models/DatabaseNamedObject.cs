using AssignmentMgmt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentMgmt.Models
{
    // <Summary>
    // Code by Elvis
    // DatabaseNamed Object inherits DatabaseObject includes the name or title of a class
    // Other Classes can inherit this class to use primary ID and Name property
    // </Summary>

    public abstract class DatabaseNamedObject :DatabaseObject
    {
        private string _Name;
        private DateTime _DateCreated;
        private DateTime _DateModified;
        private DateTime _DateDeleted;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
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
    }
}
