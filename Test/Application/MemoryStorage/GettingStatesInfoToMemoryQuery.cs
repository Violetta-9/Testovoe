using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Test.Model;
using Microsoft.Extensions.Caching.Memory;

namespace Test.Application
{
    public class GettingStatesInfoToMemoryQuery : IGettingInfo
    {
        private const string statesListCacheKey = "statesList";
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;

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
                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<StateInfo>>(responseString);
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                        .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                        .SetPriority(CacheItemPriority.Normal);
                    _cache.Set(statesListCacheKey, result, cacheEntryOptions);
                    return result;
                }

                throw new Exception($"Status code is {response.EnsureSuccessStatusCode()}");
            }

        }
    }
}