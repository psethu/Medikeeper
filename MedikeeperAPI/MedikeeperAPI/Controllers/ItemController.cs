using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace MedikeeperAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {
        
        private readonly ILogger<ItemsController> _logger;
        private IMemoryCache _cache;

        public ItemsController(ILogger<ItemsController> logger, IMemoryCache memoryCache)
        {
            _logger = logger;
            _cache = memoryCache;
            int SeedTotal = 7;

            _cache.Set("latest_id", SeedTotal);
            
            var rng = new Random();
            List<Item> dataStore = Enumerable.Range(1, SeedTotal).Select(index => new Item
            {
                Id = index,
                Name = "Item " + index.ToString(),
                Cost = rng.Next(50, 500)
            }).ToList();

            _cache.Set("items", dataStore);
        }

        [HttpGet]
        public IEnumerable<Item> Get()
        {
              
            return _cache.Get<List<Item>>("items");
        }

        [HttpPost]
        public IEnumerable<Item> Post(Item item)
        {
            List<Item> dataStore = _cache.Get<List<Item>>("items");
            dataStore.Add(item);
            return Enumerable.Empty<Item>().ToArray();
        }
    }
}
