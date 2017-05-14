using System.Collections.Generic;

namespace MyLibrary {
    public interface IActiveLoginPromoPM : IBasicWindowPM {
        List<IActiveLoginPromoButtonPM> ButtonPMs { get; }        
    }
}
