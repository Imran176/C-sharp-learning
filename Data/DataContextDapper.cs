using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace HelloWorld.Data;

public class DataContextDapper
{
    private string _connectionString = "Server=localhost;Database=DotNetCourseDatabase;TrustServerCertificate=true;Trusted_Connection=true";
    // private string connectionString = "Server=localhost;Database=DotNetCourseDatabase;TrustServerCertificate=true;Trusted_Connection=false;User Id=sa;Password=SQLConnect1;";

    public IEnumerable<T> LoadData<T>(string sql)
    {
        IDbConnection dbConnection = new SqlConnection(_connectionString);
        return dbConnection.Query<T>(sql);
    }

    public T LoadSingleData<T>(string sql)
    {
        IDbConnection dbConnection = new SqlConnection(_connectionString);
        return dbConnection.QuerySingle<T>(sql);
    }

    public bool ExecuteSql(string sql)
    {
        IDbConnection dbConnection = new SqlConnection(_connectionString);
        return (dbConnection.Execute(sql) > 0); // .Execute will return integer
    }

    public int ExecuteSqlWithRowCount(string sql)
    {
        IDbConnection dbConnection = new SqlConnection(_connectionString);
        return dbConnection.Execute(sql);
    }

}