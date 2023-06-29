using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DudeWithAnApi.Models;
using DudeWithAnApi.ResponseDOs;
using Microsoft.AspNetCore.Authorization;
using DudeWithAnApi.Repositories;

namespace DudeWithAnApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class AppTranslationController : ControllerBase
    {
        private readonly IRepository<AppTranslation> _appTranslationRepository;

        public AppTranslationController(IRepository<AppTranslation> appTranslationRepository)
        {
            _appTranslationRepository = appTranslationRepository;
        }

        // GET: api/Quote/printsByDay
        [HttpGet]
        public async Task<ActionResult<AppTranslation>> GetAppTranslation(string language)
        {
            var translation = await _appTranslationRepository.FindAsync(t => t.LanguageCode == language);
            if (translation.Count() == 0)
            {
                translation = await _appTranslationRepository.FindAsync(t => t.LanguageCode == "en");
            }
            return Ok(translation.FirstOrDefault());
        }

    }
}
