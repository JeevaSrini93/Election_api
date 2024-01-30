using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace Test_APi
{
    public class HashPassword
    {
            public static string hashPassword(string password)
            {
                // Generate a random salt
                byte[] salt = new byte[16];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }

                // Hash the password using PBKDF2 with 10000 iterations
                string hashedPassword = Convert.ToBase64String(
                    KeyDerivation.Pbkdf2(
                        password: password,
                        salt: salt,
                        prf: KeyDerivationPrf.HMACSHA256,
                        iterationCount: 10000,
                        numBytesRequested: 256 / 8
                    )
                );

                // Combine the salt and hashed password for storage
                string storedPassword = $"{Convert.ToBase64String(salt)}:{hashedPassword}";

                return storedPassword;
            }

            public static bool VerifyPassword(string hashedPassword, string enteredPassword)
            {
                // Extract the salt and hashed password from the stored password
                string[] parts = hashedPassword.Split(':');
                if (parts.Length != 2)
                {
                    // Invalid stored password format
                    return false;
                }

                byte[] salt = Convert.FromBase64String(parts[0]);
                string storedHashedPassword = parts[1];

                // Hash the entered password with the stored salt
                string enteredHashedPassword = Convert.ToBase64String(
                    KeyDerivation.Pbkdf2(
                        password: enteredPassword,
                        salt: salt,
                        prf: KeyDerivationPrf.HMACSHA256,
                        iterationCount: 10000,
                        numBytesRequested: 256 / 8
                    )
                );

                // Compare the stored hashed password with the entered hashed password
                return storedHashedPassword == enteredHashedPassword;
            }
        }
    }

