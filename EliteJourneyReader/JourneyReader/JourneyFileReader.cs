using System.Text;
using EliteJourneyReader.Public;
using EliteJourneyReader.Public.EliteJourneyProvider;

namespace EliteJourneyReader.JourneyReader;
internal class JourneyFileReader
{
    internal static JourneyFileReader Instance = default!;
    private readonly JourneyEventProcessor _processor;
    internal string JourneyDirectoryPath { get; set; }
    
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

    internal static void SetInstance(JourneyFileReader reader)
    {
        Instance = reader;
    }
    
    public JourneyFileReader(JourneyEventProcessor processor)
    {
        JourneyDirectoryPath = DefaultDirectoryPath;
        _processor = processor;
        FileSystemWatcher = new FileSystemWatcher(JourneyDirectoryPath);
        FileSystemWatcher.EnableRaisingEvents = true;
        FileSystemWatcher.Filter = "*.log";
        FileSystemWatcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size;
        FileSystemWatcher.Created += WatcherOnCreated;
        FileSystemWatcher.Changed += WatcherOnChanged;
    }
    private EliteJourneyProvider.CallEventDelegate? _callEventDelegate;
    internal void Register(EliteJourneyProvider.CallEventDelegate callEventDelegate)
    {
        _callEventDelegate = callEventDelegate;
    }   
    
    internal void Unregister()
    {
        _callEventDelegate = null;
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