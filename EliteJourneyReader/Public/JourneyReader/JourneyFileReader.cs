using System.Text;

namespace EliteJourneyReader.Public.JourneyReader;

public class JourneyFileReader
{
    private readonly JourneyEventMediator _mediator;
    private static string JourneyDirectoryPath => Environment.ExpandEnvironmentVariables(RawDirectoryPath);
    private const string RawDirectoryPath = "%userprofile%\\Saved Games\\Frontier Developments\\Elite Dangerous\\";
    private FileSystemWatcher FileSystemWatcher { get; }

    public JourneyFileReader(JourneyEventMediator mediator)
    {
        _mediator = mediator;
        FileSystemWatcher = new FileSystemWatcher(JourneyDirectoryPath);
        FileSystemWatcher.EnableRaisingEvents = true;
        FileSystemWatcher.Filter = "*.log";
        FileSystemWatcher.Created += WatcherOnCreated;
        FileSystemWatcher.Changed += WatcherOnChanged;
    }
    private EliteJourneyReader.CallEventDelegate? _callEventDelegate;
    private Dictionary<string, Type> _typesDictionary { get; set; } = new();
    internal void Register(EliteJourneyReader.CallEventDelegate callEventDelegate, Dictionary<string, Type> typesDictionary)
    {
        _callEventDelegate = callEventDelegate;
        _typesDictionary = typesDictionary;
    }
    private void WatcherOnChanged(object sender, FileSystemEventArgs e)
    {
        var events = ReadLines(e.FullPath).ToArray();
        foreach (var eventMessage in _mediator.ProcessMessages(events, _typesDictionary))
        {
            var (msg, json) = eventMessage;
            _callEventDelegate?.Invoke(msg, json);
        }
    }

    private void WatcherOnCreated(object sender, FileSystemEventArgs e)
    {
        var events = ReadLines(e.FullPath).ToArray();
        foreach (var eventMessage in _mediator.ProcessMessages(events, _typesDictionary))
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