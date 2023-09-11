
namespace Framework.Core.Caching
{
    /// <summary>
    /// Represents default values related to caching
    /// </summary>
    public static partial class CachingDefaults
    {
        /// <summary>
        /// Gets the default cache time in minutes
        /// </summary>
        public static int CacheTime => 60;
        public static int LookupsCacheTime => 20000; // Two Weeks, because it didn't change

        /// <summary>
        /// Gets a key for caching
        /// </summary>
        public static string SettingsAllCacheKey => "settings.all";
        public static string LookupsAllCacheKey => "lookups";



    }
}