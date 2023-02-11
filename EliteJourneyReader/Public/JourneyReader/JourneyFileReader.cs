using System.Text;

namespace EliteJourneyReader.Public.JourneyReader;
public class JourneyFileReader
{
    private readonly JourneyEventProcessor _processor;
    internal string JourneyDirectoryPath { get; set; } = string.Empty;
    
    private string DefaultDirectoryPath => Environment.ExpandEnvironmentVariables(RawDefaultDirectoryPath);
    private string RawDefaultDirectoryPath = "%userprofile%\\Saved Games\\Frontier Developments\\Elite Dangerous\\";
    private FileSystemWatcher FileSystemWatcher { get; }

    internal void SetNewJournalDirectoryPath(string path)
    {
        if (Directory.Exists(path) == false)
            throw new DirectoryNotFoundException($"Path provided to journal directory does not exist! " +
                                                 $"Directory path: {path}");
        
        JourneyDirectoryPath = path;
        FileSystemWatcher.Path = path;
    }

    public JourneyFileReader(JourneyEventProcessor processor)
    {
        JourneyDirectoryPath = DefaultDirectoryPath;
        _processor = processor;
        FileSystemWatcher = new FileSystemWatcher(JourneyDirectoryPath);
        FileSystemWatcher.EnableRaisingEvents = true;
        FileSystemWatcher.Filter = "*.log";
        FileSystemWatcher.Created += WatcherOnCreated;
        FileSystemWatcher.Changed += WatcherOnChanged;
    }
    private EliteJourneyProvider.CallEventDelegate? _callEventDelegate;
    internal void Register(EliteJourneyProvider.CallEventDelegate callEventDelegate)
    {
        _callEventDelegate = callEventDelegate;
    }
    private void WatcherOnChanged(object sender, FileSystemEventArgs e)
    {
        var events = ReadLines(e.FullPath).ToArray();
        foreach (var eventMessage in _processor.ProcessMessages(events))
        {
            var (msg, json) = eventMessage;
            _callEventDelegate?.Invoke(msg, json);
        }
    }

    private void WatcherOnCreated(object sender, FileSystemEventArgs e)
    {
        var events = ReadLines(e.FullPath).ToArray();
        foreach (var eventMessage in _processor.ProcessMessages(events))
        {
            var (msg, json) = eventMessage;
            _callEventDelegate?.Invoke(msg, json);
        }
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
}