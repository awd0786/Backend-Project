using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace Backend_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public EmployeeController (IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult GetEmployee()
        {
            string query = @"Exec SP_EmployeeList_BackendProject";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("BackendProjectConnection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        //[HttpGet]
        //public JsonResult GetEmployee(string action = null)
        //{
        //    string query = "EXEC SP_EmployeeList_BackendProject @Action";
        //    DataTable table = new DataTable();
        //    string sqlDataSource = _configuration.GetConnectionString("BackendProjectConnection");

        //    using (SqlConnection myCon = new SqlConnection(sqlDataSource))
        //    {
        //        myCon.Open();

        //        using (SqlCommand myCommand = new SqlCommand(query, myCon))
        //        {
        //            myCommand.Parameters.Add("@Action", SqlDbType.NVarChar).Value = (object)action ?? DBNull.Value;

        //            using (SqlDataReader myReader = myCommand.ExecuteReader())
        //            {
        //                table.Load(myReader);
        //            }
        //        }
        //    }

        //    return new JsonResult(table);
        //}


    }
}
