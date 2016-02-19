using DTS.DataTables.MVC.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using DTS.DataTables.MVC.Tests.Helpers;

namespace DTS.DataTables.MVC.Tests
{
    [TestClass]
    public class OrderByTests
    {
        [TestMethod]
        public void IEnumerableOrderBy()
        {
            // arrange 
            var data = Mocks.People.AsEnumerable();

            // act
            var result = data.OrderBy("LastName DESC");

            // assert
            Assert.AreEqual("Young", result.First().LastName);
        }

        [TestMethod]
        public void IQueryableOrderBy()
        {
            // arrange 
            var data = Mocks.People.AsQueryable();

            // act
            var result = data.OrderBy("LastName DESC");

            // assert
            Assert.AreEqual("Young", result.First().LastName);
        }

        [TestMethod]
        public void IEnumerableOrderByMany()
        {
            // arrange 
            var data = Mocks.People.Where(x => x.LastName == "Ford");

            // act
            var result = data.OrderBy("LastName ASC, FirstName ASC");

            // assert
            Assert.AreEqual("Gilda", result.First().FirstName);
        }

        [TestMethod]
        public void IQueryableOrderByMany()
        {
            // arrange 
            var data = Mocks.People.Where(x => x.LastName == "Ford").AsQueryable();

            // act
            var result = data.OrderBy("LastName DESC, FirstName DESC");

            // assert
            Assert.AreEqual("Kirkland", result.First().FirstName);
        }

        [TestMethod]
        public void OrderByEmptyString()
        {
            // arrange 
            var data = Mocks.People;

            // act
            var result = data.OrderBy(string.Empty);

            // assert
            Assert.AreEqual(1, result.First().Id);
        }
    
    }
}
