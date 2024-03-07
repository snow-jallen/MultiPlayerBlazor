using CommunityToolkit.Mvvm.Messaging;
using System.Collections.Concurrent;

namespace BlazorApp2;

public static class GameLogic
{
    private static ConcurrentDictionary<string, long> playerValues = new();

    //If you are using built-in events you need to declare the event for subscribers to...subscribe to.
    public static event EventHandler<IEnumerable<(string, long)>>? ValuesChanged;

    public static void ScorePlayer(string playerName)
    {
        int increment = playerName switch
        {
            "JAMES" => 5,
            "Lydia" => 100,
            "CORA" => 200,
            "Daddy" => 1_234_567_891,
            _ => 1
        };

        increment = 1_234_567_891;

        playerValues.AddOrUpdate(playerName, 1, (_, currentScore) => currentScore+increment);

        //Using built-in events
        ValuesChanged?.Invoke(null, playerValues.Select(p => (p.Key, p.Value)));

        //Using the WeakReferenceMessenger from the CommunityToolkit.MVVM library
        WeakReferenceMessenger.Default.Send<IEnumerable<(string, long)>>(playerValues.Select(p => (p.Key, p.Value)));
    }


}
