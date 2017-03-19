using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using Zenject;

#pragma warning disable 0219

namespace MyLibrary {
    [TestFixture]
    public class TestAllNewsPM {

        [Test]
        public void WhenDismissed_EventIsSent() {
            IMessageService mockMessenger = Substitute.For<IMessageService>();
            AllNewsPM systemUnderTest = new AllNewsPM( mockMessenger );

            systemUnderTest.Hide();

            mockMessenger.Received().Send( AllNewsPM.NEWS_DIMISSED_EVENT );
        }
    }
}
