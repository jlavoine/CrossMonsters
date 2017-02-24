/// <summary>
/// Core network messages that can be used across many games.
/// The actual values are arbitrary.
/// </summary>

namespace MyLibrary {
    public class CoreNetworkMessages {
        public const short Authenticate = 200;
        public const short OnAuthenticated = 201;
        public const short GameReady = 202;
    }
}