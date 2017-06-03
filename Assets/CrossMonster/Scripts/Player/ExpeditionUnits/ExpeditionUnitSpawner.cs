using MyLibrary;

namespace MonsterMatch {
    public interface IExpeditionUnitSpawner {
        IExpeditionUnit Create( IMyItemInstance i_itemInstance );
    }

    public class ExpeditionUnitSpawner : IExpeditionUnitSpawner {
        readonly ExpeditionUnit.Factory factory;

        public ExpeditionUnitSpawner( ExpeditionUnit.Factory i_factory ) {
            this.factory = i_factory;
        }

        public IExpeditionUnit Create( IMyItemInstance i_itemInstance ) {
            return factory.Create( i_itemInstance, i_itemInstance.GetCustomData<ExpeditionUnitCustomData>() );
        }
    }
}