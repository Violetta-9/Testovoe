using System.Collections.Generic;
using System.Threading.Tasks;
using Test.DTO;

namespace Test.Application.Query.Abstraction
{
    public interface ICasesPer100kLevelQuery
    {
        public Task<List<StatesDTO>> GetCasesPer100kLevel();
    }
}
