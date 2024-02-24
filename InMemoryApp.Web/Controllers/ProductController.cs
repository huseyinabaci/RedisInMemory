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

            return View();
		}
	}
}
