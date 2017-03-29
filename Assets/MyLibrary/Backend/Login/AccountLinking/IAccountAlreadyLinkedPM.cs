
namespace MyLibrary {
    public interface IAccountAlreadyLinkedPM : IBasicWindowPM {
        ILinkAccountButton LinkMethod { get; set; }

        void ForceLink();
    }
}
