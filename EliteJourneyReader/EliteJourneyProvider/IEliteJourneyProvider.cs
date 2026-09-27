using EliteJourneyReader.EventMessages;
using EliteJourneyReader.EventMessages.ColonisationEvents;
using EliteJourneyReader.EventMessages.DockingEvents;

namespace EliteJourneyReader.EliteJourneyProvider;

public interface IEliteJourneyProvider
{
    /// <summary>
    /// Sets the directory path to watch file changes in that trigger raising all events
    /// </summary>
    /// <param name="path">Path to watch file changes in</param>
    void SetJournalDirectoryPath(string path);
    
    /// <summary>
    /// Sets the directory path to watch file changes in that trigger raising all events
    /// </summary>
    /// <param name="uri">Path to watch file changes in</param>
    void SetJournalDirectoryPath(Uri uri);

    #region Events

    /// <summary>
    /// Triggered by any in-game event. Contains deserialized message and json value of the message
    /// </summary>
    public event JourneyEventDelegate? OnAnyEvent;
    
    /// <summary>
    /// Triggered if message was received but there was error during deserializing the message,
    /// sending at least the json of original message
    /// </summary>
    public event JourneyErrorEventDelegate? OnReaderError;
    /// <summary>
    /// Triggered when receiving information about a change in a friend's status
    /// </summary>
    public event EventHandler<FriendsEventMessage>? OnFriendsChange;
     
    /// <summary>
    /// Triggered at startup, when loading from main menu into game
    /// </summary>
    public event EventHandler<LoadGameEventMessage>? OnLoadGame;
    
    /// <summary>
    /// Heading entry. If the play session goes on a long time, and the journal gets very large,
    /// the file will be closed and a new file started with an increased part number: the heading entry is added at the beginning of every file.
    /// </summary>
    public event EventHandler<FileHeaderEventMessage>? OnFileHeader;
    
    /// <summary>
    /// Triggered when purchasing goods in the market
    /// </summary>
    public event EventHandler<MarketBuyEventMessage>? OnMarketBuy;
    
    /// <summary>
    /// Triggered when selling goods in the market
    /// </summary>
    public event EventHandler<MarketSellEventMessage>? OnMarketSell;

    /// <summary>
    /// Triggered when zooming in on a signal using the FSS scanner
    /// </summary>
    public event EventHandler<FssSignalDiscoveredEventMessage>? OnFssSignalDiscovered;
    
    /// <summary>
    /// Triggered when scooping fuel from a star
    /// </summary>
    public event EventHandler<FuelScoopEventMessage>? OnFuelScoop;
    
    /// <summary>
    /// Triggered when scooping fuel from a star
    /// </summary>
    public event EventHandler<FsdJumpEventMessage>? OnFsdJump;
    
    /// <summary>
    /// Triggered basic or detailed discovery scan of a star, planet or moon.
    /// This is also generated when scanning a navigation beacon in a populated system, to record info about all the bodies in the system
    /// </summary>
    public event EventHandler<ScanEventMessage>? OnScan;

    /// <summary>
    /// Trigggered when undocking from an outpost, station, fleet carrier, etc
    /// Check if triggered when launch slv
    /// </summary>
    public event EventHandler<UndockedEventMessage>? OnUndocked;

    /// <summary>
    /// Trigggered when requesting docking permission from an outpost, station, fleet carrier, etc
    /// </summary>
    public event EventHandler<DockingRequestedEventMessage>? OnDockingRequested;

    /// <summary>
    /// Trigggered when a docking request has been accepted
    /// </summary>
    public event EventHandler<DockingGrantedEventMessage>? OnDockingGranted;

    /// <summary>
    /// Trigggered when a docking request has been denied
    /// </summary>
    public event EventHandler<DockingDeniedEventMessage>? OnDockingDenied;

    /// <summary>
    /// Trigggered when docking at a landing pad
    /// </summary>
    public event EventHandler<DockedEventMessage>? OnDocked;

    /// <summary>
    /// Trigggered when viewing the commodity market at a station
    /// </summary>
    public event EventHandler<MarketEventMessage>? OnMarket;

    /// <summary>
    /// Trigggered every ~15 seconds while docked at a colonisation construction depot
    /// The behavior on these is weird, I think that these events are batched and all written to the file
    /// at the same time as the next event, usually either ColonisationContribution or Undocked
    /// </summary>
    public event EventHandler<ColonisationConstructionDepotEventMessage>? OnColonisationConstructionDepot;

    /// <summary>
    /// Trigggered after contributing commodities to a colonisation construction depot
    /// </summary>
    public event EventHandler<ColonisationContributionEventMessage>? OnColonisationContribution;
    #endregion
}
