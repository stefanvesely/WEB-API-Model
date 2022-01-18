using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public FightService(DataContext context)
        {
            _context = context;
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
                int damage = attacker.Weapon.Damage + (new Random().Next(attacker.Strength));
                damage -= new Random().Next(defender.Defense);
                if (damage > 0)
                {
                    defender.HitPoints -= damage;
                }
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
    }
}
