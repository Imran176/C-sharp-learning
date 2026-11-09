
namespace HelloWorld.Models;

// Computer is a model class (POCO - Plain Old CLR Object).
// It represents a Computer entity and defines the properties
// that will be used when working with JSON, Dapper, and Entity Framework Core.
public class Computer
{
    // Primary key of the Computer entity.
    // SQL Server can automatically generate this value if the corresponding
    // database column is configured as an IDENTITY column.
    //
    // To map JSON properties with different names to C# properties,
    // we can use the [JsonPropertyName] attribute from System.Text.Json.
    //
    // Place [JsonPropertyName("property_name_in_json")] directly above
    // the C# property that should be mapped to the specified JSON property.
    //
    // Example:
    // JSON property: "computer_id"
    // C# property: ComputerId
    //
    // [JsonPropertyName("computer_id")]
    public int ComputerId { get; set; }

    // Stores the computer's motherboard name or manufacturer.
    // Initialized to an empty string to avoid a null value by default.
    public string Motherboard { get; set; } = "";

    // Stores the number of CPU cores.
    // The ? makes this property nullable, meaning it can contain
    // an integer or null.
    public int? CPUCores { get; set; }

    // Indicates whether the computer has Wi-Fi connectivity.
    // true means Wi-Fi is available; false means it is not.
    public bool HasWifi { get; set; }

    // Indicates whether the computer supports LTE connectivity.
    // true means LTE is available; false means it is not.
    public bool HasLTE { get; set; }

    // Stores the computer's release date.
    // DateTime? means the date can either contain a valid DateTime
    // value or null if the release date is unknown or unavailable.
    public DateTime? ReleaseDate { get; set; }

    // Stores the computer's price.
    // decimal is recommended for financial values because it provides
    // better precision for decimal numbers than float or double.
    public decimal Price { get; set; }

    // Stores the computer's graphics card name or manufacturer.
    // Initialized to an empty string to avoid a null value by default.
    public string VideoCard { get; set; } = "";

    // Constructor:
    // This method runs automatically whenever a new Computer object
    // is created using the new keyword.
    //
    // Its purpose here is to initialize properties that should
    // not contain null values.
    public Computer()
    {
        // If VideoCard is null, assign an empty string instead.
        if (VideoCard == null)
        {
            VideoCard = "";
        }

        // If Motherboard is null, assign an empty string instead.
        if (Motherboard == null)
        {
            Motherboard = "";
        }

        // If CPUCores is null, assign 0 as its default value.
        if (CPUCores == null)
        {
            CPUCores = 0;
        }
    }
}
