using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using try5000rpg.DTOs.Characters;
using try5000rpg.DTOs.Weapons;
using try5000rpg.Models;

namespace try5000rpg.Services.WeaponService
{
    public interface IWeaponService
    {
        Task<ServiceResponse<GetCharacterDTO>> AddWeapon(AddWeaponDTO newWeapon);
    }
}
