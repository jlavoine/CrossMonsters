
namespace MyLibrary {
    public interface IPresentationModel {
        ViewModel ViewModel { get; }

        void Dispose();
    }
}
