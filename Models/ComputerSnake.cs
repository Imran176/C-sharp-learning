
namespace HelloWorld.Models;

// ComputerSnake is a model class (POCO - Plain Old CLR Object).
// It represents a Computer entity and defines the properties
// that will be used when working with JSON, Dapper, and Entity Framework Core.
public class ComputerSnake
{
    // Primary key of the ComputerSnake entity.
    // SQL Server can automatically generate this value if the corresponding
    // database column is configured as an IDENTITY column.
    public int computer_id { get; set; }

    // Stores the computer's motherboard name or manufacturer.
    // Initialized to an empty string to avoid a null value by default.
    public string motherboard { get; set; } = "";

    // Stores the number of CPU cores.
    // The ? makes this property nullable, meaning it can contain
    // an integer or null.
    public int? cpu_cores { get; set; }

    // Indicates whether the computer has Wi-Fi connectivity.
    // true means Wi-Fi is available; false means it is not.
    public bool has_wifi { get; set; }

    // Indicates whether the computer supports LTE connectivity.
    // true means LTE is available; false means it is not.
    public bool has_lte { get; set; }

    // Stores the computer's release date.
    // DateTime? means the date can either contain a valid DateTime
    // value or null if the release date is unknown or unavailable.
    public DateTime? release_date { get; set; }

    // Stores the computer's price.
    // decimal is recommended for financial values because it provides
    // better precision for decimal numbers than float or double.
    public decimal price { get; set; }

    // Stores the computer's graphics card name or manufacturer.
    // Initialized to an empty string to avoid a null value by default.
    public string video_card { get; set; } = "";

    // Constructor:
    // This method runs automatically whenever a new Computer object
    // is created using the new keyword.
    //
    // Its purpose here is to initialize properties that should
    // not contain null values.
    public ComputerSnake()
    {
        // If video_card is null, assign an empty string instead.
        if (video_card == null)
        {
            video_card = "";
        }

        // If motherboard is null, assign an empty string instead.
        if (motherboard == null)
        {
            motherboard = "";
        }

        // If cpu_cores is null, assign 0 as its default value.
        if (cpu_cores == null)
        {
            cpu_cores = 0;
        }
    }
}
