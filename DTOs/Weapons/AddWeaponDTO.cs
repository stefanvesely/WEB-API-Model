﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace try5000rpg.DTOs.Weapons
{
    public class AddWeaponDTO
    {
        public string Name { get; set; }
        public int Damage { get; set; }
        public int CharacterId { get; set; }
    }
}
