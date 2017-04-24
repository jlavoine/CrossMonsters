using System.Collections.Generic;
using Zenject;

namespace MonsterMatch {
    public class GameRules : IGameRules {
        private Dictionary<int, int> mPieceRotations;
        public Dictionary<int, int> PieceRotations { get { return mPieceRotations; } set { mPieceRotations = value; } }

        public List<int> PieceTypes;

        public int BoardSize;

        public GameRules() {
            PieceTypes = new List<int>() { 0, 1, 2, 3, 4 };
            PieceRotations = new Dictionary<int, int>() { { 0, 1 }, { 1, 2 }, { 2, 3 }, { 3, 4 }, { 4, 0 } };           
        }

        public int GetGamePieceRotation( int i_pieceType ) {
            if ( PieceRotations.ContainsKey( i_pieceType ) ) {
                return PieceRotations[i_pieceType];
            } else {
                return i_pieceType;
            }
        }

        public int GetActiveMonsterCount() {
            return 3;
        }

        public int GetBoardSize() {
            return 6;
        }

        public List<int> GetPieceTypes() {
            return PieceTypes;
        }
    }
}
