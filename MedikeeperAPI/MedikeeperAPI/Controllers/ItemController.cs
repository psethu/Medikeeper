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

        // In constructor, populate cache with seed data
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

            _cache.Set("items", dataStore, TimeSpan.FromMinutes(30));
        }

        [HttpGet]
        public IEnumerable<Item> Get()
        {

            return _cache.Get<List<Item>>("items");
        }

        [HttpPost]
        public IActionResult Post(Item item)
        {
            System.Diagnostics.Debug.WriteLine("In POST http\n");
            System.Diagnostics.Debug.WriteLine(item.Name);

            // mimic a database's identity generator
            List<Item> dataStore = _cache.Get<List<Item>>("items");
            int len = dataStore.Count();
            int latest_id = _cache.Get<int>("latest_id");
            latest_id += 22;    
            item.Id = latest_id;
            dataStore.Add(item);
            _cache.Set("latest_id", latest_id, TimeSpan.FromMinutes(30));
            _cache.Set("items", dataStore, TimeSpan.FromMinutes(30));
            len = _cache.Get<List<Item>>("items").Count();
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
