using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartEnergy.Contract.Interfaces;

namespace SmartEnergyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {

        private readonly ISettingsService _settingService;
        private readonly IIconService _iconService;

        public SettingsController(ISettingsService settingService, IIconService iconService)
        {
            _settingService = settingService;
            _iconService = iconService;
        }

        [HttpGet("last")]     
        public IActionResult GetLastSettings()
        {
            return Ok(_settingService.GetLastSettings());
           
        }


        [HttpGet("default")]
        public IActionResult GetDefaultSettings()
        {
            return Ok(_settingService.GetDefaultSettings());

        }




    }
}
