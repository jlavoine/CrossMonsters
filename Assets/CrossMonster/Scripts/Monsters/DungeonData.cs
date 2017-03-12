﻿using System.Collections.Generic;

namespace CrossMonsters {
    public class DungeonData {
        public string Id;
        public string AreaId;

        public List<string> MonsterGroups;
        public Dictionary<string, float> MonsterGroupBoosts;
    }
}