
using HelloWorld.Models;
using HelloWorld.Data;

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

            // When choosing a data access layer for C# applications, 
            // the choice typically comes down to Dapper (a lightweight Micro-ORM)
            //  and Entity Framework Core (a full-featured Object-Relational Mapper)

            // OPTION 1: Using Dapper
            DataContextDapper dapper = new DataContextDapper();
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
            bool result = dapper.ExecuteSql(sql);
            Console.WriteLine(result);

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
            DataContextEF entityFramework = new DataContextEF();

            entityFramework.Add(myComputer);
            entityFramework.SaveChanges();

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

        }
    }
}