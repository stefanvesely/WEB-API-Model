using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using try5000rpg.DTOs.Fights;
using try5000rpg.Models;
using try5000rpg.Services.FightService;

namespace try5000rpg.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FightController : ControllerBase
    {
        private readonly IFightService _fightservice;

        public FightController (IFightService fightservice)
        {
            _fightservice = fightservice;
        }
        [HttpPost("Weapon")]
        public async Task<ActionResult<ServiceResponse<AttackResultsDTO>>> WeaponAttack(WeaponAttackDTO request)
        {
            return Ok(await _fightservice.WeaponAttack(request));
        }
        [HttpPost("Skill")]
        public async Task<ActionResult<ServiceResponse<AttackResultsDTO>>> SkillAttack(SkillAttackDTO request)
        {
            return Ok(await _fightservice.SkillAttack(request));
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<FightResultDTO>>> Fight(FightRequestDTO request)
        {
            return Ok(await _fightservice.Fight(request));
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<HighScoreDTO>>> GetHighscore()
        {
            return Ok(await _fightservice.GetHighScore());
        }
    }
}
