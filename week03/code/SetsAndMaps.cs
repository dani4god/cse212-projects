using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// Find symmetric pairs of two-character words using a set.
    ///
    /// For example:
    /// [am, at, ma, if, fi]
    ///
    /// Returns:
    /// ["ma & am", "fi & if"]
    ///
    /// This solution runs in O(n) time.
    /// </summary>
    /// <param name="words">
    /// An array of two-character lowercase words with no duplicates.
    /// </param>
    public static string[] FindPairs(string[] words)
    {
        // The set stores words that we have already examined.
        var previouslySeen = new HashSet<string>();

        // This list stores the symmetric pairs that we find.
        var pairs = new List<string>();

        foreach (string word in words)
        {
            // Words such as "aa" cannot form a pair because the input
            // does not contain duplicates.
            if (word[0] == word[1])
            {
                continue;
            }

            // Reverse the two-character word.
            string reversedWord = $"{word[1]}{word[0]}";

            // If the reversed word has already been seen, then the
            // current word and reversed word form a symmetric pair.
            if (previouslySeen.Contains(reversedWord))
            {
                pairs.Add($"{word} & {reversedWord}");
            }
            else
            {
                // Store the word so a reversed version appearing later
                // can find it.
                previouslySeen.Add(word);
            }
        }

        return pairs.ToArray();
    }

    /// <summary>
    /// Read a census file and summarize the degrees earned by the
    /// people contained in the file.
    ///
    /// The degree is stored in the fourth column. The dictionary key
    /// is the degree name, and the value is the number of people who
    /// have earned that degree.
    /// </summary>
    /// <param name="filename">The name of the census file to read.</param>
    /// <returns>A dictionary containing degree names and totals.</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();

        foreach (string line in File.ReadLines(filename))
        {
            // Divide the line into its separate columns.
            string[] fields = line.Split(',');

            // Index 3 represents the fourth column because indexes begin at 0.
            // Trim removes spaces around the degree name.
            string degree = fields[3].Trim();

            // Increase the existing total if the degree is already present.
            if (degrees.ContainsKey(degree))
            {
                degrees[degree]++;
            }
            else
            {
                // Add the degree to the dictionary the first time it appears.
                degrees[degree] = 1;
            }
        }

        return degrees;
    }

    /// <summary>
    /// Determine whether two strings are anagrams.
    ///
    /// Spaces and letter case are ignored. A dictionary is used to
    /// count how many times each character appears.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // Store the number of times each character appears.
        var letterCounts = new Dictionary<char, int>();

        // Count the characters in the first string.
        foreach (char originalLetter in word1)
        {
            // Ignore spaces, tabs, and other whitespace.
            if (char.IsWhiteSpace(originalLetter))
            {
                continue;
            }

            // Convert the character to lowercase so letter case is ignored.
            char letter = char.ToLowerInvariant(originalLetter);

            if (letterCounts.ContainsKey(letter))
            {
                letterCounts[letter]++;
            }
            else
            {
                letterCounts[letter] = 1;
            }
        }

        // Subtract the characters found in the second string.
        foreach (char originalLetter in word2)
        {
            if (char.IsWhiteSpace(originalLetter))
            {
                continue;
            }

            char letter = char.ToLowerInvariant(originalLetter);

            // If the character did not appear in the first string,
            // the strings cannot be anagrams.
            if (!letterCounts.ContainsKey(letter))
            {
                return false;
            }

            letterCounts[letter]--;

            // A negative count means the second string contains this
            // character more times than the first string.
            if (letterCounts[letter] < 0)
            {
                return false;
            }
        }

        // Every character count must be zero for the strings
        // to be anagrams.
        foreach (int count in letterCounts.Values)
        {
            if (count != 0)
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Read current-day earthquake JSON data from the United States
    /// Geological Survey and return formatted earthquake descriptions.
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri =
            "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";

        using var client = new HttpClient();
        using var getRequestMessage =
            new HttpRequestMessage(HttpMethod.Get, uri);

        using var response = client.Send(getRequestMessage);

        // Throw an exception if the server returns an unsuccessful response.
        response.EnsureSuccessStatusCode();

        using var jsonStream = response.Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);

        string json = reader.ReadToEnd();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        FeatureCollection? featureCollection =
            JsonSerializer.Deserialize<FeatureCollection>(json, options);

        // Return an empty array if the JSON could not be deserialized.
        if (featureCollection == null)
        {
            return [];
        }

        var summaries = new List<string>();

        foreach (Feature feature in featureCollection.Features)
        {
            string place = feature.Properties.Place ?? "Unknown location";
            double? magnitude = feature.Properties.Mag;

            summaries.Add($"{place} - Mag {magnitude}");
        }

        return summaries.ToArray();
    }
}