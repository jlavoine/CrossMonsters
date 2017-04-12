
namespace MyLibrary {
    public interface ILinkAccountButton {
        void OnClick( LoginMethods i_linkType );
        void ForceLinkAccount();
        void SetPreferredLoginMethod();
    }
}