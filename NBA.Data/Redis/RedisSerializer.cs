using System.Text.Json;
using System.Text.Json.Serialization;

namespace NBA.Data.Redis
{
    // Single canonical serializer for everything persisted to Redis and to the draft snapshot.
    // Previously three different JsonSerializerOptions instances were in play (MVC defaults for the
    // Redis layer, configured Http.Json options for the snapshot), so the same object could round
    // trip through mismatched settings. One shared, immutable instance removes that footgun and is
    // also the recommended pattern for System.Text.Json metadata caching.
    public static class RedisSerializer
    {
        public static readonly JsonSerializerOptions Options = new()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
        };
    }
}
