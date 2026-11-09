
using HelloWorld.Models;
using HelloWorld.Data;
using Microsoft.Extensions.Configuration;
using SystemSerializer = System.Text.Json.JsonSerializer; // Using namespace aliases 
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace HelloWorld
{
    class Program
    {
        // Main is the entry point of the application.
        //
        // The async keyword allows us to use await inside this method.
        // Task represents an asynchronous operation that may complete
        // in the future.
        static async Task Main(string[] args)
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
            // Console.WriteLine(fileText);

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
                    // dapper.ExecuteSqlWithParameters(insertNewComputerSql, singleComputer);

                    // Console.WriteLine($"Added computer {singleComputer.ComputerId} to database successfully!");
                }
            }

            // Working with JSON - Option 3 : AutoMapper
            string computersSnakeJson = File.ReadAllText("ComputersSnake.json");

            // Create an ILoggerFactory instance.
            // AutoMapper uses this for internal logging and licensing operations.
            ILoggerFactory loggerFactory = LoggerFactory.Create(builder => { });

            // Configure AutoMapper to map ComputerSnake objects into Computer objects.
            MapperConfiguration mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ComputerSnake, Computer>()

                    // Explicitly map computer_id from the source object
                    // to ComputerId in the destination object.
                    .ForMember(destination => destination.ComputerId, options =>
                        options.MapFrom(source => source.computer_id));

            }, loggerFactory);

            // Create an IMapper instance using the configured mapping rules.
            IMapper mapper = mapperConfig.CreateMapper();

            // Deserialize the JSON string into a collection of ComputerSnake objects.
            IEnumerable<ComputerSnake>? systemDeserializedComputersSnake =
                SystemSerializer.Deserialize<IEnumerable<ComputerSnake>>(computersSnakeJson);

            // Check whether deserialization returned a collection.
            if (systemDeserializedComputersSnake != null)
            {
                // Convert the collection of ComputerSnake objects into Computer objects.
                // AutoMapper automatically maps properties with matching names
                // and uses our custom mapping rule for ComputerId.
                IEnumerable<Computer> computerResults =
                    mapper.Map<IEnumerable<Computer>>(systemDeserializedComputersSnake);

                // Iterate through the mapped Computer objects.
                foreach (Computer computer in computerResults)
                {
                    Console.WriteLine(
                        $"computer# {computer.ComputerId}: {computer.Motherboard}");
                }
            }


            // Working with asynchronous logic.
            //
            // Asynchronous programming allows operations to execute
            // without blocking the current thread while waiting for
            // operations such as delays, network requests, or file I/O.


            // OPTION 1: Creating and running Tasks manually.

            // Create a new Task.
            // The code inside the lambda expression will execute
            // when this Task is started.
            Task firstTask = new Task(() =>
            {
                // Thread.Sleep blocks the thread for 100 milliseconds.
                // Unlike Task.Delay, this is a synchronous blocking operation.
                Thread.Sleep(100);

                // This message is printed after the delay completes.
                Console.WriteLine("Task 1");
            });

            // Start executing the Task.
            // The Task is scheduled to run on a thread managed by
            // the .NET Task Scheduler (normally using the ThreadPool).
            firstTask.Start();


            // OPTION 2: Calling an asynchronous method.

            // Call ConsoleAfterDelayAsync.
            // It starts executing immediately and returns a Task.
            //
            // Task.Delay waits asynchronously without blocking a thread.
            // We store the returned Task so we can await its completion later.
            Task secondTask = ConsoleAfterDelayAsync("Task 2", 150);


            // OPTION 3: Calling a synchronous method.

            // This method blocks the current thread for 75 milliseconds.
            // The next statement cannot execute until this method finishes.
            ConsoleAfterDelay("Sync Delay", 75);


            // Start another asynchronous operation.
            //
            // This operation can progress while we await other Tasks.
            // We don't need to await it immediately.
            Task thirdTask = ConsoleAfterDelayAsync("Task 3", 50);


            // Wait asynchronously for secondTask to finish.
            // The current method pauses here, but the thread is not blocked.
            await secondTask;


            // Wait asynchronously for firstTask to finish.
            // Even though firstTask was started earlier, we explicitly
            // wait for it here before continuing.
            await firstTask;


            // This message is printed only after both secondTask
            // and firstTask have completed.
            Console.WriteLine("After Task 1 was created");


            // Wait for the third asynchronous operation to complete.
            // This ensures that Task 3 finishes before Main exits.
            await thirdTask;

        }

        // Synchronous method:
        // Executes the code sequentially and blocks the calling thread
        // while waiting for the specified delay.
        static void ConsoleAfterDelay(string text, int delayTime)
        {
            // Thread.Sleep pauses the current thread.
            // The thread cannot perform other work during this time.
            Thread.Sleep(delayTime);

            // Print the supplied text after the delay.
            Console.WriteLine(text);
        }


        // Asynchronous method:
        // The async keyword allows this method to use await.
        //
        // Task indicates that this method performs an asynchronous
        // operation and does not return a result.
        static async Task ConsoleAfterDelayAsync(string text, int delayTime)
        {
            // Task.Delay creates a Task that completes after the
            // specified number of milliseconds.
            //
            // Unlike Thread.Sleep, Task.Delay does not block the
            // current thread while waiting.
            //
            // await pauses this method until the delay completes,
            // allowing the thread to perform other work.
            await Task.Delay(delayTime);

            // This statement executes after the delay has completed.
            Console.WriteLine(text);
        }


        // Utility method:
        // Escapes single quotes in a string so they can be represented
        // safely inside a SQL string literal.
        static string EscapeSingleQuote(string input)
        {
            // Replace every single quote (') with two single quotes ('').
            // SQL Server interprets two consecutive single quotes
            // inside a string literal as one actual single quote.
            string output = input.Replace("'", "''");

            // Return the modified string.
            return output;
        }
    }
}