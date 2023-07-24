using System.Collections.Generic;
using System.Threading.Tasks;
using Test.DTO;

namespace Test.Application.Query.Abstraction
{
    public interface IPositivityTestLevelQuery
    {
        public Task<List<StatesDTO>> GetPositivityTestLevel();
    }
}
