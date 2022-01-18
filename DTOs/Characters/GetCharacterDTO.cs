using System.Collections.Generic;
using try5000rpg.DTOs.Skills;
using try5000rpg.DTOs.Weapons;
using try5000rpg.Models; 
namespace try5000rpg.DTOs.Characters
{
    public class GetCharacterDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = "steff";

        public int HitPoints { get; set; } = 100;

        public int Strength { get; set; } = 10;

        public int Defense { get; set; } = 10;

        public int Intelligance { get; set; } = 10;
        public CharClass Class { get; set; } = CharClass.knight;

        public GetWeaponDTO Weapon {get;set;}
        public List<GetSkillDTO> Skills { get; set; }

        public int Fights { get; set; }
        public int Victories { get; set; }
        public int Defeats { get; set; }
    }
}