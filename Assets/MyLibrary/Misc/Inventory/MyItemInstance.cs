
namespace MyLibrary {
    public class MyItemInstance : IMyItemInstance {
        public string Id;
        public int Count;

        public string GetId() {
            return Id;
        }

        public int GetCount() {
            return Count;
        }
    }
}