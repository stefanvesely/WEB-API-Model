using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using try5000rpg.DTOs.Characters;
using try5000rpg.DTOs.Weapons;
using try5000rpg.Models;
using try5000rpg.Services.WeaponService;

namespace try5000rpg.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeaponController : ControllerBase
    {
        private readonly IWeaponService _weaponService;

        public WeaponController (IWeaponService weaponService)
        {
            _weaponService = weaponService;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetCharacterDTO>>> AddWeapon (AddWeaponDTO newWeapon)
        {
            return Ok(await _weaponService.AddWeapon(newWeapon));
        }
    }
}
