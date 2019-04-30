using NetCoreRedis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreRedis.RedisRepository
{
    public interface IRedisService
    {
        List<Person> GetAll(string cachekey);
        Person GetById(string cachekey);
    }
}
