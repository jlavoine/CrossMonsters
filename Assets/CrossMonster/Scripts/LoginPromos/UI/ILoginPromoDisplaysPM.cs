using MyLibrary;
using System.Collections.Generic;

namespace MonsterMatch {
    public interface ILoginPromoDisplaysPM : IPresentationModel {        
        List<ISingleLoginPromoDisplayPM> DisplayPMs { get; }
    }
}
