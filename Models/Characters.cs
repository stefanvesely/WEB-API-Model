using System.Collections.Generic;

namespace try5000rpg.Models
{
    public class Characters
    {
        public int Id { get; set; }
        public string Name { get; set; }= "steff";

        public int HitPoints { get; set; } = 100;

        public int Strength { get; set; } = 10;

        public int Defense { get; set; } = 10;

        public int Intelligance { get; set; } = 10;
        public CharClass Class { get; set; } = CharClass.knight;
        public User User { get; set; }
        public Weapon Weapon { get; set; }
        public List<Skills> Skills { get; set; }
    }
}