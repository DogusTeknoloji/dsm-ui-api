﻿using System.Linq;
using System.Threading.Tasks;
using DSM.UI.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DSM.UI.Api.Controllers
{
    public class CustomController : Controller
    {
        private readonly ICustomService _customService;

        public CustomController(ICustomService customService)
        {
            _customService = customService;
        }

        [HttpGet]
        [Route("getSentries/{pageNumber}")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public async Task<IActionResult> GetAllSentries(int pageNumber)
        {
            var result = await _customService.GetSentryListItemsAsync(pageNumber);

            if (!result.Any())
                return NotFound("No sentries found, please check excel file.");

            return Ok(result.Reverse());
        }

        [HttpGet]
        [Route("getSentry/{id}")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public async Task<IActionResult> GetSentry(int id)
        {
            var result = await _customService.GetSentryListItemAsync(id);
            
            if (result == null)
                return NotFound("Sentry not found, please check excel file.");
            
            return Ok(result);
        }

        [HttpGet]
        [Route("getTodaySentry")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public async Task<IActionResult> GetTodaySentry()
        {
            var result = await _customService.GetTodaySentryAsync();

            if (result == null)
                return NotFound("No sentry for today, please check excel file.");

            return Ok(result);
        }
        
        [HttpGet]
        [Route("getNextWeeksSentry")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public async Task<IActionResult> GetNextWeeksSentry()
        {
            var result = await _customService.GetNextWeeksSentryAsync();

            if (result == null)
                return NotFound("No sentry for today, please check excel file.");

            return Ok(result);
        }
        
        [HttpGet]
        [Route("getSentryByMonthRange/")]
        [Authorize(Roles = "Member, Spectator, Manager, Administrator, CIFANG")]
        public async Task<IActionResult> GetSentryWithTimeRangeAsync(int monthRange = 1)
        {
            var result = await _customService.GetSentryWithTimeRangeAsync(monthRange);

            if (result == null)
                return NotFound("No sentry for today, please check excel file.");

            return Ok(result);
        }
    }
}