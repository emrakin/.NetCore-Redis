using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreRedis.Models;
using NetCoreRedis.RedisRepository;
using Newtonsoft.Json;
using ServiceStack.Redis;

namespace NetCoreRedis.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRedisService _redisService;

        public HomeController(IRedisService redisService)
        {
            _redisService = redisService;

            using (IRedisClient client = new RedisClient())
            {
                if (client.SearchKeys("Person*").Count == 0)
                {
                    var personvalue = new Person();
                    personvalue.ID = 1;
                    personvalue.Name = "Emrah";
                    personvalue.Surname = "Akın";
                    personvalue.Age = 40;

                    var cachedata = client.As<Person>();
                    cachedata.SetValue("Person"+personvalue.ID, personvalue);

                }
            }
        }

        public IActionResult Index()
        {
            const string cacheKey = "Person*";
            var redisdata = _redisService.GetAll(cacheKey);

            return View(redisdata);
        }

        public IActionResult Detail(int Id)
        {
            string cacheKey = "Person"+Id;
            var redisdata = _redisService.GetById(cacheKey);

            return View(redisdata);
        }
    }
}