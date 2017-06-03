
namespace MonsterMatch {
    public interface IExpeditionUnitCustomData {
        bool HasEffect( string i_effectId );

        int GetEffect( string i_effectId );
    }
}