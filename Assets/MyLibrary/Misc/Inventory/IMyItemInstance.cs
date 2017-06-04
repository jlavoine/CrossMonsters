
namespace MyLibrary {
    public interface IMyItemInstance {
        string GetId();

        int GetCount();

        bool HasTag( string i_tag );

        T GetCustomData<T>();

        void SetCatalogItem( IMyCatalogItem i_catalogItem );
        void RemoveUses( int i_count );
    }
}
