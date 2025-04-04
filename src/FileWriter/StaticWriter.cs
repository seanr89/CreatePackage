
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace FileWriter;
public static class StaticWriter
{

    public static void CheckAndCreateDirectory(string directoryPath = "./files"){
        if(!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
    }

    /// <summary>
    /// Try to write to a file, if the file exists, append to it.
    /// If the file does not exist, create it and write to it.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="directoryPath"></param>
    /// <param name="fileName"></param>
    /// <typeparam name="T"></typeparam>
    public static void TryWriteOrAppendToFile<T>(IEnumerable<T> data, string directoryPath, string fileName)
    {
        // Check if the directory exists and create it if it doesn't
        CheckAndCreateDirectory(directoryPath);
        var combinedPath = Path.Combine(directoryPath, fileName);
        // Check if the file exists
        if(FileExists(directoryPath, fileName))
        {
            AppendToFile(data, combinedPath);
        }
        else
        {
            WriteToFile(data, combinedPath);
        }
    }

    static void WriteToFile<T>(IEnumerable<T> data, string writePath)
    {
    
        // Append to the file.
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            // Don't write the header again.
            HasHeaderRecord = true,
            Delimiter = "|"
        };
        using (var writer = new StreamWriter(writePath))
        using (var csv = new CsvWriter(writer, config))
        {
            var options = new TypeConverterOptions { Formats = ["dd/MM/yyyy"] };
            csv.Context.TypeConverterOptionsCache.AddOptions<DateTime>(options);          
            csv.WriteRecords(data);
        }
    }

    static void AppendToFile<T>(IEnumerable<T> data, string writePath)
    {
        // Append to the file.
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            // Don't write the header again.
            HasHeaderRecord = false,
            Delimiter = "|"
        };
        // Append to file
        using (var stream = File.Open(writePath, FileMode.Append))
        using (var writer = new StreamWriter(stream))
        using (var csv = new CsvWriter(writer, config))
        {
            var options = new TypeConverterOptions { Formats = ["dd/MM/yyyy"] };
            csv.Context.TypeConverterOptionsCache.AddOptions<DateTime>(options);       
            csv.WriteRecords(data);
        }
    }

    static bool FileExists(string directoryPath, string fileName)
    {
        // Check if the file exists
        if(File.Exists(Path.Combine(directoryPath, fileName)))
        {
            return true;
        }
        return false;
    }
}