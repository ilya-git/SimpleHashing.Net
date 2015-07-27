using System;

namespace SimpleHashing.Net
{
    public interface ISimpleHash
    {
        string Compute(string password);

        string Compute(string password, int iterations);

        bool Verify(string password, string passwordHashString);

        TimeSpan Estimate(string password, int iterations);
    }
}