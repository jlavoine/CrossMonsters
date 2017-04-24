﻿using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;

#pragma warning disable 0219

namespace MonsterMatch {
    [TestFixture]
    public class TestMonsterPM : CrossMonstersUnitTest {
        [Test]
        public void WhenCreating_PropertiesSetAsExpected() {
            List<int> mockAttackCombo = new List<int>() { 1, 2, 3 };
            IGameMonster mockMonster = Substitute.For<IGameMonster>();
            mockMonster.Id.Returns( "Blob" );
            mockMonster.RemainingHP = 100;
            mockMonster.AttackCombo = mockAttackCombo;

            MonsterPM systemUnderTest = new MonsterPM( mockMonster );

            Assert.AreEqual( "Blob", systemUnderTest.ViewModel.GetPropertyValue<string>( MonsterPM.ID_PROPERTY ) );
            Assert.AreEqual( 100, systemUnderTest.ViewModel.GetPropertyValue<int>( MonsterPM.HP_PROPERTY ) );
            Assert.AreEqual( mockAttackCombo, systemUnderTest.AttackCombo );
            Assert.AreEqual( false, systemUnderTest.ViewModel.GetPropertyValue<bool>( MonsterPM.DESTROY_PROPERTY ) );
        }

        [Test]
        public void WhenModelUpdated_PropertiesAsExpected() {
            IGameMonster mockMonster = Substitute.For<IGameMonster>();
            mockMonster.RemainingHP = 100;
            MonsterPM systemUnderTest = new MonsterPM( mockMonster );

            Assert.AreEqual( 100, systemUnderTest.ViewModel.GetPropertyValue<int>( MonsterPM.HP_PROPERTY ) );

            mockMonster.RemainingHP = 50;
            mockMonster.ModelUpdated += Raise.Event<ModelUpdateHandler>();
            Assert.AreEqual( 50, systemUnderTest.ViewModel.GetPropertyValue<int>( MonsterPM.HP_PROPERTY ) );
        }

        [Test]
        public void WhenModelUpdated_IfMonsterIsDead_DestroyPropertyIsTrue() {
            IGameMonster mockMonster = Substitute.For<IGameMonster>();
            mockMonster.IsDead().Returns( false );
            MonsterPM systemUnderTest = new MonsterPM( mockMonster );

            mockMonster.IsDead().Returns( true );
            mockMonster.ModelUpdated += Raise.Event<ModelUpdateHandler>();
            Assert.AreEqual( true, systemUnderTest.ViewModel.GetPropertyValue<bool>( MonsterPM.DESTROY_PROPERTY ) );
        }
    }
}
