
using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace HelloWorld.Data;

// DataContextDapper is a database context class responsible for
// managing database communication using Dapper.
//
// Dapper is a lightweight Micro ORM (Object-Relational Mapper).
// It allows us to execute SQL queries and map database results
// directly to C# objects without writing a lot of mapping code.
public class DataContextDapper
{
    // Stores the database connection string.
    // The connection string contains information required to connect
    // to SQL Server, such as the server name, database name,
    // authentication method, and other connection settings.
    //
    // private IConfiguration _config;
    private string _connectionString;

    // Example connection strings:
    // private string _connectionString =
    //     "Server=localhost;Database=DotNetCourseDatabase;TrustServerCertificate=true;Trusted_Connection=true";

    // SQL Server authentication example:
    // private string connectionString =
    //     "Server=localhost;Database=DotNetCourseDatabase;TrustServerCertificate=true;Trusted_Connection=false;User Id=sa;Password=SQLConnect1;";


    // Constructor:
    // IConfiguration provides access to application configuration
    // settings, including connection strings stored in appsettings.json.
    //
    // This constructor runs automatically when DataContextDapper
    // is instantiated through dependency injection.
    public DataContextDapper(IConfiguration config)
    {
        // Retrieve the connection string named "DefaultConnection"
        // from the application's configuration file.
        //
        // Example appsettings.json:
        // {
        //     "ConnectionStrings": {
        //         "DefaultConnection": "Server=localhost;Database=DotNetCourseDatabase;Trusted_Connection=true;TrustServerCertificate=true"
        //     }
        // }
        //
        // If the connection string is missing or null,
        // throw an exception immediately.
        // This helps identify configuration problems early.
        //
        // _config = config;
        _connectionString = config.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException(
                "Connection string 'DefaultConnection' not found.");
    }


    // LoadData<T>:
    // Executes a SQL query and returns multiple records from the database.
    //
    // T is a generic type, meaning this method can return different
    // types of objects depending on what is passed when calling it.
    //
    // For example:
    // IEnumerable<Computer> computers = LoadData<Computer>(sql);
    //
    // Dapper automatically maps the returned SQL columns
    // to the corresponding properties of the Computer model.
    public IEnumerable<T> LoadData<T>(string sql)
    {
        // Create a new SQL Server database connection using
        // the connection string retrieved from configuration.
        IDbConnection dbConnection = new SqlConnection(_connectionString);

        // Query<T> executes the SQL statement and maps every returned
        // database row into an object of type T.
        //
        // Returns IEnumerable<T>, which represents a collection
        // of the retrieved records.
        return dbConnection.Query<T>(sql);
    }


    // LoadSingleData<T>:
    // Executes a SQL query that is expected to return exactly one record.
    //
    // T represents the type of object that should be returned.
    //
    // Example:
    // Computer computer = LoadSingleData<Computer>(sql);
    public T LoadSingleData<T>(string sql)
    {
        // Establish a connection to SQL Server.
        IDbConnection dbConnection = new SqlConnection(_connectionString);

        // QuerySingle<T> executes the query and maps the result
        // into a single object of type T.
        //
        // IMPORTANT:
        // QuerySingle throws an exception if the query returns
        // zero records or more than one record.
        return dbConnection.QuerySingle<T>(sql);
    }


    // ExecuteSql:
    // Executes a SQL command that modifies database records.
    //
    // Common examples include:
    // INSERT, UPDATE, and DELETE statements.
    //
    // Returns true if at least one database row was affected.
    // Returns false if no rows were affected.
    public bool ExecuteSql(string sql)
    {
        // Create a SQL Server database connection.
        IDbConnection dbConnection = new SqlConnection(_connectionString);

        // Execute() runs the SQL command and returns an integer
        // representing the number of rows affected.
        //
        // For example:
        // INSERT affecting 1 row  -> returns 1
        // UPDATE affecting 3 rows -> returns 3
        // DELETE affecting 0 rows -> returns 0
        //
        // We use > 0 to convert the integer result into a boolean.
        return (dbConnection.Execute(sql) > 0);
    }


    // ExecuteSqlWithRowCount:
    // Executes a SQL command and returns the exact number of rows affected.
    //
    // Unlike ExecuteSql(), this method returns an integer
    // instead of true or false.
    //
    // Useful when you need to know how many records were inserted,
    // updated, or deleted.
    public int ExecuteSqlWithRowCount(string sql)
    {
        // Create a SQL Server database connection.
        IDbConnection dbConnection = new SqlConnection(_connectionString);

        // Execute the SQL command.
        // Returns the number of affected rows.
        return dbConnection.Execute(sql);
    }


    // ExecuteSqlWithParameters:
    // Executes a SQL command using parameterized queries.
    //
    // This method accepts:
    // 1. sql        -> The SQL statement to execute.
    // 2. parameters -> An object containing values for SQL parameters.
    //
    // Example:
    //
    // string sql = @"
    //     INSERT INTO Computer (Motherboard, Price)
    //     VALUES (@Motherboard, @Price)";
    //
    // ExecuteSqlWithParameters(sql, new
    // {
    //     Motherboard = "Dell",
    //     Price = 1200.50m
    // });
    //
    // Dapper automatically maps the object's properties
    // to the corresponding SQL parameters.
    //
    // IMPORTANT:
    // Parameterized queries help prevent SQL injection attacks
    // and avoid data conversion problems involving dates,
    // decimals, strings, booleans, and nullable values.
    public void ExecuteSqlWithParameters(string sql, object parameters)
    {
        // Create a database connection.
        //
        // The using statement ensures that the connection is
        // automatically disposed of when execution finishes,
        // even if an exception occurs.
        using (IDbConnection dbConnection = new SqlConnection(_connectionString))
        {
            // Execute the SQL command and pass the parameters to Dapper.
            //
            // Dapper automatically matches object properties
            // with SQL parameter names.
            dbConnection.Execute(sql, parameters);
        }
    }
}
