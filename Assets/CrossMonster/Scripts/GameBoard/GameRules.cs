﻿using System.Collections.Generic;

namespace CrossMonsters {
    public class GameRules : IGameRules {
        private static IGameRules mInstance;
        public static IGameRules Instance {
            get {
                if ( mInstance == null ) {
                    mInstance = new GameRules();
                }
                return mInstance;
            }
            set {
                // tests only!
                mInstance = value;
            }
        }

        private Dictionary<int, int> mPieceRotations;
        public Dictionary<int, int> PieceRotations { get { return mPieceRotations; } set { mPieceRotations = value; } }

        public int GetGamePieceRotation( int i_pieceType ) {
            if ( PieceRotations.ContainsKey( i_pieceType ) ) {
                return PieceRotations[i_pieceType];
            } else {
                return i_pieceType;
            }
        }
    }
}
