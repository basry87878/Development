namespace WebApplication1.Controllers
{
    internal class SqlConnection
    {
        private string sqlDatasource;

        public SqlConnection(string sqlDatasource)
        {
            this.sqlDatasource = sqlDatasource;
        }
    }
}