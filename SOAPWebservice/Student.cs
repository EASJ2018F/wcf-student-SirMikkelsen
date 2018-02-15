using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Web;

namespace SOAPWebservice
{
    [DataContract]
    public class Student
    {
        private int _id;
        private string _name;
        private string _className;
        private int _room;
    


        [DataMember]
        public string Name
        {
            get { return _name;  }
            set { _name = value; }
        }

        [DataMember]
        public string ClassName
        {
            get {return _className;}
            set { _className = value; }
        }


        public Student(string name, string className, int room, int id)
        {
            _name = name;
            _className = className;
            Room = room;
            _id = id;
        }
        [DataMember]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        [DataMember]
        public int Room
        {
            get { return _room; }
            set { _room = value; }
        }
    }
}