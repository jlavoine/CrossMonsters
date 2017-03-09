using MyLibrary;
using System.Collections.Generic;

namespace CrossMonsters {
    public interface ITreasureSetPM : IPresentationModel {
        List<ITreasurePM> TreasurePMs { get; }
    }
}
