
namespace MyLibrary {
    public interface IGoogleLinker {
        void AttemptLink( Callback<AccountLinkResultTypes> i_requestCallback, bool i_forceLink = false );
    }
}
