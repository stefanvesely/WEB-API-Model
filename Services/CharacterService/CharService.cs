using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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

        public CharService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<List<GetCharacterDTO>>> CreateSingle(AddCharacterDTO newSingle)
        {
            var srListService = new ServiceResponse<List<GetCharacterDTO>>();
            Characters NewCharacter = _mapper.Map<Characters>(newSingle);
            _context.Characters.Add(NewCharacter);
            await _context.SaveChangesAsync();
            srListService.Data = await _context.Characters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToListAsync();
            return srListService;
        }

        public async Task<ServiceResponse<List<GetCharacterDTO>>> DeleteCharacter(int iCharacterID)
        {
            var srResponse = new ServiceResponse<List<GetCharacterDTO>>();
            try
            {
                Characters chSingleCharacter = await _context.Characters.FirstAsync(c => c.Id == iCharacterID);
                _context.Characters.Remove(chSingleCharacter);
                await _context.SaveChangesAsync();
                srResponse.Data = _context.Characters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
                
            }
            catch (Exception ex)
            {
                srResponse.Success = false;
                srResponse.Message = ex.Message;
            }
            return srResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDTO>>> GetAllCharacters(int UserId)
        {
            var srListService = new ServiceResponse<List<GetCharacterDTO>>();
            var dbCharacters = await _context.Characters.Where(c => c.User.Id == UserId).ToListAsync();
            srListService.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
            return srListService;
        }

        public async Task<ServiceResponse<GetCharacterDTO>> GetSingle(int id)
        {
            var srSingleCharacter = new ServiceResponse<GetCharacterDTO>();
            var drSingleCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
            srSingleCharacter.Data = _mapper.Map<GetCharacterDTO>(drSingleCharacter);
            return srSingleCharacter;
        }

        public async Task<ServiceResponse<GetCharacterDTO>> UpdateCharacter(UpdateCharacterDTO updatedCharacter)
        {
            var srResponse = new ServiceResponse<GetCharacterDTO>();
            try
            {
                Characters chSingleCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);
                chSingleCharacter.Name = updatedCharacter.Name;
                chSingleCharacter.Class = updatedCharacter.Class;
                chSingleCharacter.Defense = updatedCharacter.Defense;
                chSingleCharacter.Intelligance = updatedCharacter.Intelligance;
                chSingleCharacter.Strength = updatedCharacter.Strength;
                await _context.SaveChangesAsync();
                srResponse.Data = _mapper.Map<GetCharacterDTO>(chSingleCharacter);
            }
            catch (Exception ex)
            {
                srResponse.Success = false;
                srResponse.Message = ex.Message;
            }
            return srResponse;
        }
    }
}