
namespace MyLibrary {
    public interface IAccountLinker {
        void AttemptToLink( LoginMethods i_linkType, Callback<AccountLinkResultTypes> i_callback );
        void AttemptForceLink( LoginMethods i_linkType, Callback<bool> i_callback );
    }
}
