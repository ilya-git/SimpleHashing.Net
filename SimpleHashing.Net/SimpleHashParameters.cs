using System;

namespace SimpleHashing.Net
{
    public class SimpleHashParameters
    {
        public string Algorithm { get; private set; }

        public int Iterations { get; private set; }

        public string Salt { get; private set; }

        public string PasswordHash { get; private set; }

        public SimpleHashParameters(string passwordHashString)
        {
            string[] parameters = ParseParameters(passwordHashString);

            ProcessParameters(parameters);
        }

        private static string[] ParseParameters(string passwordHashString)
        {
            string[] parameters = passwordHashString.Split(Constants.Splitter);

            if (parameters.Length != 4)
            {
                throw new ArgumentException("Invalid password hash string format", nameof(passwordHashString));
            }
            return parameters;
        }

        private void ProcessParameters(string[] parameters)
        {
            Algorithm = parameters[0];
            Iterations = int.Parse(parameters[1]);
            Salt = parameters[2];
            PasswordHash = parameters[3];
        }
    }
}