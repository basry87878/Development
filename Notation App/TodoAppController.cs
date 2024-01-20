using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;



namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoAppController : ControllerBase
    {
        private IConfiguration _configuration;
        public TodoAppController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //get the text method 
        [HttpGet]
        [Route("GetNotes")]
        public JsonResult GetNotes()
        {
            string query = "select * from dbo.Notes";
            
            DataTable table = new DataTable(); //DataTable object store the data retrieved from the SQL query
            string sqlDatasource = _configuration.GetConnectionString("todoAppDBCon"); //contains the necessary details to connect to the database
            SqlDataReader myReader;
            System.Data.SqlClient.SqlConnection myCon = new System.Data.SqlClient.SqlConnection(sqlDatasource); //Creating a SqlConnection


            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon)) //Executing SQL Command and Reading Data
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); //Loads the data from the SqlDataReader into the DataTable
                    myReader.Close(); //Closes the SqlDataReader 
                    myCon.Close(); //Closes the database connection
                }
            }
            return new JsonResult(table); //Returns the data stored in the DataTable as a JSON
        }

        //text addition & deletion method
        [HttpPost]
        [Route("AddNotes")]
        public JsonResult AddNotes([FromForm] string newNotes)
        {
            string query = "insert into dbo.Notes values(@newNotes)";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("todoAppDBCon");
            SqlDataReader myReader;
            System.Data.SqlClient.SqlConnection myCon = new System.Data.SqlClient.SqlConnection(sqlDatasource);


            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@newNotes", newNotes);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added Successfully");
        }
        [HttpDelete]
        [Route("DeleteNotes")]
        public JsonResult DeleteNotes(int id)
        {
            string query = "delete from dbo.Notes where id=@id";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("todoAppDBCon");
            SqlDataReader myReader;
            System.Data.SqlClient.SqlConnection myCon = new System.Data.SqlClient.SqlConnection(sqlDatasource);


            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Deleted Successfully");
        }
    }
}