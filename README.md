# SimpleHashing.Net
This is a simple wrapper on top of Microsoft PBKDF2 implementation to store and verify password hashes.
It works very similar to bcrypt (not from the point of view of cryptography, but it generates a similar type of string that can be saved to database and used for verification later).

# Nuget

Nuget package is available here: https://www.nuget.org/packages/SimpleHashing.Net/

# Usage example

    ISimpleHash simpleHash = new SimpleHash();

    // Creating a user hash, hashedPassword can be stored in a database
    // hashedPassword contains the number of iterations and salt inside it similar to bcrypt format
    string hashedPassword = simpleHash.Compute("Password123");
	            
    // Validating user's password by first loading it from database by username
    string storedHash = _repository.GetUserPasswordHash(username);
    bool isPasswordValid = false;
    if (storedHash != null)
    {
        isPasswordValid = simpleHash.Verify("Password123", storedHash);
    }

# Security

SimpleHashing.Net does not do any self-made cryptography, but it is based on Microsoft implementation of PBKDF2 via the class Rfc2898DeriveBytes. The problem with this class is that it's not very convenient to use, so this simple wrapper allows an easy integration with solutions that need to store/verify password with strong cryptography. Simplicity was the main focus, so some parameters are hard-coded:

   1. Salt size is 16 bytes (128 bits)
   2. HashSize is 32 bytes (256 bits)
   3. Default iterations count is 50000
    
Iteration parameter can be passed to the Compute method to override the default settings. Rough estimate for execution time on few years old machine is 0.5 seconds. To increase security against brute force attacks this parameter can be increased to a desired value. For more information on number of iterations you can read an excellent [stackexchange answer](http://security.stackexchange.com/questions/3959/recommended-of-iterations-when-using-pkbdf2-sha256/3993#3993) by Thomas Pornin. 

You can use Estimate method to check how long it will take to compute the hash string.
