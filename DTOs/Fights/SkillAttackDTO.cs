﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace try5000rpg.DTOs.Fights
{
    public class SkillAttackDTO
    {
        public int AttackerId { get; set; }
        public int OpponentId { get; set; }
        public int SkillId { get; set; }
    }
}
