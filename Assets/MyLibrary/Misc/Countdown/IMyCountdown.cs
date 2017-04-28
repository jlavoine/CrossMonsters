
namespace MyLibrary {
    public interface IMyCountdown {
        long RemainingTimeMs { get; }

        void Tick( long i_tickTimeMs );
    }
}
