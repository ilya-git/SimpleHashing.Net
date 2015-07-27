using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleHashing.Net.Tests
{
    [TestClass]
    public class SimpleHashParametersTests
    {
        private const string Test1 = "Rfc2898DeriveBytes$300$3buJ9SI7xEfO88bh3+Y4+g==$Y6IkgQ9U4p/dt7dIFCtApBSALB3N5i3qc/ojDKsj5gM=";

        [TestMethod, ExpectedException(typeof(NullReferenceException))]
        public void Constructor_WithNull_ThrowsException()
        {
            var parameters = new SimpleHashParameters(null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void Constructor_WithWrongValue_ThrowsException()
        {
            var parameters = new SimpleHashParameters("fgkljdflg");
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void Constructor_WithEmptyValue_ThrowsException()
        {
            var parameters = new SimpleHashParameters("");
        }

        [TestMethod]
        public void Constructor_WithProperString_ParserProperly()
        {
            var parameters = new SimpleHashParameters(Test1);

            Assert.AreEqual(300, parameters.Iterations);
            Assert.AreEqual("Rfc2898DeriveBytes", parameters.Algorithm);
            Assert.AreEqual("Y6IkgQ9U4p/dt7dIFCtApBSALB3N5i3qc/ojDKsj5gM=", parameters.PasswordHash);
            Assert.AreEqual("3buJ9SI7xEfO88bh3+Y4+g==", parameters.Salt);
        }
    }
}