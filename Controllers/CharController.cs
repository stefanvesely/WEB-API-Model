using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using try5000rpg.DTOs.Characters;
using try5000rpg.Models;
using try5000rpg.Services.CharacterService;

namespace try5000rpg.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CharController : ControllerBase
    {
        private readonly iCharService _charService;

        public CharController(iCharService charService)
        {
            _charService = charService;
        }

        [AllowAnonymous]
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDTO>>>> Get()
        {
            return Ok(await _charService.GetAllCharacters());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDTO>>> GetSingle(int id)
        {
            return Ok(await _charService.GetSingle(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDTO>>>> AddChar(AddCharacterDTO newCharacter)
        {
            return Ok(await _charService.CreateSingle(newCharacter));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetCharacterDTO>>> UpdateChar(UpdateCharacterDTO updateCharacter)
        {
            var srResponse = await _charService.UpdateCharacter(updateCharacter);
            if (srResponse.Data == null)
            {
                return NotFound(srResponse);
            }
            return Ok(srResponse);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDTO>>>> Delete(int id)
        {
            var srResponse = await _charService.DeleteCharacter(id);
            if (srResponse.Data == null)
            {
                return NotFound(srResponse);
            }
            return Ok(srResponse);
        }
        [HttpPost("Skill")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDTO>>> AddCharacterSkill (AddCharacterSkillDTO newCharacterSkill)
        {
            return Ok(await _charService.AddCharacterSkill(newCharacterSkill));
        }
    }
}