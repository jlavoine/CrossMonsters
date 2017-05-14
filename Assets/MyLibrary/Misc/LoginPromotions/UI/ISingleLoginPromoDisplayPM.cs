
namespace MyLibrary {
    public interface ISingleLoginPromoDisplayPM : IBasicWindowPM {
        string GetPrefab();

        void UpdateVisibilityBasedOnCurrentlyDisplayedPromo( string i_id );
    }
}
