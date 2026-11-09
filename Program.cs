
using HelloWorld.Models;
using HelloWorld.Data;
using Microsoft.Extensions.Configuration;
using SystemSerializer = System.Text.Json.JsonSerializer; // Using namespace aliases 
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {

            // Console.WriteLine("Hello, World!");
            // Console.Write("One");
            // Console.Write("Two");
            // Console.WriteLine("Hello, C# Programmers!");
            // Console.WriteLine(5 + 5);

            //// 1 byte is made up of 8 bits 00000000 - these bits can be used to store a number as follows
            // //// Each bit can be worth 0 or 1 of the value it is placed in
            // ////// From the right we start with a value of 1 and double for each digit to the left
            // //// 00000000 = 0
            // //// 00000001 = 1
            // //// 00000010 = 2
            // //// 00000011 = 3
            // //// 00000100 = 4
            // //// 00000101 = 5
            // //// 00000110 = 6
            // //// 00000111 = 7
            // //// 00001000 = 8

            // 1 byte (8 bit) unsigned, where signed means it can be negative
            // byte myByte = 255;
            // byte mySecondByte = 0;

            // 1 byte (8 bit) signed, where signed means it can be negative
            // sbyte mySbyte = 127;
            // sbyte mySecondSbyte = -128;


            // 2 byte (16 bit) unsigned, where signed means it can be negative
            // ushort myUshort = 65535;

            // 2 byte (16 bit) signed, where signed means it can be negative
            // short myShort = -32768;

            // 4 byte (32 bit) signed, where signed means it can be negative
            // int myInt = 2147483647;
            // int mySecondInt = -2147483648;

            // 8 byte (64 bit) signed, where signed means it can be negative
            // long myLong = -9223372036854775808;


            // 4 byte (32 bit) floating point number
            // float myFloat = 0.751f;
            // float mySecondFloat = 0.75f;

            // 8 byte (64 bit) floating point number
            // double myDouble = 0.751;
            // double mySecondDouble = 0.75d;

            // 16 byte (128 bit) floating point number
            // decimal myDecimal = 0.751m;
            // decimal mySecondDecimal = 0.75m;

            // Console.WriteLine(myFloat - mySecondFloat);
            // Console.WriteLine(myDouble - mySecondDouble);
            // Console.WriteLine(myDecimal - mySecondDecimal);


            // string myString = "Hello World";
            // Console.WriteLine(myString);
            // string myStringWithSymbols = "!@#$@^$%%^&(&%^*__)+%^@##$!@%123589071340698ughedfaoig137";
            // Console.WriteLine(myStringWithSymbols);

            // bool myBool = true;

            Computer myComputer = new Computer()
            {
                Motherboard = "Intel Core i7",
                HasWifi = true,
                HasLTE = false,
                ReleaseDate = DateTime.Now,
                Price = 945.5m,
                VideoCard = "RTX 5070",
            };

            IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

            // When choosing a data access layer for C# applications, 
            // the choice typically comes down to Dapper (a lightweight Micro-ORM)
            //  and Entity Framework Core (a full-featured Object-Relational Mapper)

            // OPTION 1: Using Dapper
            DataContextDapper dapper = new DataContextDapper(config);
            string sqlCommand = "SELECT GETDATE()";
            DateTime rightNow = dapper.LoadSingleData<DateTime>(sqlCommand);
            Console.WriteLine(rightNow);

            string sql = @"INSERT INTO Project1AppSchema.Computer (
                Motherboard,
                HasWifi,
                HasLTE,
                ReleaseDate,
                Price,
                VideoCard   
            ) VALUES ('" + myComputer.Motherboard
                            + "','" + myComputer.HasWifi
                            + "','" + myComputer.HasLTE
                            + "','" + myComputer.ReleaseDate
                            + "','" + myComputer.Price
                            + "','" + myComputer.VideoCard
                         + "')";
            // int result = dapper.ExecuteSqlWithRowCount(sql);
            // dapper.ExecuteSql(sql);

            string sqlSelect = @"SELECT 
                 Computer.ComputerId,
                 Computer.Motherboard,
                Computer.HasWifi,
                Computer.HasLTE,
                Computer.ReleaseDate,
                Computer.Price,
                Computer.VideoCard
                FROM Project1AppSchema.Computer";

            IEnumerable<Computer> computers = dapper.LoadData<Computer>(sqlSelect);
            // IEnumerable<Computer> computers = dbConnection.Query<Computer>(sqlSelect).ToList();

            foreach (Computer singleComputer in computers)
            {
                Console.WriteLine("'ComputerId', 'Motherboard', 'CPUCores', 'HasWifi', 'HasLTE', 'ReleaseDate', 'Price', 'VideoCard'");

                Console.WriteLine("'"
                + singleComputer.ComputerId
                + "','" + singleComputer.Motherboard
                + "','" + singleComputer.CPUCores
                + "','" + singleComputer.HasWifi
                + "','" + singleComputer.HasLTE
                + "','" + singleComputer.ReleaseDate
                + "','" + singleComputer.Price
                + "','" + singleComputer.VideoCard
                 + "'");
            }

            // OPTION 2: Using Entity Framework Core
            DataContextEF entityFramework = new DataContextEF(config);

            // entityFramework.Add(myComputer);
            // entityFramework.SaveChanges();

            IEnumerable<Computer>? computersEf = entityFramework.Computer?.ToList<Computer>();
            if (computersEf != null)
            {
                foreach (Computer singleComputer in computersEf)
                {
                    Console.WriteLine("'ComputerId', 'Motherboard', 'CPUCores', 'HasWifi', 'HasLTE', 'ReleaseDate', 'Price', 'VideoCard'");

                    Console.WriteLine("'"
                    + singleComputer.ComputerId
                    + "','" + singleComputer.Motherboard
                    + "','" + singleComputer.CPUCores
                    + "','" + singleComputer.HasWifi
                    + "','" + singleComputer.HasLTE
                    + "','" + singleComputer.ReleaseDate
                    + "','" + singleComputer.Price
                    + "','" + singleComputer.VideoCard
                 + "'");
                }
            }


            // Write File - Option 1 : Create a file and write text into the file 
            // Console.WriteLine("=============== Write File - Option 1 ===============");
            // File.WriteAllText("log.txt", "\n" + sql + "\n");

            // // Write File - Option 2 : Create a file and write text into the file 
            // Console.WriteLine("=============== Write File - Option 2 ===============");
            // using StreamWriter openFile = new("log.txt", append: true);
            // openFile.WriteLine("\n" + sql + "\n");
            // openFile.Close();

            // Read File - Option 1 :
            Console.WriteLine("=============== Read File - Option 1 ===============");
            string fileText = File.ReadAllText("log.txt");
            Console.WriteLine(fileText);

            // Working with JSON files
            Console.WriteLine("=============== Reading Computers JSON File ===============");
            string computersJson = File.ReadAllText("Computers.json");

            // Working with JSON - Option 1 : System.Text.Json

            // Configure System.Text.Json serialization options.
            // PropertyNamingPolicy controls how C# property names are written
            // when converting objects into JSON.
            //
            // CamelCase converts PascalCase property names into camelCase.
            // Example: ComputerId becomes computerId.
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            // Deserialize the JSON string into a collection of Computer objects.
            // SystemSerializer refers to System.Text.Json.JsonSerializer.
            //
            // It reads the JSON properties and maps them to their corresponding
            // C# properties in the Computer model.
            //
            // The options parameter tells the serializer to use the configured
            // JSON serialization settings.
            IEnumerable<Computer>? SystemDeserializedComputers =
                SystemSerializer.Deserialize<IEnumerable<Computer>>(computersJson, options);

            // Serialize the deserialized Computer objects back into a JSON string.
            // The same options are passed to ensure that property names
            // are written in camelCase format.
            string computerCopySystem =
                SystemSerializer.Serialize(SystemDeserializedComputers, options);

            // Write the serialized JSON string to a text file.
            // This allows us to inspect the output and verify that
            // System.Text.Json serialization is working correctly.
            File.WriteAllText("computerCopySystem.txt", computerCopySystem);



            // Working with JSON - Option 2 : Newtonsoft.Json

            // Configure Newtonsoft.Json serialization settings.
            // ContractResolver changes property names to camelCase when serializing objects.
            // For example: ComputerId becomes computerId.
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };


            // Deserialize the JSON string into a collection of Computer objects.
            // Newtonsoft.Json automatically maps JSON properties to the corresponding
            // C# properties and converts values such as dates, booleans, and integers.
            IEnumerable<Computer>? newtonsoftDeserializedComputers = JsonConvert.DeserializeObject<IEnumerable<Computer>>(computersJson);

            // Serialize the deserialized Computer objects back into JSON.
            // The CamelCasePropertyNamesContractResolver ensures that property names
            // are written in camelCase format.
            string computerCopyNewtonsoft = JsonConvert.SerializeObject(newtonsoftDeserializedComputers, settings);

            // Save the serialized JSON into a text file.
            // This is useful for debugging and verifying that deserialization
            // and serialization are working as expected.
            File.WriteAllText("computerCopyNewtonsoft.txt", computerCopyNewtonsoft);

            // Check whether deserialization returned a valid collection.
            // If the JSON is null, there is no collection to iterate over.
            if (newtonsoftDeserializedComputers != null)
            {
                // Iterate through each Computer object in the deserialized collection.
                foreach (Computer singleComputer in newtonsoftDeserializedComputers)
                {
                    // Prepare an SQL INSERT statement to add the current Computer
                    // object into the Computer table.
                    //
                    // IMPORTANT:
                    // Use SQL parameters instead of string concatenation.
                    // This prevents SQL injection and avoids problems with
                    // converting C# values (especially DateTime) into SQL values.
                    string insertNewComputerSql = @"
                    INSERT INTO Project1AppSchema.Computer (
                        Motherboard,
                        HasWifi,
                        HasLTE,
                        ReleaseDate,
                        Price,
                        VideoCard
                    )
                    VALUES (
                        @Motherboard,
                        @HasWifi,
                        @HasLTE,
                        @ReleaseDate,
                        @Price,
                        @VideoCard
                    )";

                    // Execute the INSERT statement using Dapper.
                    //
                    // Dapper automatically maps the properties of singleComputer
                    // to the corresponding SQL parameters.
                    //
                    // For example:
                    // @Motherboard -> singleComputer.Motherboard
                    // @HasWifi     -> singleComputer.HasWifi
                    // @ReleaseDate -> singleComputer.ReleaseDate
                    //
                    // DateTime values are passed as typed SQL parameters rather
                    // than being converted into culture-dependent strings.
                    //
                    // Nullable properties are also handled automatically.
                    dapper.ExecuteSqlWithParameters(insertNewComputerSql, singleComputer);

                    Console.WriteLine($"Added computer {singleComputer.ComputerId} to database successfully!");
                }
            }
        }

        static string EscapeSingleQuote(string input)
        {
            string output = input.Replace("'", "''");
            return output;
        }
    }
}