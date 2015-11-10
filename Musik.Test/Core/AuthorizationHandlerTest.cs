using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Musik.Core.Bussiness;
using Musik.Core.Data;
using Sphere.Core;

namespace Musik.Test.Core
{
    [TestFixture]
    public class AuthorizationHandlerTest
    {
        [SetUp]
        public void SetUp()
        {
            SphereConfig.GlobalContext = new MusikContext();
        }

        [Test]
        public void TryAuthenticateWithBadCredentials()
        {
            var areValid = AuthorizationHandler.AreValidCredentials("Raul", "123");

            Assert.AreEqual(false, areValid);
        }

        [Test]
        public void TryAuthenticateWithEmptyCredentials()
        {
            var areValid = AuthorizationHandler.AreValidCredentials("", "");

            Assert.AreEqual(false, areValid);
        }

        [Test]
        public void TryAuthenticateWithNullCredentials()
        {
            var areValid = AuthorizationHandler.AreValidCredentials(null,null);

            Assert.AreEqual(false, areValid);
        }

        [Test]
        public void AuthenticateWithValidCredentials()
        {
            var areValid = AuthorizationHandler.AreValidCredentials("raulmonteroc", "123456");

            Assert.AreEqual(true, areValid);
        }
    }
}
