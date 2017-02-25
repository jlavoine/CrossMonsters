
namespace CrossMonsters {
    public class GameRules : IGameRules {
        private static IGameRules mInstance;
        public static IGameRules Instance {
            get {
                if ( mInstance == null ) {
                    mInstance = new GameRules();
                }
                return mInstance;
            }
            set {
                // tests only!
                mInstance = value;
            }
        }
    }
}
