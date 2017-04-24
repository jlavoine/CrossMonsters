using MyLibrary;
using System.Collections.Generic;

namespace MonsterMatch {
    public interface ITreasureSetPM : IPresentationModel {
        List<ITreasurePM> TreasurePMs { get; }
    }
}
