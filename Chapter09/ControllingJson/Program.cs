using Packt.Shared;
using System.Text.Json;

Book csharpBook = new("C# 12 and .net 8 – Modern Cross-Platform Development")
{
    Author = "Mark J. Price",
    PublishDate = new DateTime(2023, 11, 14),
    Created = DateTimeOffset.UtcNow,
    Pages = 823
};

JsonSerializerOptions options = new()
{
    IncludeFields = true, //Includes all fields 
    PropertyNameCaseInsensitive = true,
    WriteIndented = true,
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
};

string path = Combine(CurrentDirectory, "book.json");

using (Stream fileStream = File.Create(path)) 
{
    JsonSerializer.Serialize(utf8Json: fileStream, value: csharpBook, options);
}

WriteLine("**** FILE INFOR ****");
WriteLine($"File name: {GetFileName(path)}");
WriteLine($"Path: {GetDirectoryName(path)}");
WriteLine($"Size: {new FileInfo(path).Length:N0} bytes");
WriteLine("/------------------------/");
WriteLine(File.ReadAllText(path));
WriteLine("/------------------------/");

