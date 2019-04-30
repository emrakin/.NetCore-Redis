using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCoreRedis.Models;
using ServiceStack.Redis;

namespace NetCoreRedis.RedisRepository
{
    public class RedisManager : IRedisService
    {
        public List<Person> GetAll(string cachekey)
        {
            using (IRedisClient client = new RedisClient())
            {
                List<Person> dataList = new List<Person>();
                List<string> allKeys = client.SearchKeys(cachekey);
                foreach (string key in allKeys)
                {
                    dataList.Add(client.Get<Person>(key));
                }
                return dataList;
            }
        }

        public Person GetById(string cachekey)
        {
            using (IRedisClient client = new RedisClient())
            {
                var redisdata = client.Get<Person>(cachekey);

                return redisdata;
            }
        }
    }
}
