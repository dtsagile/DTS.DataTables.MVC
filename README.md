DTS.DataTables.MVC
==================
DTS.DataTables.MVC is a simple model binder for DataTables v1.10.4+. 
<h2>Usage: Controller</h2>

<pre>
public class UsersController : Controller
{
  private IUserRepo _userRepo;
  
  // assuming AutoFac here
  public UsersController(IUserRepo userRepo) {
    this._userRepo = userRepo;
  }

  public ActionResult Index() {
    var columns = JsonConvert.SerializeObject(new List<ColumnHeader>() { 
                new ColumnHeader() { data = "Id", name = "", visible = true, sortable = false, width = "50px", className = "text-center"  },
                new ColumnHeader() { data = "Name", name = "Name", visible = true, sortable = true },
                new ColumnHeader() { data = "Email", name = "Email", visible = true, sortable = true },
                new ColumnHeader() { data = "PhoneNumber", name = "Phone", visible = true, sortable = true }
            });
  ViewBag.Columns = columns;
  return View("Index");
}


[RequireHttps]
[HttpPost]
public JsonResult GetData([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
{
  // get all users 
  var users = _userRepo.All;
  
  // create another collection of user, will use this one to filter our results
  var data = things;
  
  // search value string for convenience, so we don't have to navigate the object model each time
  var searchValue = "";
  
  // filter data if searching across all columns
  if (!string.IsNullOrWhiteSpace(requestModel.Search.Value))
  {
    searchValue = requestModel.Search.Value.ToLower();
    data = data.Where(x => 
      x.Name.ToLower().Contains(searchValue) || 
      x.Email.ToLower().Contains(searchValue) || 
      x.PhoneNumber.ToLower().Contains(searhValue)
  }
  
  // now filter data for each column if there's a searchValue for the columns, if supported in datatables ui
  searchValue = GetColSearchValue(requestModel, "Name");
  if (!string.IsNullOrWhiteSpace(searchValue))
  {
    data = filteredData.Where(x => x.Name.ToLower() == searchValue);
  }
  
  searchValue = GetColSearchValue(requestModel, "Email");
  if (!string.IsNullOrWhiteSpace(searchValue))
  {
    data = filteredData.Where(x => x.Email.ToLower() == searchValue);
  }
  
  searchValue = GetColSearchValue(requestModel, "PhoneNumber");
  if (!string.IsNullOrWhiteSpace(searchValue))
  {
    data = filteredData.Where(x => x.PhoneNumber.ToLower() == searchValue);
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
  retrun new DataTablesResponse(requestModel.Draw, pagedData, data.Count(), users.Count());
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
