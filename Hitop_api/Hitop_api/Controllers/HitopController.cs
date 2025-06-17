using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using hitop.model;
using hitop.app.service;
using Microsoft.Extensions.Caching.Memory;

namespace hitop.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]

    public class HitopController : ControllerBase
    {
        public readonly IConfiguration _configuration;
        private readonly IHitopService sv;
        private readonly IMemoryCache _cache;
        public HitopController(IHitopService AppService, IConfiguration configuration, IMemoryCache cache)
        {
            sv = AppService;
            _configuration = configuration;
            _cache = cache;
        }


        [HttpGet]
        public async Task<string> Test()
        {
            string ien = null;
            ien = await sv.Test();
            if (ien == null && !string.IsNullOrEmpty(sv.getErrorMessage()))
            {
                throw new ApplicationException(sv.getErrorMessage());
            }
            return ien;
        }


        [HttpGet]
        public async Task<IEnumerable<productModel>> GetProduct(string search = null)
        {
            string cacheKey = $"product_search_{search ?? "all"}";

            if (!_cache.TryGetValue(cacheKey, out IEnumerable<productModel> cachedResult))
            {
                cachedResult = await sv.GetProduct(search);

                if (cachedResult == null && !string.IsNullOrEmpty(sv.getErrorMessage()))
                {
                    throw new ApplicationException(sv.getErrorMessage());
                }

                // Cache for result within 10 min
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(10));

                _cache.Set(cacheKey, cachedResult, cacheOptions);
            }

            return cachedResult;
        }


        [HttpGet]
        public async Task<IEnumerable<productModel>> GetProductDatabase(string search = null)
        {
            string cacheKey = $"product_search_{search ?? "all"}";

            if (!_cache.TryGetValue(cacheKey, out IEnumerable<productModel> cachedResult))
            {
                cachedResult = await sv.GetProductDatabase(search);

                if (cachedResult == null && !string.IsNullOrEmpty(sv.getErrorMessage()))
                {
                    throw new ApplicationException(sv.getErrorMessage());
                }

                // Cache for result within 10 min
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(10));

                _cache.Set(cacheKey, cachedResult, cacheOptions);
            }

            return cachedResult;
        }

    }
}
