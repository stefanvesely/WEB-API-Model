using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using try5000rpg.Data;
using try5000rpg.DTOs.Fights;
using try5000rpg.Models;

namespace try5000rpg.Services.FightService
{
    public class FightService : IFightService 
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public FightService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<ServiceResponse<AttackResultsDTO>> WeaponAttack(WeaponAttackDTO request)
        {
            ServiceResponse<AttackResultsDTO> response = new ServiceResponse<AttackResultsDTO>();
            try
            {
                Characters attacker = await _context.Characters
                    .Include(c => c.Weapon)
                    .FirstOrDefaultAsync(c => c.Id == request.AttackerID);
                Characters defender = await _context.Characters.FirstOrDefaultAsync(o => o.Id == request.OpponentID);
                int damage = DoWeaponAttack(attacker, defender);
                if (defender.HitPoints <= 0)
                {
                    response.Message = $"{defender.Name} is dead son";
                }
                _context.Characters.Update(defender);
                await _context.SaveChangesAsync();
                response.Data = new AttackResultsDTO
                {
                    Attacker = attacker.Name,
                    AttackerHP = attacker.HitPoints,
                    Opponent = defender.Name,
                    OpponentHP = defender.HitPoints,
                    Damage = damage
                };
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        private static int DoWeaponAttack(Characters attacker, Characters defender)
        {
            int damage = attacker.Weapon.Damage + (new Random().Next(attacker.Strength));
            damage -= new Random().Next(defender.Defense);
            if (damage > 0)
            {
                defender.HitPoints -= damage;
            }

            return damage;
        }

        public async Task<ServiceResponse<AttackResultsDTO>> SkillAttack(SkillAttackDTO request)
        {
            ServiceResponse<AttackResultsDTO> response = new ServiceResponse<AttackResultsDTO>();
            try
            {
                Characters attacker = await _context.Characters
                    .Include(c => c.Skills)
                    .FirstOrDefaultAsync(c => c.Id == request.AttackerId);
                Characters defender = await _context.Characters.FirstOrDefaultAsync(o => o.Id == request.OpponentId);
                var skill = attacker.Skills.FirstOrDefault(s => s.Id == request.SkillId);
                if (skill == null)
                {
                    response.Success = false;
                    response.Message = "Sattacker dumb yo, he dont know dis";
                }
                int damage = doskillattack(attacker, defender, skill);
                if (defender.HitPoints <= 0)
                {
                    response.Message = $"{defender.Name} is dead son";
                }
                _context.Characters.Update(defender);
                await _context.SaveChangesAsync();
                response.Data = new AttackResultsDTO
                {
                    Attacker = attacker.Name,
                    AttackerHP = attacker.HitPoints,
                    Opponent = defender.Name,
                    OpponentHP = defender.HitPoints,
                    Damage = damage
                };
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        private static int doskillattack(Characters attacker, Characters defender, Skills skill)
        {
            int damage = skill.Damage + (new Random().Next(attacker.Intelligance));
            damage -= new Random().Next(defender.Defense);
            if (damage > 0)
            {
                defender.HitPoints -= damage;
            }

            return damage;
        }

        
        

        public async Task<ServiceResponse<List<HighScoreDTO>>> GetHighScore()
        {
            var characters = await _context.Characters
                 .Where(c => c.Fights > 0)
                 .OrderByDescending(c => c.Victories)
                 .ThenBy(c => c.Defeats)
                 .ToListAsync();

            var response = new ServiceResponse<List<HighScoreDTO>>
            {
                Data = characters.Select(c => _mapper.Map<HighScoreDTO>(c)).ToList()
            };
            return response;
        }

        public async Task<ServiceResponse<FightResultDTO>> Fight(FightRequestDTO reqest)
        {
            var serviceresponse = new ServiceResponse<FightResultDTO>
            {
                Data = new FightResultDTO()
            };
            try
            {
                var characters = await _context.Characters
                    .Include(c => c.Weapon)
                    .Include(c => c.Skills)
                    .Where(c => reqest.CharacterIds.Contains(c.Id)).ToListAsync();
                bool defeated = false;
                while (!defeated)
                {
                    foreach (var attacher in characters)
                    {
                        var opponents = characters.Where(c => c.Id != attacher.Id).ToList();
                        var opponent = opponents[new Random().Next(opponents.Count)];
                        int damage = 0;
                        string attackused = string.Empty;
                        bool useweapon = new Random().Next(2) == 0;
                        if (useweapon)
                        {
                            attackused = attacher.Weapon.Name;
                            damage = DoWeaponAttack(attacher, opponent);
                        }
                        else
                        {
                            var skill = attacher.Skills[new Random().Next(attacher.Skills.Count)];
                            attackused = skill.Name;
                            damage = doskillattack(attacher, opponent, skill);
                        }
                        serviceresponse.Data.log.Add($"{attacher.Name} attacked {opponent.Name} using {attackused} with {(damage >= 0 ? damage : 0)} damage.");
                        if (opponent.HitPoints <= 0)
                        {
                            attacher.Victories++;
                            opponent.Defeats++;
                            defeated = true;
                            serviceresponse.Data.log.Add($"{opponent.Name} has been defeated by {attacher.Name} and there is {attacher.HitPoints} hit points left.");
                            break;
                        }
                    }
                }
                characters.ForEach(c =>
                {
                    c.Fights++;
                    c.HitPoints = 200;
                }
                );
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                serviceresponse.Success = false;
                serviceresponse.Message = ex.Message;
            }
            return serviceresponse;
        }
    }
}
