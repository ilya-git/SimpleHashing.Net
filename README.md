# SimpleHashing.Net
This is a simple wrapper on top of Microsoft PBKDF2 implementation to store and verify password hashes.
It works very similar to bcrypt (not from the point of view of cryptography, but it generates a similar type of string that can be saved to database and used for verification later).

#Usage example

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
