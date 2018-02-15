using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SOAPWebservice
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

       [OperationContract]
       Student AddStudent(string name, string classRoom, int rooom, int id);

        [OperationContract]

        Student FindStudent(string name);

        [OperationContract]

        List<Student> GetAllStudents(string name);

        [OperationContract]

        void RemoveStudent(string name);

        [OperationContract]

        void EditStudent(string name, string className, int rooom, string newName, int id);

        [OperationContract]

        IList<Student> GetAllStudentsDB();


        [OperationContract]

        Student GetStudentByIdDB(int id);

        [OperationContract]

        IList<Student> GetStudentsByNameDB(string name);

        [OperationContract]

        int AddStudentDB(string name, string className, int room, int id);


        [OperationContract]
        int DeletetudentDB(int id);

        [OperationContract]
        int UpdateStudentDB(string name, string className, int room, int id);


    }

    
}
