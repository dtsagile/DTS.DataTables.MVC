DTS.DataTables.MVC
==================
DTS.DataTables.MVC is a simple model binder for DataTables v1.10.4+. 
<h2>Usage: Controller</h2>

<pre>
    public class ProfilesController : Controller
    {
        private IProfileRepository _profileRepo;
        public ProfilesController(IProfileRepository profileRepo)
        {
            this._profileRepo = profileRepo;
        }

        [RequireHttps]
        public ActionResult Index()
        {
            ViewBag.Columns = GetColumns();
            return View("Index");
        }

        private string GetColumns()
        {
            return JsonConvert.SerializeObject(new List<ColumnHeader>() { 
                new ColumnHeader() { data = "Id", name = "", visible = true, sortable = false, width = "50px", className = "text-center"  },
                new ColumnHeader() { data = "DirectoryName", name = "Name", visible = true, sortable = true },
                new ColumnHeader() { data = "Email", name = "Email", visible = true, sortable = true },
                new ColumnHeader() { data = "Roles", name = "Roles", visible = true, sortable = true },
                new ColumnHeader() { data = "IsActive", name = "Active", visible = true, sortable = true }
            });
        }
        
        [RequireHttps]
        [HttpPost]
        public JsonResult GetData([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            // get all profiles
            var profiles = _profileRepo.All;

            // create another collection of user, will use this one to filter our results
            var data = profiles;

            // search value string for convenience, so we don't have to navigate the object model each time
            var searchValue = "";

            // filter data if searching across all columns
            if (!string.IsNullOrWhiteSpace(requestModel.Search.Value))
            {
                searchValue = requestModel.Search.Value.ToLower();
                data = data.Where(x =>
                  x.DirectoryName.ToLower().Contains(searchValue) ||
                  x.Email.ToLower().Contains(searchValue) ||
                  x.Phone.ToLower().Contains(searchValue));
            }

            // now filter data for each column if there's a searchValue for the columns, if supported in datatables ui
            searchValue = GetColSearchValue(requestModel, "DirectoryName");
            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                data = data.Where(x => x.DirectoryName.ToLower() == searchValue);
            }

            searchValue = GetColSearchValue(requestModel, "Email");
            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                data = data.Where(x => x.Email.ToLower() == searchValue);
            }

            searchValue = GetColSearchValue(requestModel, "Phone");
            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                data = data.Where(x => x.Phone.ToLower() == searchValue);
            }

            // sort data
            var sortColumns = string.Join(", ",
              requestModel.Columns.GetSortedColumns()
              .Select(x =>
                  string.Format("{0} {1}", x.Data, x.SortDirection.ToString().ToLower().Contains("asc") ? "ASC" : "DESC")
              ).ToArray());

            // page data
            var pagedData = data.OrderBy(sortColumns).Skip(requestModel.Start).Take(requestModel.Length);

            // lastly return a json result
            var result = new DataTablesResponse(requestModel.Draw, pagedData, data.Count(), profiles.ToList().Count());
            return Json(result);
        }

        private string GetColSearchValue(IDataTablesRequest requestModel, string colName)
        {
            if (requestModel.Columns.First(x => x.Name == colName).Search != null)
            {
                return requestModel.Columns.First(x => x.Name == colName).Search.Value.ToLower();
            }
            return string.Empty;
        }
    }
</pre>
