using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Application.Query.Abstraction;
using Test.DTO;

namespace Test.Application.Query
{
    public class GetStatesInfoQuery:IGeneralQuery
    {
        private readonly IGettingInfo _gettingInfo;

        public GetStatesInfoQuery(IGettingInfo gettingInfo)
        {
            _gettingInfo = gettingInfo;
        }


        public async Task<List<StatesDTO>> GetOverallLevel()
        {
            var result = await _gettingInfo.GetInfoToMemoryAsync();
            return result.Select(x => new StatesDTO() { RiskLevel = x.riskLevels.overall, StateCode = x.fips }).ToList();
            
        }

        public async Task<List<StatesDTO>> GetPositivityTestLevel()
        {
            var result = await _gettingInfo.GetInfoToMemoryAsync();
            return result.Select(x => new StatesDTO() { RiskLevel = x.riskLevels.testPositivityRatio, StateCode = x.fips }).ToList();
        }

        public async Task<List<StatesDTO>> GetInfectionLevel()
        {
            var result = await _gettingInfo.GetInfoToMemoryAsync();
            return result.Select(x => new StatesDTO() { RiskLevel = x.riskLevels.infectionRate, StateCode = x.fips }).ToList();
        }

        public async Task<List<StatesDTO>> GetCasesPer100kLevel()
        {
            var result = await _gettingInfo.GetInfoToMemoryAsync();
            return result.Select(x => new StatesDTO() { RiskLevel = x.riskLevels.casesPer100kLevel, StateCode = x.fips }).ToList();
        }
    }
}

