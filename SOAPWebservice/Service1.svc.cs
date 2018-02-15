using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SOAPWebservice
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        private static List<Student> _liste = new List<Student>();

        // Tilføjer connection-string til database på Azure
        private const string _connectionString = "Server=tcp:mineserver.database.windows.net,1433;Initial Catalog=mydatabase;Persist Security Info=False;User ID=easj;Password=Spiderman87;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public Student AddStudent(string name, string classRoom, int rooom, int id)
        {
            Student s1 = new Student(name, classRoom, rooom, id);
            _liste.Add(s1);
            return s1;
        }

        public Student FindStudent(string name)
        {
             return  _liste.Single(s => s.Name == name);
        }

        public List<Student> GetAllStudents(string name)
        {
            return _liste;
        }

        public void RemoveStudent(string name)
        {
         
            _liste.Remove(this.FindStudent(name));
        }

        public void EditStudent(string name, string className, int rooom, string newName, int id)
        {
            var student = this.FindStudent(name);
            student.Name = newName;
            student.ClassName = className;
            student.Room = rooom;
            student.Id = id;
        }


        public IList<Student> GetAllStudentsDB()
        {
            const string selectAllStudents = "select * from student order by name";

            using (SqlConnection databaseConnection = new SqlConnection(_connectionString))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectAllStudents, databaseConnection))
                {
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        List<Student> studentList = new List<Student>();
                        while (reader.Read())
                        {
                            Student student = ReadStudent(reader);
                            studentList.Add(student);
                        }
                        return studentList;
                    }
                }
            }
        }

        private static Student ReadStudent(IDataRecord reader)
        {
            int id = reader.GetInt32(0);
            string name = reader.GetString(1);
            string className = reader.GetString(2);
            int room = reader.GetInt32(3);
            Student student = new Student(name, className, room, id);

            return student;
        }


        public Student GetStudentByIdDB(int id)
        {
            const string selectStudent = "select * from student where id=@id";
            using (SqlConnection databaseConnection = new SqlConnection(_connectionString))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectStudent, databaseConnection))
                {
                    selectCommand.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            return null;
                        }
                        reader.Read(); // Advance cursor to first row
                        Student student = ReadStudent(reader);
                        return student;
                    }
                }
            }
        }

        public IList<Student> GetStudentsByNameDB(string name)
        {
            string selectStr = "select * from student where name LIKE @name";
            using (SqlConnection databaseConnection = new SqlConnection(_connectionString))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectStr, databaseConnection))
                {
                    selectCommand.Parameters.AddWithValue("@name", "%" + name + "%");
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        IList<Student> studentList = new List<Student>();
                        while (reader.Read())
                        {
                            Student st = ReadStudent(reader);
                            studentList.Add(st);
                        }
                        return studentList;
                    }
                }
            }
        }

        public int AddStudentDB(string name, string className, int room, int id)
        {
            const string insertStudent = "insert into student (name, className, room, id) values (@name, @className, @room, @id)";
            using (SqlConnection databaseConnection = new SqlConnection(_connectionString))
            {
                databaseConnection.Open();
                using (SqlCommand insertCommand = new SqlCommand(insertStudent, databaseConnection))
                {
                    insertCommand.Parameters.AddWithValue("@name", name);
                    insertCommand.Parameters.AddWithValue("@className", className);
                    insertCommand.Parameters.AddWithValue("@room", room);
                    insertCommand.Parameters.AddWithValue("@id", id);
                    int rowsAffected = insertCommand.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
        }

        public int DeletetudentDB(int id)
        {
            
            const string deleteStudent = "DELETE FROM student WHERE id=@id";
            using (SqlConnection databaseConnection = new SqlConnection(_connectionString))
            {
                databaseConnection.Open();
                using (SqlCommand deleteCommand = new SqlCommand(deleteStudent, databaseConnection))
                {
                    deleteCommand.Parameters.AddWithValue("@id", id);
                    int rowsAffected = deleteCommand.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
        }

        public int UpdateStudentDB(string name, string className, int room, int id)
        {
            const string insertStudent = "UPDATE Student SET name = @name, className = @className, room = @room Where id = @id";
            using (SqlConnection databaseConnection = new SqlConnection(_connectionString))
            {
                databaseConnection.Open();
                using (SqlCommand insertCommand = new SqlCommand(insertStudent, databaseConnection))
                {
                    insertCommand.Parameters.AddWithValue("@name", name);
                    insertCommand.Parameters.AddWithValue("@className", className);
                    insertCommand.Parameters.AddWithValue("@room", room);
                    insertCommand.Parameters.AddWithValue("@id", id);
                    int rowsAffected = insertCommand.ExecuteNonQuery();
                    return rowsAffected;
                }
                
            }
        }
    }
}
