namespace Shared.Helpers;

public class InputReader
{
    public string FilePath { get; set; }
    public string InputFolderName { get; private set; } = "Inputs";

    public InputReader(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
        {
            throw new ArgumentException($"'{nameof(fileName)}' cannot be null or whitespace.", nameof(fileName));
        }

        string inputDirectory = GetInputDirectory();
        var filePath = Path.Combine(inputDirectory, fileName);
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"File '{filePath}' not found!");
        }

        FilePath = filePath;
    }

    private string GetInputDirectory()
    {
        var directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory());

        var rootDirectory = directoryInfo.Parent?.Parent?.Parent?.FullName;

        if (rootDirectory is null)
        {
            throw new DirectoryNotFoundException(nameof(rootDirectory));
        }

        return Path.Combine(rootDirectory, InputFolderName);
    }

    public List<string> ReadFile()
    {
        var fileContent = File.ReadAllText(FilePath);

        return fileContent.Split("\r\n", StringSplitOptions.RemoveEmptyEntries).ToList();
    }
}