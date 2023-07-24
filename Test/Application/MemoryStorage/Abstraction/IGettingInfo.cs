using System.Collections.Generic;
using System.Threading.Tasks;
using Test.Model;

namespace Test.Application
{
    public interface IGettingInfo
    {
        public Task<List<StateInfo>> GetInfoToMemoryAsync();
    }
}
