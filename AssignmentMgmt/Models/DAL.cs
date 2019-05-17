using System;
using System.Collections.Generic;
using System.Net;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using AssignmentMgmt.Models;
using System.Data.SqlClient;
using System.Data;


namespace AssignmentMgmt.Model
{
    public class DAL
    {
        /// <summary>
        /// created by: Ganesh Sapkota
        /// DAL for Classweb project. 
        /// </summary



        //Database information for the hosting website db
        private static string ReadOnlyConnectionString = "Server=MYSQL7003.site4now.net;Database=db_a458d6_shreelv;Uid=a458d6_shreelv;Pwd=x129y190;";
        private static string EditOnlyConnectionString = "Server=MYSQL7003.site4now.net;Database=db_a458d6_shreelv;Uid=a458d6_shreelv;Pwd=x129y190;";



        public static string _Pepper = "gLj23Epo084ioAnRfgoaHyskjasf"; //HACK: set here for now, will move elsewhere later.
        public static int _Stretches = 10000;

        private DAL()
        {
        }
        internal enum dbAction
        {
            Read,
            Edit
        }
        #region Database Connections
        internal static void ConnectToDatabase(MySqlCommand comm, dbAction action = dbAction.Read)
        {
            try
            {
                if (action == dbAction.Edit)
                    comm.Connection = new MySqlConnection(EditOnlyConnectionString);
                else
                    comm.Connection = new MySqlConnection(ReadOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
            }
            catch (Exception)
            {
            }
        }

       
        public static MySqlDataReader GetDataReader(MySqlCommand comm)
        {
            try
            {
                ConnectToDatabase(comm);
                comm.Connection.Open();
                return comm.ExecuteReader();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                return null;
            }
        }

       

        public static int GetIntReader(MySqlCommand comm)
        {
            try
            {
                ConnectToDatabase(comm);
                comm.Connection.Open();
                int count = Convert.ToInt32(comm.ExecuteScalar());
                return count;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                return 0;
            }
        }


        /// <summary>
        /// reference: Proffesor's PeerEval Project. 
        /// </summary>
        /// <param name="comm"></param>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        internal static int AddObject(MySqlCommand comm, string parameterName)
         {
            int retInt = 0;
            try
            {
                comm.Connection = new MySqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();
                MySqlParameter retParameter;
                retParameter = comm.Parameters.Add(parameterName, MySqlDbType.Int32);
                retParameter.Direction = System.Data.ParameterDirection.Output;
                comm.ExecuteNonQuery();
                retInt = (int)retParameter.Value;
                comm.Connection.Close();
            }
            catch (Exception ex)
            {
                if (comm.Connection != null)
                    comm.Connection.Close();

                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            return retInt;
        }


        /// <summary>
        /// reference: Professor's DAL for PeerEval
        /// set connection and execute given command on the database
        /// </summary>
        /// <param name="comm">MySqlCommand to execute.</param>
        /// <returns>This will return the number of rows affected after execution. -1 on failure and positive number on success. </returns>
        internal static int UpdateObject(MySqlCommand comm)
        {
            int retInt = 0;
            try
            {
                comm.Connection = new MySqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();
                retInt = comm.ExecuteNonQuery();
                comm.Connection.Close();
            }
            catch (Exception ex)
            {
                if (comm.Connection != null)
                    comm.Connection.Close();

                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            return retInt;
        }

        #endregion


        #region Login
        ///<summary>
        /// Gets the User from the database corresponding to the Username
        /// Reference: Github, PeerEval Project
        /// </summary>
        /// <remarks></remarks>
        public static User GetUser(string userName, string password)
        {

            MySqlCommand comm = new MySqlCommand("sproc_GetUserByUserName");
            User retObj = null;
            try
            {
                comm.Parameters.AddWithValue("@" + User.db_UserName, userName);
                MySqlDataReader dr = GetDataReader(comm);
                while (dr.Read())
                {
                    retObj = new User(dr);
                }
                comm.Connection.Close();
            }
            catch (Exception ex)
            {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            //Verify password matches.
            if (retObj != null)
            {
                if (!Tools.Hasher.IsValid(password, retObj.Salt, _Pepper, _Stretches, retObj.Password))
                {
                    retObj = null;
                }
            }

            return retObj;

        }
        #endregion

        #region Section
        ///Created on: 04/01/2019
        ///Created by: Meshari
        ///CRUD methods for Section object in ClassWeb Database
        ///Reference: Prof. Holmes PeerVal Project
        ///Copied code for Roles CRUD and modified to use for the section
        /// <summary>
        /// Gets a list of all Sections objects from the database.
        /// </summary>
        /// <remarks></remarks>
        public static List<Section> GetSections()
        {
            MySqlCommand comm = new MySqlCommand("sproc_SectionsGetAll");
            List<Section> retList = new List<Section>();
            try
            {
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader dr = GetDataReader(comm);
                while (dr.Read())
                {
                    retList.Add(new Section(dr));
                }
                comm.Connection.Close();
            }
            catch (Exception ex)
            {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return retList;
        }

        /// <summary>
        /// Gets the Classweb.Section corresponding with the given ID
        /// </summary>
        /// <remarks></remarks>

        public static Section GetSection(int id)
        {
            MySqlCommand comm = new MySqlCommand("sproc_SectionGet");
            Section retObj = null;
            try
            {
                comm.Parameters.AddWithValue("@" + Section.db_ID, id);
                MySqlDataReader dr = GetDataReader(comm);
                while (dr.Read())
                {
                    retObj = new Section(dr);
                }
                comm.Connection.Close();
            }
            catch (Exception ex)
            {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return retObj;
        }



        /// <summary>
        /// Attempts to add a database entry corresponding to the given Section
        /// </summary>
        /// <remarks></remarks>

        internal static int AddSection(Section obj)
        {

            if (obj == null) return -1;
            MySqlCommand comm = new MySqlCommand("sproc_SectionAdd");
            try
            {
                comm.Parameters.AddWithValue("@" + Section.db_Number, obj.SectionNumber);
                return AddObject(comm, "@" + Section.db_ID);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }


        /// <summary>
        /// Attempts to the database entry corresponding to the given Section
        /// </summary>
        /// <remarks></remarks>

        internal static int UpdateSection(Section obj)
        {
            if (obj == null) return -1;
            MySqlCommand comm = new MySqlCommand("sproc_SectionUpdate");
            try
            {
                comm.Parameters.AddWithValue("@" + Section.db_ID, obj.ID);
                comm.Parameters.AddWithValue("@" + Section.db_Number, obj.SectionNumber);
                return UpdateObject(comm);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }


        /// <summary>
        /// Attempts to delete the database entry corresponding to the Section
        /// </summary>
        /// <remarks></remarks
        internal static int RemoveSection(Section obj)
        {
            if (obj == null) return -1;
            MySqlCommand comm = new MySqlCommand();
            try
            {
                comm.CommandText = "sproc_SectionRemove";
                comm.Parameters.AddWithValue("@" + Section.db_ID, obj.ID);
                return UpdateObject(comm);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }
        /// <summary>
        /// Attempts to delete the database entry corresponding to the Section
        /// </summary>
        /// <remarks></remarks>
        internal static int RemoveSection(int sectionID)
        {
            if (sectionID == 0) return -1;
            MySqlCommand comm = new MySqlCommand();
            try
            {
                comm.CommandText = "sproc_SectionRemoveByID";
                comm.Parameters.AddWithValue("@" + Section.db_ID, sectionID);
                return UpdateObject(comm);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }

       
        #endregion

        #region Courses
        /// <summary>
        /// Attempts to delete the database entry corresponding to the Section
        /// </summary>
        /// <remarks></remarks>
        internal static Course GetCourse(int courseID)
        {
            MySqlCommand comm = new MySqlCommand("sproc_GetCourse");
            Course retObj = null;
            try
            {
                comm.Parameters.AddWithValue("@" + Course.db_ID, courseID);
                MySqlDataReader dr = GetDataReader(comm);
                while (dr.Read())
                {
                    retObj = new Course(dr);
                }
                comm.Connection.Close();
            }
            catch (Exception ex)
            {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return retObj;
        }
        /// <summary>
        /// Attempts to add a database entry corresponding to the given Course
        /// </summary>
        /// <remarks></remarks>

        internal static int AddCourse(Course obj)
        {
            if (obj == null) return -1;
            MySqlCommand comm = new MySqlCommand("sproc_CourseAdd");
            try
            {
                comm.Parameters.AddWithValue("@" + Course.db_Title, obj.Title);
                comm.Parameters.AddWithValue("@" + Course.db_Name, obj.Name);
                comm.Parameters.AddWithValue("@" + Course.db_Description, obj.Description);

                return AddObject(comm, "@" + Course.db_ID);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }

        

        /// <summary>
        /// Gets a list of all Course object from the database.
        /// </summary>
        /// <remarks></remarks>
        public static List<Course> GetCourses()
        {
            MySqlCommand comm = new MySqlCommand("sproc_GetAllCourses");
            List<Course> retList = new List<Course>();
            try
            {
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader dr = GetDataReader(comm);
                while (dr.Read())
                {
                    retList.Add(new Course(dr));
                }
                comm.Connection.Close();
            }
            catch (Exception ex)
            {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return retList;
        }

        /// <summary>
        /// Attempts to delete the database entry corresponding to the Section
        /// </summary>
        /// <remarks></remarks>
        internal static int RemoveCourse(int courseID)
        {
            if (courseID == 0) return -1;
            MySqlCommand comm = new MySqlCommand();
            try
            {
                comm.CommandText = "sproc_CourseRemove";
                comm.Parameters.AddWithValue("@" + Course.db_ID, courseID);
                return UpdateObject(comm);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }

        #endregion
       
        #region Semester
        /// <summary>
        /// Gets the Classweb.Semester corresponding with the given ID
        /// </summary>
        /// <remarks></remarks>

        public static Semester GetSemester(int id)
        {
            MySqlCommand comm = new MySqlCommand("sproc_SemesterGet");
            Semester retObj = null;
            try
            {
                comm.Parameters.AddWithValue("@" + Semester.db_ID, id);
                MySqlDataReader dr = GetDataReader(comm);
                while (dr.Read())
                {
                    retObj = new Semester(dr);
                }
                comm.Connection.Close();
            }
            catch (Exception ex)
            {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return retObj;
        }

        /// <summary>
        /// Gets a list of all semester objects from the database.
        /// </summary>
        /// <remarks></remarks>
        public static List<Semester> GetSemesters()
        {
            MySqlCommand comm = new MySqlCommand("sproc_SemesterGetAll");
            List<Semester> retList = new List<Semester>();
            try
            {
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader dr = GetDataReader(comm);
                while (dr.Read())
                {
                    retList.Add(new Semester(dr));
                }
                comm.Connection.Close();
            }
            catch (Exception ex)
            {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return retList;
        }

        /// <summary>
        /// Attempts to add a database entry corresponding to the given Semester
        /// </summary>
        /// <remarks></remarks>

        internal static int AddSemester(Semester obj)
        {
            if (obj == null) return -1;
            MySqlCommand comm = new MySqlCommand("sproc_SemesterAdd");
            try
            {
                comm.Parameters.AddWithValue("@" + Semester.db_ID, obj.ID);
                comm.Parameters.AddWithValue("@" + Semester.db_Name, obj.Name);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }

        /// <summary>
        /// Attempts to edit the database entry corresponding to the given Semester
        /// </summary>
        /// <remarks></remarks>

        internal static int UpdateSemester(Semester obj)
        {
            if (obj == null) return -1;
            MySqlCommand comm = new MySqlCommand("sproc_SemesterEdit");
            try
            {
                comm.Parameters.AddWithValue("@" + Semester.db_ID, obj.ID);
                comm.Parameters.AddWithValue("@" + Semester.db_Name, obj.Name);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }

        /// <summary>
        /// Attempts to delete the database entry corresponding to the Semester
        /// </summary>
        /// <remarks></remarks>
        internal static int RemoveSemester(int semesterID)
        {
            if (semesterID == 0) return -1;
            MySqlCommand comm = new MySqlCommand();
            try
            {
                comm.CommandText = "sproc_SemesterRemove";
                comm.Parameters.AddWithValue("@" + Semester.db_ID, semesterID);
                return UpdateObject(comm);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }

        #endregion

        #region Year
        /// <summary>
        /// Gets the Classweb.Semester corresponding with the given ID
        /// </summary>
        /// <remarks></remarks>

        public static Year GetYear(int id)
        {
            MySqlCommand comm = new MySqlCommand("sproc_YearGet");
            Year retObj = null;
            try
            {
                comm.Parameters.AddWithValue("@" + Year.db_ID, id);
                MySqlDataReader dr = GetDataReader(comm);
                while (dr.Read())
                {
                    retObj = new Year(dr);
                }
                comm.Connection.Close();
            }
            catch (Exception ex)
            {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return retObj;
        }

        /// <summary>
        /// Gets a list of all semester objects from the database.
        /// </summary>
        /// <remarks></remarks>
        public static List<Year> GetYears()
        {
            MySqlCommand comm = new MySqlCommand("sproc_YearGetAll");
            List<Year> retList = new List<Year>();
            try
            {
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader dr = GetDataReader(comm);
                while (dr.Read())
                {
                    retList.Add(new Year(dr));
                }
                comm.Connection.Close();
            }
            catch (Exception ex)
            {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return retList;
        }

        /// <summary>
        /// Attempts to add a database entry corresponding to the given Year
        /// </summary>
        /// <remarks></remarks>

        internal static int AddYear(Year obj)
        {
            if (obj == null) return -1;
            MySqlCommand comm = new MySqlCommand("sproc_YearAdd");
            try
            {
                comm.Parameters.AddWithValue("@" + Year.db_ID, obj.ID);
                comm.Parameters.AddWithValue("@" + Year.db_Year, obj.Year1);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }

        /// <summary>
        /// Attempts to edit the database entry corresponding to the given Semester
        /// </summary>
        /// <remarks></remarks>

        internal static int UpdateYear(Year obj)
        {
            if (obj == null) return -1;
            MySqlCommand comm = new MySqlCommand("sproc_YearEdit");
            try
            {
                comm.Parameters.AddWithValue("@" + Year.db_ID, obj.ID);
                comm.Parameters.AddWithValue("@" + Year.db_Year, obj.Year1);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }

        /// <summary>
        /// Attempts to delete the database entry corresponding to the Year
        /// </summary>
        /// <remarks></remarks>
        internal static int RemoveYear(int YearID)
        {
            if (YearID == 0) return -1;
            MySqlCommand comm = new MySqlCommand();
            try
            {
                comm.CommandText = "sproc_YearRemove";
                comm.Parameters.AddWithValue("@" + Year.db_ID, YearID);
                return UpdateObject(comm);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }

        #endregion

        #region Role
        /// <summary>
        /// Get list of all Role CLassweb.objects from the database
        /// Reference: Taken code from the peerval project
        /// </summary>
        /// <returns></returns>
        public static List<Role> GetRoles()
        {
            MySqlCommand comm = new MySqlCommand("sproc_RolesGetAll");
            List<Role> retList = new List<Role>();
            try
            {
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader dr = GetDataReader(comm);
                while (dr.Read())
                {
                    retList.Add(new Role(dr));
                }
                comm.Connection.Close();
            }
            catch (Exception ex)
            {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return retList;
        }

        // <summary>
        /// Attempts to add a database entry corresponding to the given Role
        /// Reference: Taken code from the peerval project
        /// </summary>
        internal static int AddRole(Role obj)
        {
            if (obj == null) return -1;
            MySqlCommand comm = new MySqlCommand("sproc_RoleAdd");
            try
            {
                comm.Parameters.AddWithValue("@" + Role.db_Name, obj.Name);
                comm.Parameters.AddWithValue("@" + Role.db_IsAdmin, obj.IsAdmin);
                comm.Parameters.AddWithValue("@" + Role.db_Users, obj.Users.DAVESet);
                comm.Parameters.AddWithValue("@" + Role.db_Role, obj.Roles.DAVESet);
                comm.Parameters.AddWithValue("@" + Role.db_Assignment, obj.Assignment.DAVESet);
                return AddObject(comm, "@" + Role.db_ID);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }

        /// <summary>
        /// Gets the Classweb.Role corresponding with the given ID
        /// Reference: Code taken from the peerval project
        /// </summary>
        /// <remarks></remarks>

        public static Role GetRole(int id)
        {
            return Roles.Get(id);
        }

        /// <summary>
        /// Attempts to the database entry corresponding to the given Role
        /// Reference: Taken code from the peerval project
        /// </summary>
        internal static int UpdateRole(Role obj)
        {
            if (obj == null) return -1;
            MySqlCommand comm = new MySqlCommand("sproc_RoleUpdate");
            try
            {
                comm.Parameters.AddWithValue("@" + Role.db_ID, obj.ID);
                comm.Parameters.AddWithValue("@" + Role.db_Name, obj.Name);
                comm.Parameters.AddWithValue("@" + Role.db_IsAdmin, obj.IsAdmin);
                comm.Parameters.AddWithValue("@" + Role.db_Users, obj.Users.DAVESet);
                comm.Parameters.AddWithValue("@" + Role.db_Role, obj.Roles.DAVESet);
                comm.Parameters.AddWithValue("@" + Role.db_Assignment, obj.Assignment.DAVESet);
                return UpdateObject(comm);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }

        internal static int RemoveRole(Role role)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Attempts to delete the database entry corresponding to the Role
        /// </summary>
        /// <remarks></remarks>
        internal static int RemoveRole(int roleID)
        {
            if (roleID == 0) return -1;
            MySqlCommand comm = new MySqlCommand();
            try
            {
                comm.CommandText = "sproc_RoleRemove";
                comm.Parameters.AddWithValue("@" + Role.db_ID, roleID);
                return UpdateObject(comm);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }


        #endregion



    }
}



        