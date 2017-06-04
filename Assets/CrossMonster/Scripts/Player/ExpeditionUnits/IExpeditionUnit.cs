
namespace MonsterMatch {
    public interface IExpeditionUnit {
        bool HasEffect( string i_effectId );

        int GetEffect( string i_effectId );
    }
}