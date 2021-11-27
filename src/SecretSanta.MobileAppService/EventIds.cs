using Microsoft.Extensions.Logging;

namespace SecretSanta.MobileAppService
{
    public static class EventIds
    {
        public static readonly EventId TestingDebugLogging = new EventId(1, "TestingDebugLogging");
        public static readonly EventId TestWarning = new EventId(2, "TestWarning");
    }
}
