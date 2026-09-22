/// <summary>
/// Represents the top-level object in the USGS GeoJSON response.
/// </summary>
public class FeatureCollection
{
    public Feature[] Features { get; set; } = [];
}

/// <summary>
/// Represents one earthquake feature in the USGS response.
/// </summary>
public class Feature
{
    public EarthquakeProperties Properties { get; set; } = new();
}

/// <summary>
/// Represents the earthquake information needed by this assignment.
/// </summary>
public class EarthquakeProperties
{
    public string? Place { get; set; }

    public double? Mag { get; set; }
}