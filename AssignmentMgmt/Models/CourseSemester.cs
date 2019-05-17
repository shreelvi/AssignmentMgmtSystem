using System;
using System.Xml.Serialization;
using AssignmentMgmt.Model;
using MySql.Data.MySqlClient;

namespace AssignmentMgmt.Models
{
    public class CourseSemester : DatabaseRecord
    {

        #region Constructors
        public CourseSemester()
        {
        }
        internal CourseSemester
(MySql.Data.MySqlClient.MySqlDataReader dr)
        {
            Fill(dr);
        }

        #endregion

        #region Private Variables
        private int _CRN;
        private int _CourseID;
        private int _SemesterID;
        private int _YearID;
        private int _SectionID;
        private int _UserID;
        private DateTime _DateStart;
        private DateTime _DateEnd;
        private Course _Course;
        private Semester _Semester;
        private Year _Year;
        private Section _Section;

        #endregion

        #region Public Properties
        public int CRN
        {
            get
            {
                return _CRN;
            }

            set
            {
                _CRN = value;
            }
        }

        public int CourseID
        {
            get
            {
                return _CourseID;
            }

            set
            {
                _CourseID = value;
            }
        }

        public int SemesterID
        {
            get
            {
                return _SemesterID;
            }

            set
            {
                _SemesterID = value;
            }
        }

        public int YearID
        {
            get
            {
                return _YearID;
            }

            set
            {
                _YearID = value;
            }
        }

        public int SectionID
        {
            get
            {
                return _SectionID;
            }

            set
            {
                _SectionID = value;
            }
        }

        public int UserID
        {
            get
            {
                return _UserID;
            }

            set
            {
                _UserID = value;
            }
        }

        public DateTime DateStart
        {
            get
            {
                return _DateStart;
            }

            set
            {
                _DateStart = value;
            }
        }

        public DateTime DateEnd
        {
            get
            {
                return _DateEnd;
            }

            set
            {
                _DateEnd = value;
            }
        }




        /// <summary>
        /// Gets or sets the Course for this object.
        /// Reference: Taken code from prof. Holmes Peerval Project
        /// </summary>
        /// <remarks></remarks>
        [XmlIgnore]
        public Course Course
        {
            get
            {
                if (_Course == null)
                {
                   // _Course = DAL.GetCourse(_CourseID);
                }
                return _Course;
            }
            set
            {
                _Course = value;
                if (value == null)
                {
                    _CourseID = -1;
                }
                else
                {
                    _CourseID = value.ID;
                }
            }
        }

        /// <summary>
        /// Gets or sets the User for this object.
        /// Reference: Taken code from prof. Holmes Peerval Project
        /// </summary>
        /// <remarks></remarks>
        [XmlIgnore]
        public Year Year
        {
            get
            {
                if (_Year == null)
                {
                    _Year = DAL.GetYear(_YearID);
                }
                return _Year;
            }
            set
            {
                _Year = value;
                if (value == null)
                {
                    _YearID = -1;
                }
                else
                {
                    _YearID = value.ID;
                }
            }
        }

        /// <summary>
        /// Gets or sets the Semester for this object.
        /// Reference: Taken code from prof. Holmes Peerval Project
        /// </summary>
        /// <remarks></remarks>
        [XmlIgnore]
        public Semester Semester
        {
            get
            {
                if (_Semester == null)
                {
                    _Semester = DAL.GetSemester(_SemesterID);
                }
                return _Semester;
            }
            set
            {
                _Semester = value;
                if (value == null)
                {
                    _SemesterID = -1;
                }
                else
                {
                    _SemesterID = value.ID;
                }
            }
        }

        /// <summary>
        /// Gets or sets the User for this object.
        /// Reference: Taken code from prof. Holmes Peerval Project
        /// </summary>
        /// <remarks></remarks>
        [XmlIgnore]
        public Section Section
        {
            get
            {
                if (_Section == null)
                {
                    _Section = DAL.GetSection(_SectionID);
                }
                return _Section;
            }
            set
            {
                _Section = value;
                if (value == null)
                {
                    _SectionID = -1;
                }
                else
                {
                    _SectionID = value.ID;
                }
            }
        }

      


        #endregion

        #region Database String
        internal const string db_ID = "CourseSemesterID";
        internal const string db_CRN = "CRN";
        internal const string db_CourseID = "CourseID";
        internal const string db_SemesterID = "SemesterID";
        internal const string db_YearID = "YearID";
        internal const string db_SectionID = "SectionID";
        internal const string db_UserID = "UserID";
        internal const string db_DateStart = "DateStart";
        internal const string db_DateEnd = "DateEnd";

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
            _CRN = dr.GetInt32(db_CRN);
            _CourseID = dr.GetInt32(db_CourseID);
            _SemesterID = dr.GetInt32(db_SemesterID);
            _YearID = dr.GetInt32(db_YearID);
            _SectionID = dr.GetInt32(db_SectionID);
            //_DateStart = dr.GetDateTime(db_DateStart);
            //_DateEnd = dr.GetDateTime(db_DateEnd);
        }
        #endregion


        public override string ToString()
        {
            return this.GetType().ToString();
        }
    }
}
