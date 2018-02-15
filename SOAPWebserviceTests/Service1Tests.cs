using Microsoft.VisualStudio.TestTools.UnitTesting;
using SOAPWebservice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace SOAPWebservice.Tests
{
    [TestClass()]
    public class Service1Tests
    {
        [TestMethod()]
        public void AddStudentTest()
        {
            // Arrange
            Service1 s1Client = new Service1();

            // Act
            var s1 = s1Client.AddStudent("Bruce wayne", "13b", 7, 4);

            Assert.AreEqual("Bruce wayne", s1.Name);

        }

        [TestMethod()]
        public void FindStudentTest()
        {
            // Arrange
            Service1 s1Client = new Service1();

            // Act
            var s1 = s1Client.AddStudent("Naruto", "13b", 7, 4);

            var s2 = s1Client.FindStudent("Naruto");

            Assert.AreEqual(s1.ClassName, s2.ClassName);
            Assert.AreEqual(s1.Room, s2.Room);


        }

    }

}