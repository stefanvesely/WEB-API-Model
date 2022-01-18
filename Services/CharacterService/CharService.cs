using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using try5000rpg.Data;
using try5000rpg.DTOs.Characters;
using try5000rpg.Models;

namespace try5000rpg.Services.CharacterService
{
    public class CharService : iCharService
    {
        

        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        public CharService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _context = context;
            _HttpContextAccessor = httpContextAccessor;
        }

        private int GetUserID() => int.Parse(_HttpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceResponse<List<GetCharacterDTO>>> CreateSingle(AddCharacterDTO newSingle)
        {
            var srListService = new ServiceResponse<List<GetCharacterDTO>>();
            Characters NewCharacter = _mapper.Map<Characters>(newSingle);
            NewCharacter.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserID());
            _context.Characters.Add(NewCharacter);
            await _context.SaveChangesAsync();
            srListService.Data = await _context.Characters
                .Where(c => c.User.Id == GetUserID())
                .Select(c => _mapper.Map<GetCharacterDTO>(c)).ToListAsync();
            return srListService;
        }

        public async Task<ServiceResponse<List<GetCharacterDTO>>> DeleteCharacter(int iCharacterID)
        {
            var srResponse = new ServiceResponse<List<GetCharacterDTO>>();
            try
            {
                Characters chSingleCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == iCharacterID && c.User.Id == GetUserID());
                if (chSingleCharacter != null)
                {
                    _context.Characters.Remove(chSingleCharacter);
                    await _context.SaveChangesAsync();
                    srResponse.Data = _context.Characters
                        .Where(c => c.User.Id == GetUserID())
                        .Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
                }
                else
                {
                    srResponse.Success = false;
                    srResponse.Message = "No Character";
                }
                
            }
            catch (Exception ex)
            {
                srResponse.Success = false;
                srResponse.Message = ex.Message;
            }
            return srResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDTO>>> GetAllCharacters()
        {
            
            var srListService = new ServiceResponse<List<GetCharacterDTO>>();
            var dbCharacters = await _context.Characters
                .Include(c => c.Weapon)
                .Include(c => c.Skills)
                .Where(c => c.User.Id == GetUserID()).ToListAsync();
            srListService.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
            return srListService;
        }

        public async Task<ServiceResponse<GetCharacterDTO>> GetSingle(int id)
        {
            var srSingleCharacter = new ServiceResponse<GetCharacterDTO>();
            var drSingleCharacter = await _context.Characters
                .Include(c => c.Weapon)
                .Include(c => c.Skills)
                .FirstOrDefaultAsync(c => c.Id == id && c.User.Id == GetUserID());
            srSingleCharacter.Data = _mapper.Map<GetCharacterDTO>(drSingleCharacter);
            return srSingleCharacter;
        }

        public async Task<ServiceResponse<GetCharacterDTO>> UpdateCharacter(UpdateCharacterDTO updatedCharacter)
        {
            var srResponse = new ServiceResponse<GetCharacterDTO>();
            try
            {
                Characters chSingleCharacter = await _context.Characters
                    .Include(c => c.User)
                    .FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);
                if (chSingleCharacter.User.Id == GetUserID())
                {
                    chSingleCharacter.Name = updatedCharacter.Name;
                    chSingleCharacter.Class = updatedCharacter.Class;
                    chSingleCharacter.Defense = updatedCharacter.Defense;
                    chSingleCharacter.Intelligance = updatedCharacter.Intelligance;
                    chSingleCharacter.Strength = updatedCharacter.Strength;
                    await _context.SaveChangesAsync();
                    srResponse.Data = _mapper.Map<GetCharacterDTO>(chSingleCharacter);
                }
                else
                {
                    srResponse.Success = false;
                    srResponse.Message = "no character";
                }
            }
            catch (Exception ex)
            {
                srResponse.Success = false;
                srResponse.Message = ex.Message;
            }
            return srResponse;
        }

        public async Task<ServiceResponse<GetCharacterDTO>> AddCharacterSkill(AddCharacterSkillDTO characterSkill)
        {
            var response = new ServiceResponse<GetCharacterDTO>();
            try
            {
                var character = await _context.Characters
                    .Include(c=> c.Weapon)
                    .Include(c=> c.Skills)
                    .FirstOrDefaultAsync(c => c.Id == characterSkill.CharacterId && c.User.Id == GetUserID());
                if (character == null)
                {
                    response.Success = false;
                    response.Message = "No Character";
                    return response;
                }
                var skill = await _context.Skills.FirstOrDefaultAsync(s => s.Id == characterSkill.SkillId);
                if (skill == null)
                {
                    response.Success = false;
                    response.Message = "No skillz";
                    return response;
                }
                character.Skills.Add(skill);
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetCharacterDTO>(character);

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}