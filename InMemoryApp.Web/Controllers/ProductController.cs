﻿using InMemoryApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemoryApp.Web.Controllers
{
	public class ProductController : Controller
	{
		private readonly IMemoryCache _memoryCache;

		public ProductController(IMemoryCache memoryCache)
		{
			_memoryCache = memoryCache;
		}

		public IActionResult Index()
		{
			// 1.Yol
			//_memoryCache.Set<string>("zaman",DateTime.Now.ToString());

			//if (String.IsNullOrEmpty(_memoryCache.Get<string>("zaman")))
			//{
            //   _memoryCache.Set<string>("zaman", DateTime.Now.ToString());
            //}

			// 2.Yol
			//if(!_memoryCache.TryGetValue("zaman",out string zamancache))
			//{
			//	_memoryCache.Set<string>("zaman", DateTime.Now.ToString());
			//}

			MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();

            //  options.SlidingExpiration = TimeSpan.FromSeconds(10);
            options.AbsoluteExpiration = DateTime.Now.AddSeconds(10);

			options.Priority = CacheItemPriority.High;

			options.RegisterPostEvictionCallback((key, value, reason, state) =>
			{
				_memoryCache.Set("callback", $"{key} ->{value} => sebep:{reason}");
			});

			_memoryCache.Set<string>("zaman", DateTime.Now.ToString(), options);

			Product p = new Product()
			{
				Id = 1,
				Name = "Kalem",
				Price = 200
			};

			_memoryCache.Set<Product>("product:1", p);
			_memoryCache.Set<double>("money", 100.99);

			return View();
		}

		public IActionResult Show()
		{
			// 1.Yol
			//ViewBag.zaman = _memoryCache.Get<string>("zaman");
			//_memoryCache.Remove("zaman");

			// 2.Yol
			//_memoryCache.GetOrCreate<string>("zaman", entry =>
			//{
			//	return DateTime.Now.ToString();
			//});

			//ViewBag.zaman = _memoryCache.Get<string>("zaman");

			_memoryCache.TryGetValue("zaman", out string zamancache);
			_memoryCache.TryGetValue("callback", out string callback);
			ViewBag.zaman = zamancache;
			ViewBag.callback = callback;

			ViewBag.product = _memoryCache.Get<Product>("product:1");


			return View();
		}
	}
}
