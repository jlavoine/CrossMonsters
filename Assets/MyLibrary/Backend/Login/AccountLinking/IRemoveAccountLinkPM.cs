
namespace MyLibrary {
    public interface IRemoveAccountLinkPM : IBasicWindowPM {
        ILinkAccountButton LinkMethod { get; set; }

        void AttemptToUnlinkAccount();
    }
}
