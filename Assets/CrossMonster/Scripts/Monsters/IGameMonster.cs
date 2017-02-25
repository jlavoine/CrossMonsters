
namespace CrossMonsters {
    public interface IGameMonster {
        int RemainingHP { get; set; }

        void Tick( long i_time );   
    }
}
