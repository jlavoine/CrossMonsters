
namespace MyLibrary {
    public interface ILinkAccountButton {
        void OnClick();
        void ForceLinkAccount();
        void UnlinkAccount();
        void SetPreferredLoginMethod();
    }
}