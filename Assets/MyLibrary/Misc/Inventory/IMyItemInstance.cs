
namespace MyLibrary {
    public interface IMyItemInstance {
        string GetId();

        int GetCount();

        void SetCatalogItem( IMyCatalogItem i_catalogItem );
        void RemoveUses( int i_count );
    }
}
