using System.Collections.Generic;

namespace MyLibrary {
    public interface ILoginPromoDisplaysPM : IPresentationModel {        
        List<ISingleLoginPromoDisplayPM> DisplayPMs { get; }

        void DisplayPromoAndHideOthers( string i_id );

        bool DoesHaveDisplayForPromo( string i_id );
    }
}
