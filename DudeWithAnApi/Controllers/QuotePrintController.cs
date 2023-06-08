using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DudeWithAnApi.Models;
using DudeWithAnApi.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using DudeWithAnApi.ResponseDOs;
using Microsoft.AspNetCore.Authorization;

namespace DudeWithAnApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class QuotePrintController : ControllerBase
    {
        private readonly IQuotePrintService _quotePrintService;

        public QuotePrintController(IQuotePrintService quoteprintService)
        {
            _quotePrintService = quoteprintService;
        }

        // GET: api/Quote/printsByDay
        [HttpGet("printsByDay")]
        public async Task<ActionResult<IEnumerable<QuotePrintByDay>>> GetQuotePrintsByDay(int year, int month)
        {
            return Ok(await _quotePrintService.GetQuotePrintsByDay(year, month));
        }

        // GET: api/Quote/printsByMonth
        [HttpGet("printsByMonth")]
        public async Task<ActionResult<IEnumerable<QuotePrintByMonth>>> GetQuotePrintsByMonth(int year)
        {
            return Ok(await _quotePrintService.GetQuotePrintsByMonth(year));
        }


    }
}
