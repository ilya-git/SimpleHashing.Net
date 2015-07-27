using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleHashing.Net.Tests
{
    [TestClass]
    public class SimpleHashTests
    {
        private const string TestPassword = "TestPassword";
        private const int Iterations = 500;

        private SimpleHash m_SimpleHash;

        [TestInitialize]
        public void TestInitialize()
        {
            m_SimpleHash = new SimpleHash();
        }

        [TestMethod]
        public void Estimate_Always_ReturnsReasonableTime()
        {
            TimeSpan estimate = m_SimpleHash.Estimate(TestPassword, 50);

            Assert.IsTrue(estimate.TotalMilliseconds < 10);
        }

        [TestMethod, ExpectedException(typeof (ArgumentException))]
        public void Verify_WithWrongAlgorithm_ThrowsException() // Smoke test, SimpleHashParameters covers this
        {
            m_SimpleHash.Verify(TestPassword, "wrongstring");
        }

        [TestMethod]
        public void Verify_AfterCompute_ReturnsTrue()
        {
            string hash = m_SimpleHash.Compute(TestPassword, Iterations);

            bool result = m_SimpleHash.Verify(TestPassword, hash);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Verify_AfterComputeUnicodePassword_ReturnsTrue()
        {
            const string unicodePassword = "Unicode_привет_øæ";

            string hash = m_SimpleHash.Compute(unicodePassword, Iterations);

            bool result = m_SimpleHash.Verify(unicodePassword, hash);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Verify_WithWrongPassword_ReturnsFalse()
        {
            string hash = m_SimpleHash.Compute(TestPassword, Iterations);

            bool result = m_SimpleHash.Verify(TestPassword + "1", hash);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Verfiy_WithoutIterationsParameter_WorksWithDefault()
        {
            string hash = m_SimpleHash.Compute(TestPassword);

            bool result = m_SimpleHash.Verify(TestPassword, hash);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Compute_Always_GeneratesProperString()
        {
            string hash = m_SimpleHash.Compute(TestPassword, Iterations);
            string[] hashArray = hash.Split('$');

            Assert.AreEqual("Rfc2898DeriveBytes", hashArray[0]);
            Assert.AreEqual(Iterations.ToString(), hashArray[1]);
            Assert.IsTrue(hashArray[2].Length > 0); // Just a smoke check as it's random salt there
            Assert.IsTrue(hashArray[3].Length > 0); // Just a smoke check as it's hard to verify what should be there (it is the actuall hash from Rfc2898DeriveBytes)
        }
    }
}