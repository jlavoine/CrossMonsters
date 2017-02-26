using MyLibrary;
using System.Collections.Generic;

namespace CrossMonsters {
    public interface IMonsterPM : IPresentationModel {
        List<int> AttackCombo { get; }
    }
}
