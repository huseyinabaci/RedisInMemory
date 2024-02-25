using IDistributedCacheRedisApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace IDistributedCacheRedisApp.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IDistributedCache _distributedCache;

        public ProductController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<IActionResult> Index()
        {
            DistributedCacheEntryOptions cacheEntryOptions = new DistributedCacheEntryOptions();

            //_distributedCache.SetString("name", "Raftello", cacheEntryOptions);

            //await _distributedCache.SetStringAsync("surname", "KetalMetal");

            cacheEntryOptions.AbsoluteExpiration = DateTime.Now.AddMinutes(30);

            Product product = new Product()
            {
                Id = 1,
                Name = "Test",
                Price = 100,
            };

            string jsonproduct = JsonConvert.SerializeObject(product);

            //await _distributedCache.SetStringAsync("product:1",jsonproduct, cacheEntryOptions);

            Byte[] byteproduct = Encoding.UTF8.GetBytes(jsonproduct);

            _distributedCache.Set("product:1", byteproduct);


            return View();
        }

        public IActionResult Show()
        {
            //string name = _distributedCache.GetString("name");

            //ViewBag.Name = name;

            //string jsonproduct = _distributedCache.GetString("product:1");

            Byte[] byteproduct = _distributedCache.Get("product:1");

            string jsonproduct = Encoding.UTF8.GetString(byteproduct);

            Product p = JsonConvert.DeserializeObject<Product>(jsonproduct);

            ViewBag.Product = p;

            return View();
        }

        public IActionResult Remove()
        {
            _distributedCache?.Remove("name");
            return View();
        }

        public IActionResult ImageUrl()
        {
            byte[] resimbyte = _distributedCache.Get("resim");

            return File(resimbyte,"image/jpg");
        }

        public IActionResult ImageCache()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/download.jpg");


            byte[] imageByte = System.IO.File.ReadAllBytes(path);

            _distributedCache.Set("resim", imageByte);

            return View();
        }
    }
}
