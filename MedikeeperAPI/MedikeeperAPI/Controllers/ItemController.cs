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
        private readonly IMemoryCache _cache;

        // In constructor, populate cache with seed data
        public ItemsController(ILogger<ItemsController> logger, IMemoryCache memoryCache)
        {
            _logger = logger;
            _cache = memoryCache;
        }

        [HttpGet]
        public IEnumerable<Item> Get()
        {
            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddHours(2),
                Priority = CacheItemPriority.Normal,
                SlidingExpiration = TimeSpan.FromMinutes(10)

            };

            // check if latest id in the cache
            if (!_cache.TryGetValue(Constants.LatestId, out int id))
            {
                _cache.Set(Constants.LatestId, Constants.SeedTotal, cacheOptions);
            }

            // check if items list in the cache
            if (!_cache.TryGetValue(Constants.ItemsKey, out List<Item> items))
            {
                var rng = new Random();
                List<Item> dataStore = Enumerable.Range(1, Constants.SeedTotal).Select(index => new Item
                {
                    Id = index,
                    Name = "Item " + index.ToString(),
                    Cost = rng.Next(50, 500)
                }).ToList();

                _cache.Set(Constants.ItemsKey, dataStore, cacheOptions);
            }

            return _cache.Get<List<Item>>(Constants.ItemsKey);
        }

        [HttpPost]
        public IActionResult Post(Item item)
        {
            System.Diagnostics.Debug.WriteLine("In POST http\n");
            System.Diagnostics.Debug.WriteLine(item.Name);
            System.Diagnostics.Debug.WriteLine(Constants.LatestId);

            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddHours(2),
                Priority = CacheItemPriority.Normal,
                SlidingExpiration = TimeSpan.FromMinutes(10)

            };

            // mimic a database's identity generator
            List<Item> dataStore = _cache.Get<List<Item>>("items");
            int id = _cache.Get<int>(Constants.LatestId);
            id += 1;    
            item.Id = id;
            dataStore.Add(item);
            _cache.Set(Constants.LatestId, id, cacheOptions);
            _cache.Set("items", dataStore, cacheOptions);
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Item item)
        {
            //find the item in the list with the item id
            List<Item> dataStore = _cache.Get<List<Item>>("items");
            int idx = dataStore.FindIndex(lambdaItem => lambdaItem.Id == id);
            dataStore[idx] = item;
            _cache.Set("items", dataStore);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            List<Item> dataStore = _cache.Get<List<Item>>("items");
            int idx = dataStore.FindIndex(lambdaItem => lambdaItem.Id == id);
            dataStore.RemoveAt(idx);
            _cache.Set("items", dataStore);
            return NoContent();
        }
    }
}
