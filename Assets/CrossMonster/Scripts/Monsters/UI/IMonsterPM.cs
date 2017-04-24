using MyLibrary;
using System.Collections.Generic;

namespace MonsterMatch {
    public interface IMonsterPM : IPresentationModel {
        List<int> AttackCombo { get; }
    }
}
