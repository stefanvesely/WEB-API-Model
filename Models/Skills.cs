using System.Collections.Generic;

namespace try5000rpg.Models
{
    public class Skills
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Damage { get; set; }
        public List<Characters> Characters { get; set; }
    }
}