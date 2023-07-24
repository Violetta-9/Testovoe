using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.Application;
using Test.Application.Query;
using Test.Application.Query.Abstraction;
using Test.DTO;

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatesController : ControllerBase
    {
     
        private readonly GetStatesInfoQuery _query;

        public StatesController(GetStatesInfoQuery query)
        {
            
            _query= query;
        }
        [HttpGet("infection")]
        [SwaggerOperation(Summary = "Get Infection level", OperationId = "GetInfectionLevel")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(List<StatesDTO>))]
        public async Task<ActionResult> GetInfectionLevel()
        {
           
           var result=await _query.GetInfectionLevel();
           return Ok(result);
        }
        [HttpGet("overall")]
        [SwaggerOperation(Summary = "Get Overall level", OperationId = "GetOverallLevel")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(List<StatesDTO>))]
        public async Task<ActionResult> GetOverallLevel()
        {

            var result = await _query.GetOverallLevel();
            return Ok(result);
        }
        [HttpGet("positivityTest")]
        [SwaggerOperation(Summary = "Get positivity test level", OperationId = "GetPositivityTestLevel")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(List<StatesDTO>))]
        public async Task<ActionResult> GetPositivityTestLevel()
        {

            var result = await _query.GetPositivityTestLevel();
            return Ok(result);
        }
        [HttpGet("Per100k")]
        [SwaggerOperation(Summary = "Get cases Per 100k level", OperationId = "GetCasesPer100kLevel")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(List<StatesDTO>))]
        public async Task<ActionResult> GetCasePer100kLevel()
        {
            var result = await _query.GetCasesPer100kLevel();
            return Ok(result);
        }
    }
}
