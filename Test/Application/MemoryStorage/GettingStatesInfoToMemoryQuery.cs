using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Threading.Tasks;
using Test.Model;
using Microsoft.Extensions.Caching.Memory;
using Test.Resources;

namespace Test.Application
{
    public class GettingStatesInfoToMemoryQuery : IGettingInfo
    {
        private const string statesListCacheKey = "statesList";
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        private const int cacheSlidingExpirationSeconds = 60;
        private const int cacheAbsoluteExpirationSeconds = 3600;

        public GettingStatesInfoToMemoryQuery(HttpClient httpClient,IMemoryCache cache)
        {
            _httpClient = httpClient;
            _cache = cache;
        }

        public async Task<List<StateInfo>> GetInfoToMemoryAsync()
        {
            if (_cache.TryGetValue(statesListCacheKey, out List<StateInfo> states))
            {
                return states;
            }

            using (var response = await _httpClient.GetAsync(""))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(String.Format(Messages.StatusCode,response.StatusCode));
                }
                var responseString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<StateInfo>>(responseString);
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(cacheSlidingExpirationSeconds))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(cacheAbsoluteExpirationSeconds))
                    .SetPriority(CacheItemPriority.Normal);
                _cache.Set(statesListCacheKey, result, cacheEntryOptions);
                return result;
                
            }

        }
    }
}