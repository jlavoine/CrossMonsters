using MyLibrary;
using System.Collections.Generic;

namespace MonsterMatch {
    public interface ITimedChestsMainPM : IBasicWindowPM {
        List<ITimedChestPM> ChestPMs { get; set; }
    }
}
