using Konscious.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;


namespace LilaRent.Application.Services;


public class CryptographyService
{
    private const int _hashSize = 64;

    public CryptographyService() 
    { }

    public string GenerateSalt()
    {
        var randomBytes = RandomNumberGenerator.GetBytes(_hashSize);
        return Convert.ToBase64String(randomBytes);
    }

    public string HashPassword(string password, string salt)
    {
        var argon2 = new Argon2i(Encoding.UTF8.GetBytes(password))
        {
            DegreeOfParallelism = 16,
            MemorySize = 8192,
            Iterations = 40,
            Salt = Encoding.UTF8.GetBytes(salt)
        };

        var hash = argon2.GetBytes(_hashSize);
        var res = Convert.ToBase64String(hash);
        return res;
    }
}
