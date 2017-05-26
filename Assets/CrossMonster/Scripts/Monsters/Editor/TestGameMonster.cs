﻿using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;

#pragma warning disable 0219
#pragma warning disable 0414

namespace MonsterMatch {
    [TestFixture]
    public class TestGameMonster : CrossMonstersUnitTest {
        [Test]
        public void WhenCreating_RemainingHP_EqualsMaxHP() {
            IMonsterData mockData = Substitute.For<IMonsterData>();
            mockData.GetMaxHP().Returns( 101 );

            GameMonster systemUnderTest = new GameMonster( mockData );

            Assert.AreEqual( 101, systemUnderTest.RemainingHP );
        }

        [Test]
        public void WhenCreating_AttackCycleIsZero() {
            GameMonster systemUnderTest = new GameMonster( Substitute.For<IMonsterData>() );

            Assert.AreEqual( 0f, systemUnderTest.AttackCycle );
        }

        [Test]
        public void WhenCreating_StatsSetToDataValues() {
            List<int> mockAttackCombo = new List<int>() { 1, 2, 3 };

            IMonsterData mockData = Substitute.For<IMonsterData>();
            mockData.GetDefense().Returns( 10 );
            mockData.GetDefenseType().Returns( 2 );
            mockData.GetAttackRate().Returns( 1500 );
            mockData.GetDamage().Returns( 1000 );
            mockData.GetDamageType().Returns( 30 );
            mockData.GetAttackCombo().Returns( mockAttackCombo );
            mockData.GetId().Returns( "Blob" );

            GameMonster systemUnderTest = new GameMonster( mockData );

            Assert.AreEqual( 10, systemUnderTest.Defense );
            Assert.AreEqual( 2, systemUnderTest.DefenseType );
            Assert.AreEqual( 1500, systemUnderTest.AttackRate );
            Assert.AreEqual( 1000, systemUnderTest.AttackPower );
            Assert.AreEqual( 30, systemUnderTest.AttackType );
            Assert.AreEqual( mockAttackCombo, systemUnderTest.AttackCombo );
            Assert.AreEqual( "Blob", systemUnderTest.Id );
        }

        static object[] DamagedTestCases = {
            new object[] { 5, 0, 5 },
            new object[] { 5, 4, 1 },
            new object[] { 5, 5, 1 },
            new object[] { -1, 5, 1 }
        };

        [Test, TestCaseSource("DamagedTestCases")]
        public void WhenAttackedByPlayer_ExpectedDamageRemovedFromRemainingHP( int i_playerAttackPower, int i_monsterDefense, int i_expectedDamage ) {
            IGamePlayer mockPlayer = Substitute.For<IGamePlayer>();
            mockPlayer.GetAttackPowerForType( Arg.Any<int>() ).Returns( i_playerAttackPower );

            IMonsterData mockMonsterData = Substitute.For<IMonsterData>();
            mockMonsterData.GetMaxHP().Returns( 100 );
            mockMonsterData.GetDefense().Returns( i_monsterDefense );

            GameMonster systemUnderTest = new GameMonster( mockMonsterData );

            systemUnderTest.AttackedByPlayer( mockPlayer );

            int expectedHP = 100 - i_expectedDamage;
            Assert.AreEqual( expectedHP, systemUnderTest.RemainingHP );
        }

        static object[] IsDeadTestCases = {
            new object[] { 0, true },
            new object[] { 1, false },
            new object[] { 10, false },
            new object[] { -1, true },
        };

        [Test, TestCaseSource("IsDeadTestCases")]
        public void IsDead_ReturnsExpected( int i_remainingHP, bool i_isDead ) {
            GameMonster systemUnderTest = new GameMonster( Substitute.For<IMonsterData>() );
            systemUnderTest.RemainingHP = i_remainingHP;

            Assert.AreEqual( i_isDead, systemUnderTest.IsDead() );
        }        

        [Test]
        public void AfterTick_WhenAttackIsNotCycled_ValueIsAsExpected() {
            IMonsterData mockMonsterData = Substitute.For<IMonsterData>();
            mockMonsterData.GetAttackRate().Returns( 1000 );

            GameMonster systemUnderTest = new GameMonster( mockMonsterData );
            systemUnderTest.Tick( 100 );

            Assert.AreEqual( 100, systemUnderTest.AttackCycle );
        }

        [Test]
        public void AfterTick_AttackCycleResetsToZero_WhenTickEqualsRate() {
            IMonsterData mockMonsterData = Substitute.For<IMonsterData>();
            mockMonsterData.GetAttackRate().Returns( 1000 );

            GameMonster systemUnderTest = new GameMonster( mockMonsterData );
            systemUnderTest.Tick( 1000 );

            Assert.AreEqual( 0, systemUnderTest.AttackCycle );
        }

        [Test]
        public void AfterTick_WhenAttackCycled_ValueIsAsExpected() {
            IMonsterData mockMonsterData = Substitute.For<IMonsterData>();
            mockMonsterData.GetAttackRate().Returns( 1000 );

            GameMonster systemUnderTest = new GameMonster( mockMonsterData );
            systemUnderTest.Tick( 1100 );

            Assert.AreEqual( 100, systemUnderTest.AttackCycle );
        }

        [Test]
        public void NegativeTicks_DoNotChangeAttackCycle() {
            IMonsterData mockMonsterData = Substitute.For<IMonsterData>();
            mockMonsterData.GetAttackRate().Returns( 1000 );

            GameMonster systemUnderTest = new GameMonster( mockMonsterData );
            systemUnderTest.Tick( -1000 );

            Assert.AreEqual( 0, systemUnderTest.AttackCycle );
        }

        [Test]
        public void AfterTick_IfLappingCycle_ValueIsAsExpected() {
            IMonsterData mockMonsterData = Substitute.For<IMonsterData>();
            mockMonsterData.GetAttackRate().Returns( 1000 );

            GameMonster systemUnderTest = new GameMonster( mockMonsterData );
            systemUnderTest.Tick( 2254 );

            Assert.AreEqual( 254, systemUnderTest.AttackCycle );
        }

        static object[] AttackProgressTests = {
            new object[] { 100, 200, 0.5f },
            new object[] { 10, 100, 0.1f }
        };

        [Test, TestCaseSource( "AttackProgressTests" )]
        public void GetAttackProgress_ReturnsAsExpected( long i_curAttackCycle, long i_attackRate, float i_expected ) {
            IMonsterData mockMonsterData = Substitute.For<IMonsterData>();
            mockMonsterData.GetAttackRate().Returns( i_attackRate );

            GameMonster systemUnderTest = new GameMonster( mockMonsterData );
            systemUnderTest.Tick( i_curAttackCycle );

            Assert.AreEqual( i_expected, systemUnderTest.GetAttackProgress() );
        }

        static object[] AttackCountTests = {
            new object[] { 1000, 1 },
            new object[] { 1500, 1 },
            new object[] { 2000, 2 },
            new object[] { 2222, 2 },
        };

        [Test, TestCaseSource("AttackCountTests")]
        public void AfterTick_IfLappingCycle_CorrectNumberOfAttacksSent( long i_tick, int i_expectedAttacks ) {
            IMonsterData mockMonsterData = Substitute.For<IMonsterData>();
            mockMonsterData.GetAttackRate().Returns( 1000 );

            GameMonster systemUnderTest = new GameMonster( mockMonsterData );
            systemUnderTest.Tick( i_tick );

            MyMessenger.Instance.Received( i_expectedAttacks ).Send<IGameMonster>( GameMessages.MONSTER_ATTACK, systemUnderTest );
        }

        static object[] AttackComboMatchTests = {
            new object[] { new List<int>() { 1, 2, 3 }, true },
            new object[] { new List<int>() { 1, 2, 3, 4 }, false },
            new object[] { new List<int>() { 1, 2 }, false },
            new object[] { null, false },
            new object[] { new List<int>(), false },
        };

        [Test, TestCaseSource( "AttackComboMatchTests" )]
        public void DoesMatchCombo_ReturnsAsExpected( List<int> i_comboAttempt, bool i_expectedMatch ) {
            IMonsterData mockMonsterData = Substitute.For<IMonsterData>();
            mockMonsterData.GetAttackCombo().Returns( new List<int>() { 1, 2, 3 });

            List<IGamePiece> comboAttempt = new List<IGamePiece>();
            if ( i_comboAttempt != null ) {
                foreach ( int pieceType in i_comboAttempt ) {
                    IGamePiece mockPiece = Substitute.For<IGamePiece>();
                    mockPiece.PieceType.Returns( pieceType );
                    comboAttempt.Add( mockPiece );
                }
            }

            GameMonster systemUnderTest = new GameMonster( mockMonsterData );

            bool doesMatch = systemUnderTest.DoesMatchCombo( comboAttempt );

            Assert.AreEqual( i_expectedMatch, doesMatch );
        }
    }
}