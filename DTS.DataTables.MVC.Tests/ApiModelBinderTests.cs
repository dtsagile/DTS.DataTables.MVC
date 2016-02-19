using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTS.DataTables.MVC.Tests
{

    [TestClass]
    public class ApiModelBinderTests
    {
        private DataTablesRequest _postModel;

        public ApiModelBinderTests()
        {
            this._postModel = GetPostModel();
        }

        private DataTablesRequest GetPostModel()
        {
            // build some columns from the person model
            var columns = new List<Column>()
            {
                new Column("Id", "", false, false, "", false),
                new Column("FirstName", "First Name", true, true, "", false),
                new Column("LastName", "Last Name", true, true, "", false),
                new Column("Email", "", true, true, "", false),
                new Column("Phone", "", true, true, "", false),
                new Column("Age", "", true, true, "", false)
            }.AsEnumerable();

            var model = new DataTablesRequest();
            model.SetColumns(columns.AsEnumerable());
            model.Draw = 1;
            model.Start = 0;
            model.Length = 5;
            model.Search = new Search("", false);
            model.Columns.GetSortedColumns();

            return model;
        }

        [TestMethod]
        public void BindModel()
        {
            //var foo = new DataTablesApiModelBinder();
            //foo.BindModel(
        }

    }
}
