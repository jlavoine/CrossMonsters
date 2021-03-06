﻿using Zenject;
using UnityEngine;
using MyLibrary;

namespace MonsterMatch {
    public class GameInstaller : MonoInstaller {
        public GameObject AudioManager;

        public override void InstallBindings() {
            Container.Bind<IAudioManager>().To<MyLibrary.AudioManager>().FromComponentInNewPrefab( AudioManager ).AsSingle();

            Container.Bind<IChainBuilder>().To<ChainBuilder>().AsSingle();
            Container.Bind<IChainValidator>().To<ChainValidator>().AsSingle();
            Container.Bind<IChainProcessor>().To<ChainProcessor>().AsSingle();
            Container.Bind<IChainValidator_DuplicatePieces>().To<ChainValidator_DuplicatePieces>().AsSingle();
            Container.Bind<IChainValidator_SelectablePieces>().To<ChainValidator_SelectablePieces>().AsSingle();
            Container.Bind<IChainValidator_DiagonalPieces>().To<ChainValidator_DiagonalPieces>().AsSingle();
            Container.Bind<IChainValidator_StraightLinesOnly>().To<ChainValidator_StraightLinesOnly>().AsSingle();
            Container.Bind<IChainValidator_MaxLength>().To<ChainValidator_MaxLength>().AsSingle();

            Container.Bind<IInitializable>().To<GameManager>().AsSingle();
            Container.Bind<IGameManager>().To<GameManager>().AsSingle();
            Container.Bind<IValidBoardChecker>().To<ValidBoardChecker>().AsSingle();

            Container.Bind<IInitializable>().To<GamePlayer>().AsSingle();
            Container.Bind<IGamePlayer>().To<GamePlayer>().AsSingle();
            Container.Bind<IGamePlayerPM>().To<GamePlayerPM>().AsTransient();

            Container.Bind<GameBackgroundPM>().AsSingle();

            Container.Bind<IInitializable>().To<GameBoard>().AsSingle();
            Container.Bind<IGameBoard>().To<GameBoard>().AsSingle();

            Container.Bind<IInitializable>().To<GameBoardPM>().AsSingle();
            Container.Bind<GameBoardPM>().AsSingle();                  

            Container.Bind<IGameRules>().To<GameRules>().AsSingle();
            Container.Bind<IMonsterManager>().To<MonsterManager>().AsSingle();

            Container.BindFactory<int, int, GamePiece, GamePiece.Factory>();

            Container.Bind<IDamageCalculator>().To<DamageCalculator>().AsSingle();

            Container.BindFactory<IDungeonReward, IAllRewardsPM, SingleRewardPM, SingleRewardPM.Factory>();
            Container.Bind<ISingleRewardPM_Spawner>().To<SingleRewardPM_Spawner>().AsSingle();

            Container.Bind<IDungeonWavePM>().To<DungeonWavePM>().AsSingle();

            Container.Bind<IInGameSettingsPM>().To<InGameSettingsPM>().AsSingle();
        }
    }
}