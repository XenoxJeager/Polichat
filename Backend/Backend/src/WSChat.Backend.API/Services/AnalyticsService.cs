using System.Collections.Generic;
using Polichat_Backend.Models;

namespace Polichat_Backend.Services;

// TODO: spread these out and store them in a string obj dictionary
public class WebSocketChatAnalytics
{
    public ulong ActiveUsers { get; set; }
    public ulong TotalChatMessages { get; set; }
    
    public ulong TotalUsers { get; set; }
}

public class ApiAnalytics
{
    public ulong TotalApiCalls { get; set; }
}

[SingletonService]
public class AnalyticsService
{
    public Dictionary<Room, WebSocketChatAnalytics> ChatAnalytics { get; set; } = new();
    public ApiAnalytics ApiAnalytics { get; set; } = new();
    public uint EvaluationAnalytics { get; set; }
}
