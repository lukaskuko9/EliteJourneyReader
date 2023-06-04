using System.Text;
using EliteJourneyReader.DI;
using Microsoft.Extensions.Options;

namespace EliteJourneyReader.JourneyReader;
internal class JourneyFileReader : IJourneyFileReader
{
    public event JourneyEventDelegate OnNewEventRegistered = default!;

    private string JourneyDirectoryPath { get; set; }
    private string DefaultDirectoryPath => Environment.ExpandEnvironmentVariables(RawDefaultDirectoryPath);
    private string RawDefaultDirectoryPath = "%userprofile%\\Saved Games\\Frontier Developments\\Elite Dangerous\\";
    private FileSystemWatcher FileSystemWatcher { get; }

    public void SetNewJournalDirectoryPath(string path)
    {
        if (Directory.Exists(path) == false)
            throw new DirectoryNotFoundException($"Path provided to journal directory does not exist! " +
                                                 $"Directory path: {path}");
        
        JourneyDirectoryPath = path;
        FileSystemWatcher.Path = path;
    }

    public JourneyFileReader(IOptions<JourneyReaderOptions> options)
    {
        JourneyDirectoryPath = DefaultDirectoryPath;
        FileSystemWatcher = new FileSystemWatcher(JourneyDirectoryPath);
        FileSystemWatcher.EnableRaisingEvents = options.Value.AutoStartProcessingMessages;
        FileSystemWatcher.Filter = "*.log";
        FileSystemWatcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size;
        FileSystemWatcher.Created += WatcherOnCreated;
        FileSystemWatcher.Changed += WatcherOnChanged;
    }

    private void WatcherOnChanged(object sender, FileSystemEventArgs e)
    {
        ProcessFile(e.FullPath);
    }

    private void WatcherOnCreated(object sender, FileSystemEventArgs e)
    {
        ProcessFile(e.FullPath);
    }

    private void ProcessFile(string fullPath)
    {
        var events = ReadLines(fullPath).ToArray();
        OnNewEventRegistered.Invoke(events);
    }

    private IEnumerable<string> ReadLines(string path)
    {
        using var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        using var sr = new StreamReader(fs, Encoding.Default);
        while (!sr.EndOfStream)
        {
            var s = sr.ReadLine();
            if (s is not null)
                yield return s;
        }
    }

    public void EnableReading(bool enable)
    {
        FileSystemWatcher.EnableRaisingEvents = enable;
    }
}