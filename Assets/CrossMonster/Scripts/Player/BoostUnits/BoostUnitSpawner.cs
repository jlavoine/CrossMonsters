using MyLibrary;

namespace MonsterMatch {
    public interface IBoostUnitSpawner {
        IBoostUnit Create( IMyItemInstance i_itemInstance );
    }

    public class BoostUnitSpawner : IBoostUnitSpawner {
        readonly BoostUnit.Factory factory;

        public BoostUnitSpawner( BoostUnit.Factory i_factory ) {
            this.factory = i_factory;
        }

        public IBoostUnit Create( IMyItemInstance i_itemInstance ) {
            return factory.Create( i_itemInstance, i_itemInstance.GetCustomData<BoostUnitCustomData>() );
        }
    }
}