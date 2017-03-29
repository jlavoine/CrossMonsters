using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;
using Zenject;

#pragma warning disable 0219
#pragma warning disable 0414

namespace MyLibrary {
    public class TestLoginMethodManager : ZenjectUnitTestFixture {

        [Inject]
        IPreferredLoginMethod MockPreferredLogin;

        [Inject]
        ILoginMethodValidator MockLoginMethodValidator;

        [Inject]
        ILoginMethod_DeviceId MockDeviceIdLogin;

        [Inject]
        LoginMethodManager systemUnderTest;    

        [SetUp]
        public void CommonInstall() {
            Container.Bind<IPreferredLoginMethod>().FromInstance( Substitute.For<IPreferredLoginMethod>() );
            Container.Bind<ILoginMethodValidator>().FromInstance( Substitute.For<ILoginMethodValidator>() );
            Container.Bind<ILoginMethod_DeviceId>().FromInstance( Substitute.For<ILoginMethod_DeviceId>() );
            Container.Bind<LoginMethodManager>().AsSingle();
            Container.Inject( this );
        }

        static object[] LoginMethodTypes = {
            new object[] { LoginMethods.DeviceId },
            new object[] { LoginMethods.GameCenter },
        };

        [Test, TestCaseSource( "LoginMethodTypes" )]
        public void WhenAuthing_LoginMethodValidatorIsChecked( LoginMethods i_method ) {
            MockPreferredLogin.LoginMethod.Returns( i_method );

            systemUnderTest.Authenticate();

            MockLoginMethodValidator.Received().IsValid( i_method );
        }

        [Test, TestCaseSource( "LoginMethodTypes" )]
        public void WhenAuthing_IfMethodIsNotValid_MethodIsDefaultedToDeviceId( LoginMethods i_method ) {
            MockPreferredLogin.LoginMethod.Returns( i_method );
            MockLoginMethodValidator.IsValid( i_method ).Returns( false );

            systemUnderTest.Authenticate();

            Assert.AreEqual( LoginMethods.DeviceId, MockPreferredLogin.LoginMethod );
        }

        [Test]
        public void WhenLoggingInWithDeviceId_DeviceIdLoginIsAuthenticated() {
            MockPreferredLogin.LoginMethod.Returns( LoginMethods.DeviceId );

            systemUnderTest.Authenticate();

            MockDeviceIdLogin.Received().Authenticate();
        }
    }
}
