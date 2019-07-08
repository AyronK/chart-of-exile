# Watcher's Eye

**master**  
[![master](https://gitlab.com/ayronk-path-of-exile/watchers-eye/badges/master/pipeline.svg)](https://gitlab.com/ayronk-path-of-exile/watchers-eye/commits/master)  
**develop**  
[![develop](https://gitlab.com/ayronk-path-of-exile/watchers-eye/badges/master/pipeline.svg)](https://gitlab.com/ayronk-path-of-exile/watchers-eye/commits/develop)  

.NET Standard 2.0 library for tracking and processing Path of Exile log file.

# Usage

## Quick setup and go

```csharp

//setup our DI
IServiceCollection services = new ServiceCollection().AddGameClientMonitor();

//setup provider
using(IServiceProvider serviceProvider = services.BuildServiceProvider())
{
    // resolve INotificationMonitor
    INotificationMonitor monitor = serviceProvider.GetService<INotificationMonitor>();
    
    // subscribe to event
    monitor.NotificationReceived += (sender, args) =>
    {
        // handle required case
        if(args.Notification is AfkNotification afkNotification)
        {
            Console.WriteLine(afkNotification.IsActive ? "Hey wake up!" : "AFK disabled");
        }
    }
}
```

## Configuration

You can configure INotificationMonitor by using optional parameter of dependency injection extension method:

```csharp
services.AddGameClientMonitor(config =>
{
    config.ClientTxtPath = @"D:\Steam\steamapps\common\Path of Exile\logs\Client.txt";
    config.IsOnlyFirstMatchHandled = true;
    config.UseDefaultMatchings = false;
    config.NotificationMatches = new []{ YourNotificationMath, AnotherNotificationMatch }
});
```

- `ClientTxtPath`: custom path to Client.txt
   - default: `"C:\Program Files\Steam\steamapps\common\Path of Exile\logs\Client.txt"`
- `IsOnlyFirstMatchHandled`: whether monitor stops processing log line after meeting first match
   - default: `true`
- `UseDefaultMatchings`: whether default matching rules are registered (see **Supported Matching Rules** below)
   - default: `true`
- `NotificationMatches`: collection of custom matching rules (see **Custom Matching Rules** below)
   - default: `null`

# Supported Matching Rules

- Area enter - location name
- AFK on/off - is enabled
- Level up - login, level

Planned in the near future:
- chat messages (global, trade, pm etc.)
- NPC dialoges (chat messages with additional filter on constant names)
- error notifications with additional filter for "Disconnected from server"

# Custom Matching Rules

It is possible to write and register custom matching rules based on regular expression and capturing group mapping. This feature allows to handle specific cases like trade message, map exchange or bulk purchase templates.

```csharp
INotificationMatch notificaionMatch = new NotificationMatch
(
    regex: new Regex(@"Connect time to instance server was (\d*)ms"),
    onMap: (groups, metadata) => new ConnectionNotification(int.Parse(groups[1].Value), metadata)
);

...

class ConnectionNotification : BaseNotification
{
    public int ConnectTime { get; }

    public ConnectionNotification(int connectTime, LogMetadata metadata) : base(metadata)
    {
        ConnectTime = connectTime;
    }
}

...

// subscribe to event
monitor.NotificationReceived += (sender, args) =>
{
    // handle required case
    if(args.Notification is ConnectionNotification connectionNotification)
    {
        Console.WriteLine($"Your connect time was {connectionNotification.ConnectTime} ms");
    }
}
```
   