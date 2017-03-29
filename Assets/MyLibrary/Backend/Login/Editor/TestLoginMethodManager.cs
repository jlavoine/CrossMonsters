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
        ILoginMethod_GameCenter MockGameCenterLogin;

        [Inject]
        IBackendManager MockBackendManager;

        [Inject]
        LoginMethodManager systemUnderTest;    

        [SetUp]
        public void CommonInstall() {
            Container.Bind<IPreferredLoginMethod>().FromInstance( Substitute.For<IPreferredLoginMethod>() );
            Container.Bind<ILoginMethodValidator>().FromInstance( Substitute.For<ILoginMethodValidator>() );
            Container.Bind<ILoginMethod_DeviceId>().FromInstance( Substitute.For<ILoginMethod_DeviceId>() );
            Container.Bind<ILoginMethod_GameCenter>().FromInstance( Substitute.For<ILoginMethod_GameCenter>() );
            Container.Bind<IBackendManager>().FromInstance( Substitute.For<IBackendManager>() );
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
            MockLoginMethodValidator.IsValid( LoginMethods.DeviceId ).Returns( true );

            systemUnderTest.Authenticate();

            MockDeviceIdLogin.Received().Authenticate();
        }

        [Test]
        public void WhenLoggingInWithGameCenter_GameCenterLoginIsAuthenticated() {
            MockPreferredLogin.LoginMethod.Returns( LoginMethods.GameCenter );
            MockLoginMethodValidator.IsValid( LoginMethods.GameCenter ).Returns( true );

            systemUnderTest.Authenticate();

            MockGameCenterLogin.Received().Authenticate();
        }

        [Test, TestCaseSource( "LoginMethodTypes" )]
        public void AfterLogin_IfNotUsingDeviceId_LinkDeviceToAccount( LoginMethods i_method ) {
            MockPreferredLogin.LoginMethod.Returns( i_method );
            MockLoginMethodValidator.IsValid( i_method ).Returns( true );

            IBasicBackend mockBackend = Substitute.For<IBasicBackend>();
            MockBackendManager.GetBackend<IBasicBackend>().Returns( mockBackend );

            systemUnderTest.OnLogin();

            if ( i_method != LoginMethods.DeviceId ) {
                mockBackend.Received().LinkDeviceToAccount( Arg.Any<Callback<bool>>() );
            }
        }
    }
}
