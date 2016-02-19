using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTS.DataTables.MVC;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DTS.DataTables.MVC.Tests.Models;

namespace DTS.DataTables.MVC.Tests
{
    [TestClass]
    public class DataTablesAttributeTests
    {
        [TestMethod]
        public void GetColumnHeadersTest()
        {
            // act 
            var list = DataTablesHelpers.GetColumnsHeaders<Person>();

            // assert name 
            Assert.AreEqual(list.First(x => x.data == "Id").name, "Id");
            Assert.AreEqual(list.First(x => x.data == "FirstName").name, "First Name");
            Assert.AreEqual(list.First(x => x.data == "LastName").name, "Last Name");
            Assert.AreEqual(list.First(x => x.data == "Email").name, "Email");
            Assert.AreEqual(list.First(x => x.data == "Phone").name, "Phone");
            Assert.AreEqual(list.First(x => x.data == "Age").name, "Age");

            // assert title 
            Assert.AreEqual(list.First(x => x.data == "Id").title, "Row Id");
            Assert.AreEqual(list.First(x => x.data == "FirstName").title, "First Name");
            Assert.AreEqual(list.First(x => x.data == "LastName").title, "Last Name");
            Assert.AreEqual(list.First(x => x.data == "Email").title, "Email");
            Assert.AreEqual(list.First(x => x.data == "Phone").title, "Phone");
            Assert.AreEqual(list.First(x => x.data == "Age").title, "Age");

            // assert visible
            Assert.AreEqual(list.First(x => x.data == "Id").visible, false);
            Assert.AreEqual(list.First(x => x.data == "FirstName").visible, true);
            Assert.AreEqual(list.First(x => x.data == "LastName").visible, true);
            Assert.AreEqual(list.First(x => x.data == "Email").visible, true);
            Assert.AreEqual(list.First(x => x.data == "Phone").visible, true);
            Assert.AreEqual(list.First(x => x.data == "Age").visible, true);

            // assert orderable
            Assert.AreEqual(list.First(x => x.data == "Id").orderable, false);
            Assert.AreEqual(list.First(x => x.data == "FirstName").orderable, true);
            Assert.AreEqual(list.First(x => x.data == "LastName").orderable, true);
            Assert.AreEqual(list.First(x => x.data == "Email").orderable, true);
            Assert.AreEqual(list.First(x => x.data == "Phone").orderable, false);
            Assert.AreEqual(list.First(x => x.data == "Age").orderable, true);

            // assert orderdata
            Assert.AreEqual(list.First(x => x.data == "Id").orderData, null);
            Assert.AreEqual(list.First(x => x.data == "FirstName").orderData.First(), 2);
            Assert.AreEqual(list.First(x => x.data == "FirstName").orderData.Skip(1).First(), 1);
            Assert.AreEqual(list.First(x => x.data == "LastName").orderData.First(), 2);
            Assert.AreEqual(list.First(x => x.data == "LastName").orderData.Skip(1).First(), 1);
            Assert.AreEqual(list.First(x => x.data == "Email").orderData, null);
            Assert.AreEqual(list.First(x => x.data == "Phone").orderData, null);
            Assert.AreEqual(list.First(x => x.data == "Age").orderData, null);

            // assert orderSequece
            Assert.AreEqual(list.First(x => x.data == "Id").orderSequence, null);
            Assert.AreEqual(list.First(x => x.data == "FirstName").orderSequence, null);
            Assert.AreEqual(list.First(x => x.data == "LastName").orderSequence, null);
            Assert.AreEqual(list.First(x => x.data == "Email").orderSequence.First(), "asc");
            Assert.AreEqual(list.First(x => x.data == "Email").orderSequence.Skip(1).First(), "asc");
            Assert.AreEqual(list.First(x => x.data == "Email").orderSequence.Skip(2).First(), "desc");
            Assert.AreEqual(list.First(x => x.data == "Phone").orderSequence, null);
            Assert.AreEqual(list.First(x => x.data == "Age").orderSequence, null);

            // assert width
            Assert.AreEqual(list.First(x => x.data == "Id").width, "");
            Assert.AreEqual(list.First(x => x.data == "FirstName").width, "");
            Assert.AreEqual(list.First(x => x.data == "LastName").width, "");
            Assert.AreEqual(list.First(x => x.data == "Email").width, "150px");
            Assert.AreEqual(list.First(x => x.data == "Phone").width, "");
            Assert.AreEqual(list.First(x => x.data == "Age").width, "");

            // assert type
            Assert.AreEqual(list.First(x => x.data == "Id").type, "string");
            Assert.AreEqual(list.First(x => x.data == "FirstName").type, "string");
            Assert.AreEqual(list.First(x => x.data == "LastName").type, "string");
            Assert.AreEqual(list.First(x => x.data == "Email").type, "string");
            Assert.AreEqual(list.First(x => x.data == "Phone").type, "string");
            Assert.AreEqual(list.First(x => x.data == "Age").type, "num");

            // assert contentPadding
            Assert.AreEqual(list.First(x => x.data == "Id").contentPadding, "mmm");
            Assert.AreEqual(list.First(x => x.data == "FirstName").contentPadding, "");
            Assert.AreEqual(list.First(x => x.data == "LastName").contentPadding, "");
            Assert.AreEqual(list.First(x => x.data == "Email").contentPadding, "");
            Assert.AreEqual(list.First(x => x.data == "Phone").contentPadding, "");
            Assert.AreEqual(list.First(x => x.data == "Age").contentPadding, "");

            // assert defaultContent
            Assert.AreEqual(list.First(x => x.data == "Id").defaultContent, "");
            Assert.AreEqual(list.First(x => x.data == "FirstName").defaultContent, "");
            Assert.AreEqual(list.First(x => x.data == "LastName").defaultContent, "");
            Assert.AreEqual(list.First(x => x.data == "Email").defaultContent, "");
            Assert.AreEqual(list.First(x => x.data == "Phone").defaultContent, "");
            Assert.AreEqual(list.First(x => x.data == "Age").defaultContent, "55");

            // assert className
            Assert.AreEqual(list.First(x => x.data == "Id").className, "");
            Assert.AreEqual(list.First(x => x.data == "FirstName").className, "");
            Assert.AreEqual(list.First(x => x.data == "LastName").className, "");
            Assert.AreEqual(list.First(x => x.data == "Email").className, "");
            Assert.AreEqual(list.First(x => x.data == "Phone").className, "span-4");
            Assert.AreEqual(list.First(x => x.data == "Age").className, "");
        }
    }
}
