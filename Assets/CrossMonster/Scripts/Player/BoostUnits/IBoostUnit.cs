
namespace MonsterMatch {
    public interface IBoostUnit {
        bool HasEffect( string i_effectId );

        int GetEffect( string i_effectId );
    }
}