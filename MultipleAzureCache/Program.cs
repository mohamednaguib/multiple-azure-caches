using Microsoft.ApplicationServer.Caching;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleAzureCache
{
    public class Program
    {
        public static DataCache GetCache(string cacheName,string cacheKey)
        {
            DataCacheAutoDiscoverProperty p = new DataCacheAutoDiscoverProperty(true, cacheName);

            DataCacheFactoryConfiguration factoryConfig = new DataCacheFactoryConfiguration();
            factoryConfig.SecurityProperties = new DataCacheSecurity(cacheKey, false);

            factoryConfig.AutoDiscoverProperty = p;

            DataCacheFactory mycacheFactory = new DataCacheFactory(factoryConfig);
            return mycacheFactory.GetCache("default");
        }

        public static void AddtoCache(string cacheName, string cacheKey, string key, string value)
        {
            DataCache cache = GetCache(cacheName, cacheKey);
            cache.Put(key, value);
        }

        public static string GetValueFromCache(string cacheName, string cacheKey, string key)
        {
            DataCache cache = GetCache(cacheName, cacheKey);
            return cache.Get(key).ToString();
        }
        public static void Main(string[] args)
        {
            string cache1_name = ConfigurationManager.AppSettings["Azure_Cache_Name_cache1"];
            string cache1_key = ConfigurationManager.AppSettings["Azure_Cache_Key_cache1"];
            string cache2_name = ConfigurationManager.AppSettings["Azure_Cache_Name_cache2"];
            string cache2_key = ConfigurationManager.AppSettings["Azure_Cache_Key_cache2"];

            string key_cache1 = "key_cache1";
            string value_cache1 = "value_in_cache1";

            string key_cache2 = "key_cache2";
            string value_cache2 = "value_in_cache2";

            // save value to first cache 
            AddtoCache(cache1_name, cache1_key,key_cache1 ,value_cache1 );
            // save value to second cache 
            AddtoCache(cache2_name, cache2_key, key_cache2,value_cache2 );

            // get value from first cache
            string cache1_val = GetValueFromCache(cache1_name, cache1_key, key_cache1);

            // get value from second cache
            string cache2_val = GetValueFromCache(cache2_name, cache2_key, key_cache2);

            Console.WriteLine("Cache 1 val: " + cache1_val);
            Console.WriteLine("Cache 2 val: " + cache2_val);
        }
    }
}
