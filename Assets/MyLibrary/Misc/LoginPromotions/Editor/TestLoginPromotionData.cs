﻿using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System;
using System.Collections.Generic;
using Zenject;

#pragma warning disable 0219
#pragma warning disable 0414

namespace MyLibrary {
    [TestFixture]
    public class TestLoginPromotionData : ZenjectUnitTestFixture {

        [Test]
        public void WhenCreating_StartAndEndTimes_AreAccurate() {
            LoginPromotionData systemUnderTest = new LoginPromotionData();
            systemUnderTest.StartDateInMs = 1000;
            systemUnderTest.EndDateInMs = 5000;

            DateTime expectedStartTime = new DateTime( 1970, 1, 1, 0, 0, 0, DateTimeKind.Utc ).AddMilliseconds( 1000 );
            DateTime expectedEndTime = new DateTime( 1970, 1, 1, 0, 0, 0, DateTimeKind.Utc ).AddMilliseconds( 5000 );

            Assert.AreEqual( expectedStartTime, systemUnderTest.GetStartTime() );
            Assert.AreEqual( expectedEndTime, systemUnderTest.GetEndTime() );
        }

        static object[] IsActiveTests = {
            new object[] { 0, 1000, 0, true },
            new object[] { 1, 1000, 0, false },
            new object[] { 0, 1000, 100, true },
            new object[] { 0, 1000, 1000, true },
            new object[] { 0, 1000, 2000, false }
        };

        [Test, TestCaseSource( "IsActiveTests" )]
        public void IsActive_ReturnsAsExpected( long i_startTimeMs, long i_endTimeMs, long i_currentTimeMs, bool i_expected ) {
            LoginPromotionData systemUnderTest = new LoginPromotionData();
            systemUnderTest.StartDateInMs = i_startTimeMs;
            systemUnderTest.EndDateInMs = i_endTimeMs;

            DateTime currentTime = new DateTime( 1970, 1, 1, 0, 0, 0, DateTimeKind.Utc ).AddMilliseconds( i_currentTimeMs );
            bool isActive = systemUnderTest.IsActive( currentTime );

            Assert.AreEqual( i_expected, isActive );
        }
    }
}