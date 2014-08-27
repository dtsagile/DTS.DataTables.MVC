using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTS.DataTables.MVC;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DTS.DataTables.MVC.Tests
{
    [TestClass]
    public class DataTablesAttributeTests
    {
        [TestMethod]
        public void GetColumnHeadersTest()
        {
            var list = DataTablesHelpers.GetColumnsHeaders<Person>();
            Assert.AreEqual("Foo", "Bar");
        }
    }


    public class Person
    {
        //[DataTablesColumnAttribute(Name = "First Name")]
        public string FirstName { get; set; }
        
        //[DataTablesColumnAttribute(Name = "Last Name", Width = "150px")]
        public string LastName { get; set; }
        
        //[DataTablesColumnAttribute(Name = "Email", Sortable = false)]
        public string Email { get; set; }

        //[DataTablesColumnAttribute(Name = "Phone")]
        public string Phone { get; set; }

        //[DataTablesColumnAttribute(Visible = false)]
        public int Age { get; set; }
    }

}
