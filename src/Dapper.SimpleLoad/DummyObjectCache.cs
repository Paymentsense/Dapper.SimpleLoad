using System;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace Dapper.SimpleLoad
{
    public class DummyObjectCache : ObjectCache
    {
        public override CacheEntryChangeMonitor CreateCacheEntryChangeMonitor(IEnumerable<string> keys, string regionName = null)
        {
            return null;
        }

        protected override IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return new List<KeyValuePair<string, object>>().GetEnumerator();
        }

        public override bool Contains(string key, string regionName = null)
        {
            return false;
        }

        public override object AddOrGetExisting(string key, object value, DateTimeOffset absoluteExpiration, string regionName = null)
        {
            return value;
        }

        public override CacheItem AddOrGetExisting(CacheItem value, CacheItemPolicy policy)
        {
            return value;
        }

        public override object AddOrGetExisting(string key, object value, CacheItemPolicy policy, string regionName = null)
        {
            return value;
        }

        public override object Get(string key, string regionName = null)
        {
            return null;
        }

        public override CacheItem GetCacheItem(string key, string regionName = null)
        {
            return null;
        }

        public override void Set(string key, object value, DateTimeOffset absoluteExpiration, string regionName = null)
        {
        }

        public override void Set(CacheItem item, CacheItemPolicy policy)
        {
        }

        public override void Set(string key, object value, CacheItemPolicy policy, string regionName = null)
        {
        }

        public override IDictionary<string, object> GetValues(IEnumerable<string> keys, string regionName = null)
        {
            return new Dictionary<string, object>();
        }

        public override object Remove(string key, string regionName = null)
        {
            return null;
        }

        public override long GetCount(string regionName = null)
        {
            return 0;
        }

        public override DefaultCacheCapabilities DefaultCacheCapabilities
        {
            get
            {
                return DefaultCacheCapabilities.None;
            }
        }

        public override string Name
        {
            get { return "Dummy"; }
        }

        public override object this[string key]
        {
            get { return null; }
            set {}
        }
    }
}
